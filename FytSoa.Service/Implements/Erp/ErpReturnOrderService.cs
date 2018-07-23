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
                var guidArray = roGoodsList.Select(m=>m.GoodsGuid).ToList();
                //根据返货的商品，查询平台的条形码
                var goodsSkuList = ErpGoodsSkuDb.GetList(m=>guidArray.Contains(m.Guid));
                //根据返货的商品，查询加盟商的条形码
                var shopsSkuList = ErpShopSkuDb.GetList(m => guidArray.Contains(m.SkuGuid) && m.ShopGuid==parm.ShopGuid);
                //验证返货商品的数量是否大于库存数量
                foreach (var item in roGoodsList.GroupBy(m=>m.GoodsGuid).Select(m=>new ErpReturnGoods { GoodsGuid=m.Key,ReturnCount=m.Sum(g=>g.ReturnCount) }).ToList())
                {
                    var shopStockSum = shopsSkuList.Find(m=>m.SkuGuid==item.GoodsGuid);
                    if (shopStockSum.Stock < item.ReturnCount)
                    {
                        isStockSuccess = false;
                    }
                    //加盟商条形码表，减少返货的库存
                    shopsSkuList.Find(m => m.SkuGuid == item.GoodsGuid).Stock = shopStockSum.Stock - item.ReturnCount;
                    //平台条形码表，增加返货的库存
                    var goodsStock = goodsSkuList.Find(m => m.Guid == item.GoodsGuid);
                    goodsSkuList.Find(m => m.Guid == item.GoodsGuid).StockSum = goodsStock.StockSum + item.ReturnCount;
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
                parm.GoodsSum = roGoodsList.Sum(m=>m.ReturnCount);
                //查询今天返货数量
                DateTime dayTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString() + " 00:00:00");
                var dayCount = ErpReturnOrderDb.Count(m => SqlFunc.DateIsSame(m.AddDate, dayTime));                
                parm.Number= "RO-" + DateTime.Now.ToString("yyyyMMdd") + "-" + (1001 + dayCount);
                var result = Db.Ado.UseTran(() =>
                {
                    //添加订单
                    Db.Insertable(parm).ExecuteCommand();
                    //添加订单商品
                    Db.Insertable(roGoodsList).ExecuteCommand();
                    //修改平台库存
                    Db.Updateable(goodsSkuList).ExecuteCommand();
                    //修改加盟商库存
                    Db.Updateable(shopsSkuList).ExecuteCommand();
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
                new object[] {
                    JoinType.Left,ero.ShopGuid==es.Guid,
                    JoinType.Left,ero.StaffGuid==est.Guid })
                        .WhereIF(!string.IsNullOrEmpty(parm.guid), (ero, es, est) => ero.ShopGuid == parm.guid)
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
