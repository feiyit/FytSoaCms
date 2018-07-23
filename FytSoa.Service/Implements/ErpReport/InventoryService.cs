using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Erp;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Implements
{
    /// <summary>
    /// 库存盘点服务实现
    /// </summary>
    public class InventoryService : DbContext, IInventoryService
    {
        /// <summary>
        /// 查询多条记录
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<Page<StockInventory>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<StockInventory>>();
            try
            {
                string beginTime = string.Empty, endTime = string.Empty;
                if (!string.IsNullOrEmpty(parm.time))
                {
                    var timeRes = Utils.SplitString(parm.time, '-');
                    beginTime = timeRes[0].Trim();
                    endTime = timeRes[1].Trim();
                }
                var query = Db.Queryable<ErpGoodsSku>()
                    .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.Code.Contains(parm.key))
                    .WhereIF(!string.IsNullOrEmpty(parm.guid), m => m.BrankGuid == parm.guid)
                    .WhereIF(!string.IsNullOrEmpty(parm.time), m => m.AddDate >= Convert.ToDateTime(beginTime) && m.AddDate <= Convert.ToDateTime(endTime))

                    .PartitionBy(m => new { m.Code, m.Guid })
                    .Select(m => new StockInventory() {
                        Code = m.Code,
                        Sale = SqlFunc.AggregateSum(m.SaleSum),
                        Stock = m.StockSum,
                        TotalStock = SqlFunc.Subqueryable<ErpInOutLog>().Where(g => g.GoodsGuid == m.Guid && g.Types == 1).Sum(g => g.GoodsSum),
                        OutStock= SqlFunc.Subqueryable<ErpInOutLog>().Where(g => g.GoodsGuid == m.Guid && g.Types == 2).Sum(g => g.GoodsSum),
                        Transfer = SqlFunc.Subqueryable<ErpTransferGoods>().Where(g => g.GoodsGuid == m.Guid).Sum(g => g.GoodsSum),
                        Return = SqlFunc.Subqueryable<ErpReturnGoods>().Where(g => g.GoodsGuid == m.Guid).Count(),
                        Back = SqlFunc.Subqueryable<ErpBackGoods>().Where(g => g.GoodsGuid == m.Guid).Count()
                    }).ToPage(parm.page, parm.limit);                
                res.success = true;
                res.message = "获取成功！";
                res.data = query;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return Task.Run(() => res);
        }

        /// <summary>
        /// 查询库存剩余数量和销售数量
        /// 可根据店铺查询，日期，品牌
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<Page<StockSaleNum>>> GetStockNumByShopAsync(PageParm parm, AppSearchParm searchParm)

        {
            var res = new ApiResult<Page<StockSaleNum>>();
            try
            {   
                var query=Db.Queryable<ErpShopSku,ErpGoodsSku>((t1,t2)=>new object[] {JoinType.Left,t1.SkuGuid==t2.Guid })
                    .Where((t1, t2) => t1.ShopGuid == parm.guid)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.brand), (t1, t2) => t2.BrankGuid == searchParm.brand)
                    .OrderByIF(parm.orderType == 1, (t1, t2) => t1.Sale, OrderByType.Desc)
                    .Select((t1, t2) => new StockSaleNum()
                    {
                        Guid = t2.Guid,
                        Code = t2.Code,
                        Brand = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == t2.BrankGuid).Select(g => g.Name),
                        Style = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == t2.StyleGuid).Select(g => g.Name),
                        Stock = t1.Sale,
                        returnSum = SqlFunc.Subqueryable<ErpReturnGoods>().Where(g => g.GoodsGuid == t1.SkuGuid && g.ShopGuid == parm.guid).Sum(g => g.ReturnCount)
                    }).ToPage(parm.page, parm.limit);

                //根据日期查询
                var guidList = query.Items.Select(m=>m.Guid).ToList();
                if (parm.types == 0)
                {
                    //所有
                    var dayList = ErpSaleOrderGoodsDb.GetList(m => guidList.Contains(m.GoodsGuid) && m.ShopGuid == parm.guid);
                    foreach (var item in query.Items)
                    {
                        item.Sale = dayList.Where(m => m.GoodsGuid == item.Guid).Sum(m => m.Counts);
                    }
                }
                if (parm.types==1)
                {
                    DateTime dayTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString() + " 00:00:00");
                    //本日
                    var dayList = ErpSaleOrderGoodsDb.GetList(m=>guidList.Contains(m.GoodsGuid) && m.ShopGuid == parm.guid &&
                    SqlFunc.DateIsSame(SqlFunc.Subqueryable<ErpSaleOrder>().Where(g => g.Number == m.OrderNumber).Select(g => g.AddDate), dayTime));
                    foreach (var item in query.Items)
                    {
                        item.Sale = dayList.Where(m=>m.GoodsGuid==item.Guid).Sum(m=>m.Counts);
                    }
                }
                if (parm.types == 2)
                {
                    //本月
                    DateTime now = DateTime.Now;
                    DateTime d1 = new DateTime(now.Year, now.Month, 1);
                    DateTime d2 = d1.AddMonths(1).AddDays(-1);
                    var dayList = ErpSaleOrderGoodsDb.GetList(m => guidList.Contains(m.GoodsGuid) && m.ShopGuid==parm.guid &&
                    SqlFunc.Between(SqlFunc.Subqueryable<ErpSaleOrder>().Where(g => g.Number == m.OrderNumber).Select(g => g.AddDate), d1, d2));
                    foreach (var item in query.Items)
                    {
                        item.Sale = dayList.Where(m => m.GoodsGuid == item.Guid).Sum(m => m.Counts);
                    }
                }
                if (parm.types==3)
                {
                    //自定义时间
                    var dayList = ErpSaleOrderGoodsDb.GetList(m => guidList.Contains(m.GoodsGuid) && m.ShopGuid == parm.guid &&
                    SqlFunc.Between(SqlFunc.Subqueryable<ErpSaleOrder>().Where(g => g.Number == m.OrderNumber).Select(g => g.AddDate), Convert.ToDateTime(searchParm.btime), Convert.ToDateTime(searchParm.etime)));
                    foreach (var item in query.Items)
                    {
                        item.Sale = dayList.Where(m => m.GoodsGuid == item.Guid).Sum(m => m.Counts);
                    }
                }
                res.data = query;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return Task.Run(() => res);
        }

        /// <summary>
        /// 查询营业额
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<DayTurnover>> GetTurnover(PageParm parm,AppSearchParm searchParm)
        {
            var res = new ApiResult<DayTurnover>();
            try
            {                
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                DateTime d2 = d1.AddMonths(1).AddDays(-1);

                DateTime dayTime = Convert.ToDateTime(now.AddDays(1).ToShortDateString() + " 00:00:00");
                //订单数
                var orderSum = Db.Queryable<ErpSaleOrder>()
                    .WhereIF(!string.IsNullOrEmpty(parm.guid),m=>m.ShopGuid==parm.guid)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.btime) && !string.IsNullOrEmpty(searchParm.btime),
                    m => SqlFunc.Between(m.AddDate, Convert.ToDateTime(searchParm.btime), Convert.ToDateTime(searchParm.etime)))
                    .WhereIF(parm.types == 1, m => SqlFunc.DateIsSame(m.AddDate, dayTime))
                    .WhereIF(parm.types == 2, m => SqlFunc.Between(m.AddDate, d1, d2))
                    .Count();
                //订单金额
                var orderMoney = Db.Queryable<ErpSaleOrder>()
                    .WhereIF(!string.IsNullOrEmpty(parm.guid), m => m.ShopGuid == parm.guid)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.btime) && !string.IsNullOrEmpty(searchParm.btime),
                    m => SqlFunc.Between(m.AddDate, Convert.ToDateTime(searchParm.btime), Convert.ToDateTime(searchParm.etime)))
                    .WhereIF(parm.types == 1, m => SqlFunc.DateIsSame(m.AddDate, dayTime))
                    .WhereIF(parm.types == 2, m => SqlFunc.Between(m.AddDate, d1, d2))
                    .Sum(m=>m.RealMoney);
                res.data = new DayTurnover()
                {
                    OrderSum = orderSum,
                    Money = orderMoney
                };
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return Task.Run(() => res);
        }
    }
}
