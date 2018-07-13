using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Erp;
using FytSoa.Core.Model.Sys;
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
    public class ErpSaleOrderService : DbContext, IErpSaleOrderService
    {
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpSaleOrder parm, string goodsJson)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = (int)ApiEnum.Error };
            try
            {
                parm.Guid = Guid.NewGuid().ToString();
                //判断销售订单字符串是否为空
                if (string.IsNullOrEmpty(goodsJson))
                {
                    res.message = "销售订单里面的商品不能为空~";
                    return await Task.Run(() => res);
                }
                var isStockSuccess = true;
                //解析字符串转换成List对象
                var roGoodsList = JsonConvert.DeserializeObject<List<ErpSaleOrderGoods>>(goodsJson);
                //验证销售商品的数量是否大于库存数量
                foreach (var item in roGoodsList.GroupBy(m => m.GoodsGuid).Select(m => new ErpSaleOrderGoods { GoodsGuid = m.Key, Counts = m.Sum(g => g.Counts) }).ToList())
                {
                    var shopStockSum = Db.Queryable<ErpInOutLog>()
                        .Where(m => m.ShopGuid == parm.ShopGuid && m.Types == 2 && m.GoodsGuid == item.GoodsGuid)
                        .Sum(m => m.GoodsSum);
                    if (shopStockSum < item.Counts)
                    {
                        isStockSuccess = false;
                    }
                }
                if (!isStockSuccess)
                {
                    res.message = "商品库存数量";
                    return await Task.Run(() => res);
                }
                //查询今天销售数量
                var dayCount = ErpSaleOrderDb.Count(m => SqlFunc.DateIsSame(m.AddDate, DateTime.Now));
                parm.Number = "SO-" + DateTime.Now.ToString("yyyyMMdd") + "-" + (1001 + dayCount);
                foreach (var item in roGoodsList)
                {
                    item.OrderNumber =  parm.Number;
                    item.Guid = Guid.NewGuid().ToString();
                }
                var result = Db.Ado.UseTran(() =>
                {                    
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
        /// 查询多条记录
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<Page<SaleOrderDto>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<SaleOrderDto>>();
            try
            {
                var query = Db.Queryable<ErpSaleOrder,ErpShops>((eso,es)=>new object[] { JoinType.Left,eso.ShopGuid==es.Guid})
                    .OrderBy((eso, es) => eso.AddDate, OrderByType.Desc)
                    .Select((eso, es) => new SaleOrderDto() {
                        Number= eso.Number,
                        ShopName=es.ShopName,
                        ActivityTypes = SqlFunc.ToString(eso.ActivityTypes),
                        SaleType= SqlFunc.ToString(eso.SaleType),
                        Counts =eso.Counts,
                        ActivityName=eso.ActivityName,
                        Money=eso.Money,
                        RealMoney=eso.RealMoney,
                        AddDate =eso.AddDate
                    })
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
    }
}
