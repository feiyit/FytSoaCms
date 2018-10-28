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
        /// 查询多条记录  库存盘点
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<Page<StockInventory>>> GetPagesAsync(PageParm parm, SearchSaleOrderGoods searchParm)
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
                    .WhereIF(!string.IsNullOrEmpty(searchParm.size), m => m.SizeGuid == searchParm.size)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.year), m => m.YearGuid == searchParm.year)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.season), m => m.SeasonGuid == searchParm.season)
                    .WhereIF(!string.IsNullOrEmpty(parm.time), m => m.AddDate >= Convert.ToDateTime(beginTime) && m.AddDate <= Convert.ToDateTime(endTime))

                    .PartitionBy(m => new { m.Code, m.Guid })
                    .Select(m => new StockInventory() {
                        Code = m.Code,
                        Sale = SqlFunc.AggregateSum(m.SaleSum),
                        Stock = m.StockSum,
                        TotalStock = SqlFunc.Subqueryable<ErpInOutLog>().Where(g => g.GoodsGuid == m.Guid && g.Types == 1).Sum(g => g.GoodsSum),
                        OutStock= SqlFunc.Subqueryable<ErpInOutLog>().Where(g => g.GoodsGuid == m.Guid && g.Types == 2).Sum(g => g.GoodsSum),
                        Transfer = SqlFunc.Subqueryable<ErpTransferGoods>().Where(g => g.GoodsGuid == m.Guid).Sum(g => g.GoodsSum),
                        Return = SqlFunc.Subqueryable<ErpReturnGoods>().Where(g => g.GoodsGuid == m.Guid && g.Status==1).Sum(g=>g.ReturnCount),
                        Back = SqlFunc.Subqueryable<ErpBackGoods>().Where(g => g.GoodsGuid == m.Guid && g.Status==1).Sum(g => g.BackCount),
                        Loss= SqlFunc.Subqueryable<ErpSkuLoss>().Where(g => g.SkuGuid == m.Code).Sum(g => g.Counts),
                    })
                    .OrderByIF(!string.IsNullOrEmpty(parm.field) && !string.IsNullOrEmpty(parm.order),parm.field+" "+parm.order)
                    .ToPage(parm.page, parm.limit);                
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
                    .OrderByIF(parm.orderType == 2, (t1, t2) => t1.Stock, OrderByType.Desc)
                    .Select((t1, t2) => new StockSaleNum()
                    {
                        Guid = t2.Guid,
                        Code = t2.Code,
                        Brand = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == t2.BrankGuid).Select(g => g.Name),
                        Style = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == t2.StyleGuid).Select(g => g.Name),
                        Stock = t1.Stock,
                        returnSum = SqlFunc.Subqueryable<ErpReturnGoods>().Where(g => g.GoodsGuid == t1.SkuGuid && g.ShopGuid == parm.guid && g.Status==1).Sum(g => g.ReturnCount)
                    }).ToPage(parm.page, parm.limit);

                //根据日期查询
                var guidList = query.Items.Select(m=>m.Guid).ToList();
                if (parm.types == 0)
                {
                    //所有
                    var dayList = ErpSaleOrderGoodsDb.GetList(m => guidList.Contains(m.GoodsGuid) && m.ShopGuid == parm.guid);
                    foreach (var item in query.Items)
                    {
                        item.Sale = dayList.Where(m => m.GoodsGuid == item.Guid).Sum(m => m.Counts) - dayList.Where(m => m.GoodsGuid == item.Guid).Sum(m => m.BackCounts);
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
                        item.Sale = dayList.Where(m=>m.GoodsGuid==item.Guid).Sum(m=>m.Counts)- dayList.Where(m => m.GoodsGuid == item.Guid).Sum(m => m.BackCounts);
                    }
                }
                if (parm.types == 2)
                {
                    //本月
                    DateTime now = DateTime.Now;
                    DateTime d1 = new DateTime(now.Year, now.Month, 1);
                    DateTime d2 = d1.AddMonths(1);
                    var dayList = ErpSaleOrderGoodsDb.GetList(m => guidList.Contains(m.GoodsGuid) && m.ShopGuid==parm.guid &&
                    SqlFunc.Between(SqlFunc.Subqueryable<ErpSaleOrder>().Where(g => g.Number == m.OrderNumber).Select(g => g.AddDate), d1, d2));
                    foreach (var item in query.Items)
                    {
                        item.Sale = dayList.Where(m => m.GoodsGuid == item.Guid).Sum(m => m.Counts) - dayList.Where(m => m.GoodsGuid == item.Guid).Sum(m => m.BackCounts);
                    }
                }
                if (parm.types==3)
                {
                    //自定义时间
                    var dayList = ErpSaleOrderGoodsDb.GetList(m => guidList.Contains(m.GoodsGuid) && m.ShopGuid == parm.guid &&
                    SqlFunc.Between(SqlFunc.Subqueryable<ErpSaleOrder>().Where(g => g.Number == m.OrderNumber).Select(g => g.AddDate), Convert.ToDateTime(searchParm.btime), Convert.ToDateTime(searchParm.etime)));
                    foreach (var item in query.Items)
                    {
                        item.Sale = dayList.Where(m => m.GoodsGuid == item.Guid).Sum(m => m.Counts) - dayList.Where(m => m.GoodsGuid == item.Guid).Sum(m => m.BackCounts);
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
                DateTime d2 = d1.AddMonths(1);

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
                //查询退单金额
                var backMoney=Db.Queryable<ErpBackGoods>()
                    .Where(m=>m.Status==1)
                    .WhereIF(!string.IsNullOrEmpty(parm.guid), m => m.ShopGuid == parm.guid)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.btime) && !string.IsNullOrEmpty(searchParm.btime),
                    m => SqlFunc.Between(m.AddDate, Convert.ToDateTime(searchParm.btime), Convert.ToDateTime(searchParm.etime)))
                    .WhereIF(parm.types == 1, m => SqlFunc.DateIsSame(m.AddDate, dayTime))
                    .WhereIF(parm.types == 2, m => SqlFunc.Between(m.AddDate, d1, d2))
                    .Sum(m => m.BackMoney);
                res.data = new DayTurnover()
                {
                    OrderSum = orderSum,
                    Money = orderMoney- backMoney
                };
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
        public Task<ApiResult<Page<ShopTurnover>>> GetShopTurnover(PageParm parm)
        {
            var res = new ApiResult<Page<ShopTurnover>>();
            try
            {
                //默认只查询当月的营业额
                DateTime now = DateTime.Now;
                DateTime beginTime = new DateTime(now.Year, now.Month, 1), endTime = beginTime.AddMonths(1);
                if (!string.IsNullOrEmpty(parm.time))
                {
                    var timeRes = Utils.SplitString(parm.time, '-');
                    beginTime = Convert.ToDateTime(timeRes[0].Trim());
                    endTime = Convert.ToDateTime(timeRes[1].Trim());
                }
                if (string.IsNullOrEmpty(parm.field) || string.IsNullOrEmpty(parm.order))
                {
                    parm.field = "Money";
                    parm.order = "desc";
                }
                var query = Db.Queryable<ErpShops>()
                    .WhereIF(!string.IsNullOrEmpty(parm.guid), m => m.Guid == parm.guid)
                    .Select(m=>new ShopTurnover() {
                        ShopName=m.ShopName,
                        Principal=m.AdminName,
                        Mobile=m.Mobile,
                        OrderCount= SqlFunc.Subqueryable<ErpSaleOrder>().Where(g => g.ShopGuid == m.Guid && SqlFunc.Between(g.AddDate, beginTime, endTime)).Count(),
                        Money= SqlFunc.Subqueryable<ErpSaleOrder>().Where(g => g.ShopGuid == m.Guid && SqlFunc.Between(g.AddDate, beginTime, endTime)).Sum(g=>g.RealMoney),
                        ReturnCount = SqlFunc.Subqueryable<ErpReturnOrder>().Where(g => g.Status==1 && g.ShopGuid == m.Guid && SqlFunc.Between(g.AddDate, beginTime, endTime)).Count(),
                        BackCount = SqlFunc.Subqueryable<ErpBackGoods>().Where(g => g.Status==1 && g.ShopGuid == m.Guid && SqlFunc.Between(g.AddDate, beginTime, endTime)).Count(),
                        BackMoney = SqlFunc.Subqueryable<ErpBackGoods>().Where(g => g.Status==1 && g.ShopGuid == m.Guid && SqlFunc.Between(g.AddDate, beginTime, endTime)).Sum(g=>g.BackMoney),
                    })
                    .OrderBy(parm.field + " " + parm.order)
                    .ToPage(parm.page,parm.limit);

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
        public Task<ApiResult<List<VMonthTurnover>>> GetMonthTurnover(PageParm parm)
        {
            var res = new ApiResult<List<VMonthTurnover>>();
            try
            {
                var query = Db.Queryable<VMonthTurnover>().OrderBy(m=>m.Months).ToList();
                if (query!=null && query.Count>0)
                {
                    for (int i = 1; i < 13; i++)
                    {
                        var month = "0";
                        if (i < 10) { month = month + i.ToString(); }
                        else { month = i.ToString(); }
                        if (query.Find(m=>m.Months==month)==null)
                        {
                            query.Add(new VMonthTurnover() { Months = month });
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < 13; i++)
                    {
                        var month = "0";
                        if (i < 10) { month=month + i.ToString(); }
                        else { month = i.ToString(); }
                        query.Add(new VMonthTurnover() { Months = month });
                    }
                }
                res.data = query.OrderBy(m=>m.Months).ToList();
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return Task.Run(() => res);
        }

        /// <summary>
        /// 获得加盟商列表，包含库存总数
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<List<ShopStockReport>>> GetShopStockReport(PageParm parm)
        {
            var res = new ApiResult<List<ShopStockReport>>();
            try
            {
                var query = Db.Queryable<ErpShops>()
                    .WhereIF(!string.IsNullOrEmpty(parm.guid),m=>m.Guid==parm.guid)
                    .Select(m=>new ShopStockReport() {
                        ShopGuid=m.Guid,
                        ShopName=m.ShopName,
                        Stock = SqlFunc.Subqueryable<ErpShopSku>().Where(g => g.ShopGuid == m.Guid).Sum(g=>g.Stock),
                    })
                    .OrderBy(m=>m.Stock,OrderByType.Desc)
                    .ToList();
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
        /// 平台入库统计报表，入库 总数，可根据年份查询
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<List<PlatformInOutStockReport>>> GetPlatformInStockReport(PageParm parm)
        {
            var res = new ApiResult<List<PlatformInOutStockReport>>();
            try
            {
                if (string.IsNullOrEmpty(parm.key))
                {
                    parm.key = DateTime.Now.Year.ToString();
                }
                var strSql = "select date_format(AddDate,'%m') AS `Months`,SUM(GoodsSum) as InCounts from erpinoutlog "
                    + "where Types="+parm.types+" and InTypes=1 and date_format(AddDate,'%Y')='"+parm.key+"' "
                    + "group by months";
                var query = Db.Ado.SqlQuery<PlatformInOutStockReport>(strSql);
                if (query != null && query.Count > 0)
                {
                    for (int i = 1; i < 13; i++)
                    {
                        var month = "0";
                        if (i < 10) { month = month + i.ToString(); }
                        else { month = i.ToString(); }
                        if (query.Find(m => m.Months == month) == null)
                        {
                            query.Add(new PlatformInOutStockReport() { Months = month });
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < 13; i++)
                    {
                        var month = "0";
                        if (i < 10) { month = month + i.ToString(); }
                        else { month = i.ToString(); }
                        query.Add(new PlatformInOutStockReport() { Months = month });
                    }
                }
                res.data = query.OrderBy(m => m.Months).ToList();
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return Task.Run(() => res);
        }

        /// <summary>
        /// 加盟商退货统计报表
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<List<ShopBackReturnReport>>> GetShopBackReport(PageParm parm)
        {
            var res = new ApiResult<List<ShopBackReturnReport>>();
            try
            {
                if (string.IsNullOrEmpty(parm.time))
                {
                    parm.time = DateTime.Now.Year.ToString();
                }
                var strSql = "SELECT ShopName,"
                    + "MAX(CASE tt.Months WHEN '01' then tt.Money ELSE 0 END) 'JanuaryMoney',"
                    + "MAX(CASE tt.Months WHEN '02' then tt.Money ELSE 0 END) 'FebruaryMoney',"
                    + "MAX(CASE tt.Months WHEN '03' then tt.Money ELSE 0 END) 'MarchMoney',"
                    + "MAX(CASE tt.Months WHEN '04' then tt.Money ELSE 0 END) 'AprilMoney',"
                    + "MAX(CASE tt.Months WHEN '05' then tt.Money ELSE 0 END) 'MayMoney',"
                    + "MAX(CASE tt.Months WHEN '06' then tt.Money ELSE 0 END) 'JuneMoney',"
                    + "MAX(CASE tt.Months WHEN '07' then tt.Money ELSE 0 END) 'JulyMoney',"
                    + "MAX(CASE tt.Months WHEN '08' then tt.Money ELSE 0 END) 'AugustMoney',"
                    + "MAX(CASE tt.Months WHEN '09' then tt.Money ELSE 0 END) 'SeptemberMoney',"
                    + "MAX(CASE tt.Months WHEN '10' then tt.Money ELSE 0 END) 'OctoberMoney',"
                    + "MAX(CASE tt.Months WHEN '11' then tt.Money ELSE 0 END) 'NovemberMoney',"
                    + "MAX(CASE tt.Months WHEN '12' then tt.Money ELSE 0 END) 'DecemberMoney'"
                    + "from ("
                    + "select s.Guid,s.ShopName,t1.Months,t1.Money,t1.Counts from erpshops as s left JOIN( "
                    + "select ShopGuid,date_format(AddDate,'%m') AS `Months`,SUM(BackMoney) as Money,sum(BackCount) as Counts "
                    + "from erpbackgoods  "
                    + "where date_format(AddDate,'%Y')=" + parm.time + " and erpbackgoods.status=1 "
                    + "group by ShopGuid,Months) as t1 "
                    + "on s.Guid=t1.ShopGuid"
                    + ") tt "
                    + (!string.IsNullOrEmpty(parm.key) ? " where tt.Guid='" + parm.key + "'" : "")
                    + "group by tt.ShopName";
                res.data = Db.Ado.SqlQuery<ShopBackReturnReport>(strSql);
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return Task.Run(() => res);
        }

        /// <summary>
        /// 加盟商返货统计报表
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<List<ShopBackReturnReport>>> GetShopReturnReport(PageParm parm)
        {
            var res = new ApiResult<List<ShopBackReturnReport>>();
            try
            {
                if (string.IsNullOrEmpty(parm.time))
                {
                    parm.time = DateTime.Now.Year.ToString();
                }
                var strSql = "SELECT ShopName,"
                    + "MAX(CASE tt.Months WHEN '01' then tt.Money ELSE 0 END) 'JanuaryMoney',"
                    + "MAX(CASE tt.Months WHEN '02' then tt.Money ELSE 0 END) 'FebruaryMoney',"
                    + "MAX(CASE tt.Months WHEN '03' then tt.Money ELSE 0 END) 'MarchMoney',"
                    + "MAX(CASE tt.Months WHEN '04' then tt.Money ELSE 0 END) 'AprilMoney',"
                    + "MAX(CASE tt.Months WHEN '05' then tt.Money ELSE 0 END) 'MayMoney',"
                    + "MAX(CASE tt.Months WHEN '06' then tt.Money ELSE 0 END) 'JuneMoney',"
                    + "MAX(CASE tt.Months WHEN '07' then tt.Money ELSE 0 END) 'JulyMoney',"
                    + "MAX(CASE tt.Months WHEN '08' then tt.Money ELSE 0 END) 'AugustMoney',"
                    + "MAX(CASE tt.Months WHEN '09' then tt.Money ELSE 0 END) 'SeptemberMoney',"
                    + "MAX(CASE tt.Months WHEN '10' then tt.Money ELSE 0 END) 'OctoberMoney',"
                    + "MAX(CASE tt.Months WHEN '11' then tt.Money ELSE 0 END) 'NovemberMoney',"
                    + "MAX(CASE tt.Months WHEN '12' then tt.Money ELSE 0 END) 'DecemberMoney'"
                    + "from ("
                    + "select s.Guid,s.ShopName,t1.Months,t1.Money from erpshops as s left JOIN( "
                    + "select ShopGuid,date_format(AddDate,'%m') AS `Months`,SUM(GoodsSum) as Money "
                    + "from erpreturnorder  "
                    + "where date_format(AddDate,'%Y')=" + parm.time + " "
                    + "group by ShopGuid,Months) as t1 "
                    + "on s.Guid=t1.ShopGuid"
                    + ") tt "
                    + (!string.IsNullOrEmpty(parm.key) ? " where tt.Guid='" + parm.key + "'" : "")
                    + "group by tt.ShopName";
                res.data = Db.Ado.SqlQuery<ShopBackReturnReport>(strSql);
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
