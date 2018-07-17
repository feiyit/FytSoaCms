using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Implements
{
    public class ErpReturnOrderService : DbContext, IErpReturnOrderService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpReturnOrder parm, string goodsJson)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = (int)ApiEnum.Error };
            try
            {
                parm.Guid = Guid.NewGuid().ToString();
                //判断返货订单字符串是否为空
                if (string.IsNullOrEmpty(goodsJson))
                {
                    res.message = "返货订单里面的商品不能为空~";
                    return await Task.Run(() => res);
                }
                var isStockSuccess = true;
                //解析字符串转换成List对象
                var roGoodsList = JsonConvert.DeserializeObject<List<ErpReturnGoods>>(goodsJson);
                //验证返货商品的数量是否大于库存数量
                foreach (var item in roGoodsList.GroupBy(m=>m.GoodsGuid).Select(m=>new ErpReturnGoods { GoodsGuid=m.Key,ReturnCount=m.Sum(g=>g.ReturnCount) }).ToList())
                {
                    var shopStockSum = Db.Queryable<ErpInOutLog>()
                        .Where(m=>m.ShopGuid==parm.ShopGuid && m.Types==2 && m.GoodsGuid==item.GoodsGuid)
                        .Sum(m=>m.GoodsSum);
                    if (shopStockSum<item.ReturnCount)
                    {
                        isStockSuccess = false;
                    }
                }
                if (!isStockSuccess)
                {
                    res.message = "返货的商品数量大于库存数量";
                    return await Task.Run(() => res);
                }
                foreach (var item in roGoodsList)
                {
                    item.OrderGuid = parm.Guid;
                    item.Guid = Guid.NewGuid().ToString();
                    item.ShopGuid = parm.ShopGuid;
                }
                //查询今天返货数量
                DateTime dayTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString() + " 00:00:00");
                var dayCount = ErpReturnOrderDb.Count(m => SqlFunc.DateIsSame(m.AddDate, dayTime));                
                parm.Number= "RO-" + DateTime.Now.ToString("yyyyMMdd") + "-" + (1001 + dayCount);
                var result = Db.Ado.UseTran(() =>
                {
                    //循环减少加盟商库存，增加平台库存
                    //foreach (var item in roGoodsList)
                    //{
                    //    item.OrderGuid = parm.Guid;
                    //    item.Guid = Guid.NewGuid().ToString();
                    //    //减少加盟商库存
                    //    Db.Updateable<ErpInOutLog>()
                    //    .UpdateColumns(m => m.GoodsSum == m.GoodsSum - item.ReturnCount)
                    //    .Where(m => m.ShopGuid == parm.ShopGuid && m.GoodsGuid == item.GoodsGuid && m.Types == 2)
                    //    .ExecuteCommand();
                    //    //增加平台库存
                    //    Db.Updateable<ErpGoodsSku>()
                    //    .UpdateColumns(m => m.StockSum == m.StockSum + item.ReturnCount)
                    //    .Where(m => m.Guid == item.GoodsGuid)
                    //    .ExecuteCommand();
                    //}
                    //添加订单
                    Db.Insertable(parm).ExecuteCommand();
                    //添加订单商品
                    Db.Insertable(roGoodsList).ExecuteCommand();
                    
                });
                res.statusCode = (int)ApiEnum.Status;
                if (!result.IsSuccess)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = result.ErrorMessage;
                }                
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var list = Utils.StrToListString(parm);
                var dbres = ErpReturnOrderDb.Update(m => new ErpReturnOrder() { IsDel = true }, m => list.Contains(m.Guid));
                if (!dbres)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "删除数据失败~";
                }
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<ReturnOrderDto>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<ReturnOrderDto>>();
            try
            {
                var query = Db.Queryable<ErpReturnOrder,ErpShops,ErpStaff>((ero,es,est)=>
                new object[] {JoinType.Left,ero.ShopGuid==es.Guid,JoinType.Left,es.Guid==est.ShopGuid })
                        .WhereIF(!string.IsNullOrEmpty(parm.guid), m => m.ShopGuid == parm.guid)
                        .OrderBy((ero, es, est)=>ero.AddDate,OrderByType.Desc)
                        .Select((ero, es, est) => new ReturnOrderDto() {
                            Guid=ero.Guid,
                            Number= ero.Number,
                            ShopName=es.ShopName,
                            Operator=est.TrueName,
                            Mobile=est.Mobile,
                            Counts= ero.GoodsSum,
                            Status=SqlFunc.ToString(ero.Status),
                            AddDate= ero.AddDate
                        })                        
                        .ToPageAsync(parm.page, parm.limit);
                res.success = true;
                res.message = "获取成功！";
                res.data = await query;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return await Task.Run(() => res);
        }
        
    }
}
