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
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Implements
{
    public class ErpBackGoodsService : DbContext, IErpBackGoodsService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpBackGoods parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = (int)ApiEnum.Error };
            try
            {
                DateTime dayTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString() + " 00:00:00");
                parm.Guid = Guid.NewGuid().ToString();
                //查询今天退货数量
                var dayCount = ErpBackGoodsDb.Count(m => SqlFunc.DateIsSame(m.AddDate, dayTime));
                parm.Number = "BO-" + DateTime.Now.ToString("yyyyMMdd") + "-" + (1001 + dayCount);
                //根据条形码查询唯一编号
                var goodSku = ErpGoodsSkuDb.GetSingle(m => m.Code == parm.GoodsGuid);
                if (goodSku != null)
                {
                    parm.GoodsGuid = goodSku.Guid;
                }
                else
                {
                    res.message = "商品不存在~";
                    return await Task.Run(() => res);
                }
                //判断退货商品，金额是否大于订单金额，   以及商品数量，是否大于订单出售数量
                var orderModel = ErpSaleOrderDb.GetSingle(m => m.Number == parm.OrderNumber);
                if (orderModel == null)
                {
                    res.message = "订单不存在~";
                    return await Task.Run(() => res);
                }
                //跨月不允许退货
                if (orderModel.AddDate.Month!=DateTime.Now.Month)
                {
                    res.message = "跨月不允许退货~";
                    return await Task.Run(() => res);
                }
                //查询活动，是否买一赠一活动，如果是，需要单独处理下
                var activityModel = new ErpShopActivity();
                if (!string.IsNullOrEmpty(orderModel.ActivityGuid))
                {
                    activityModel = ErpShopActivityDb.GetSingle(m => m.Guid == orderModel.ActivityGuid);
                }
                //if (parm.BackMoney>orderModel.RealMoney)
                //{
                //    res.message = "退货金额不能大于订单金额~";
                //    return await Task.Run(() => res);
                //}
                //判断是否存在
                var egbCount = ErpBackGoodsDb.Count(m => m.ShopGuid == parm.ShopGuid && m.GoodsGuid == parm.GoodsGuid && m.OrderNumber == parm.OrderNumber);
                if (egbCount>0)
                {
                    //买一增一，最多可以退货2次
                    if (activityModel!=null && activityModel.Method==3)
                    {
                        if (egbCount > 1)
                        {
                            res.message = "该退货信息已存在~";
                            return await Task.Run(() => res);
                        }
                    }
                    else
                    {
                        res.message = "该退货信息已存在~";
                        return await Task.Run(() => res);
                    }
                    
                }
                //根据订单查询商品数量是否满足
                var orderGoodsModel = new ErpSaleOrderGoods();
                if (activityModel != null && activityModel.Method == 3)
                {
                    //如果是买一赠一，第一次差=0的，第二次差！=0的
                    if (egbCount == 0)
                    {
                        orderGoodsModel = ErpSaleOrderGoodsDb.GetSingle(m => m.OrderNumber == parm.OrderNumber && m.GoodsGuid == parm.GoodsGuid && m.ShopGuid == parm.ShopGuid && m.Money==0);
                    }
                    else
                    {
                        orderGoodsModel = ErpSaleOrderGoodsDb.GetSingle(m => m.OrderNumber == parm.OrderNumber && m.GoodsGuid == parm.GoodsGuid && m.ShopGuid == parm.ShopGuid && m.Money!= 0);
                    }
                }
                else
                {
                    orderGoodsModel = ErpSaleOrderGoodsDb.GetSingle(m => m.OrderNumber == parm.OrderNumber && m.GoodsGuid == parm.GoodsGuid && m.ShopGuid == parm.ShopGuid);
                }
                if (orderGoodsModel == null)
                {
                    res.message = "该商品在该订单号中不存在~";
                    return await Task.Run(() => res);
                }
                if (orderGoodsModel.Counts < parm.BackCount)
                {
                    res.message = "退货商品数量不能大于订单购买数量~";
                    return await Task.Run(() => res);
                }
                //获取订单销售的金额  客户端不需要手动输入， 注意需要判断下是否买一赠一活动
                parm.BackMoney = orderGoodsModel.Money;
                //构建积分变动记录
               
                var result = Db.Ado.UseTran(() =>
                {
                    //修改加盟商条形码里面的库存 退货=加盟商库存增加
                    Db.Updateable<ErpShopSku>()
                    .UpdateColumns(m=>new ErpShopSku() { Stock=m.Stock+parm.BackCount,Sale=m.Sale-parm.BackCount })
                    .Where(m=>m.ShopGuid==parm.ShopGuid && m.SkuGuid== goodSku.Guid)
                    .ExecuteCommand();
                    //根据商品编号，修改平台的销售数量
                    Db.Updateable<ErpGoodsSku>().UpdateColumns(m=>new ErpGoodsSku() {SaleSum=m.SaleSum-parm.BackCount })
                    .Where(m=>m.Guid==goodSku.Guid).ExecuteCommand();
                    //修改订单-商品详情表中的退货数量   增加
                    orderGoodsModel.BackCounts = orderGoodsModel.BackCounts + parm.BackCount;
                    Db.Updateable(orderGoodsModel).ExecuteCommand();
                    //增加一条退货信息
                    Db.Insertable(parm).ExecuteCommand();
                    //判断是否存在用户信息
                    if (!string.IsNullOrEmpty(orderModel.UserGuid))
                    {
                        var pointLogModel = new ErpUserPointLog()
                        {
                            Guid = Guid.NewGuid().ToString(),
                            UserGuid =orderModel.UserGuid,
                            OperateGuid= parm.Guid,
                            Types =1,
                            Point= orderGoodsModel.Money!=0?Convert.ToInt32((orderGoodsModel.Money / orderGoodsModel.Counts) / 10):0,
                            Summary="[减少]-退货积分变更"
                        };
                        Db.Insertable(pointLogModel).ExecuteCommand();
                        //用户积分减少
                        Db.Updateable<ErpShopUser>().UpdateColumns(m => m.Points==m.Points - pointLogModel.Point)
                        .Where(m=>m.Guid==orderModel.UserGuid).ExecuteCommand();
                    }
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
                var dbres = ErpBackGoodsDb.Delete(m => list.Contains(m.Guid));
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
        /// 获得一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<ErpBackGoods>> GetByGuidAsync(string parm)
        {
            var model = ErpBackGoodsDb.GetById(parm);
            var res = new ApiResult<ErpBackGoods>
            {
                statusCode = 200,
                data = model ?? new ErpBackGoods() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<BackGoodsDto>>> GetPagesAsync(PageParm parm, SearchParm searchParm)
        {
            var res = new ApiResult<Page<BackGoodsDto>>();
            try
            {
                string beginTime = string.Empty, endTime = string.Empty;
                if (!string.IsNullOrEmpty(parm.time))
                {
                    var timeRes = Utils.SplitString(parm.time, '-');
                    beginTime = timeRes[0].Trim();
                    endTime = timeRes[1].Trim();
                }
                var query = Db.Queryable<ErpBackGoods,ErpGoodsSku,ErpShops,ErpStaff>((ebg,eg,es,est)=>new object[] {
                    JoinType.Left,ebg.GoodsGuid==eg.Guid,
                    JoinType.Left,ebg.ShopGuid==es.Guid,
                    JoinType.Left,ebg.AdminGuid==est.Guid})
                        .WhereIF(!string.IsNullOrEmpty(searchParm.shopGuid), (ebg, eg, es, est) => ebg.ShopGuid == searchParm.shopGuid)
                        .WhereIF(!string.IsNullOrEmpty(searchParm.brank), (ebg, eg, es, est) => eg.BrankGuid == searchParm.brank)
                        .WhereIF(!string.IsNullOrEmpty(parm.key), (ebg, eg, es, est) => ebg.Number == parm.key)
                        .WhereIF(!string.IsNullOrEmpty(parm.time), (ebg, eg, es, est) => ebg.AddDate >= Convert.ToDateTime(beginTime) && ebg.AddDate <= Convert.ToDateTime(endTime))
                        .OrderBy((ebg, eg, es, est) =>ebg.AddDate,OrderByType.Desc)
                        .Select((ebg, eg, es, est) => new BackGoodsDto() {
                            Number=ebg.Guid,
                            Code=eg.Code,
                            OrderNumber=ebg.OrderNumber,
                            BrandName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == eg.BrankGuid).Select(g => g.Name),
                            SeasonName=SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == eg.SeasonGuid).Select(g => g.Name),
                            StyleName=SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == eg.StyleGuid).Select(g => g.Name),
                            ShopName =es.ShopName,
                            Operator=est.TrueName,
                            Mobile=est.Mobile,
                            BackCount =ebg.BackCount,
                            Money=ebg.BackMoney,
                            Status=ebg.Status,
                            Summary =ebg.Summary,
                            AddDate =ebg.AddDate
                        })
                        .OrderByIF(!string.IsNullOrEmpty(parm.field) && !string.IsNullOrEmpty(parm.order), parm.field + " " + parm.order)
                        .ToPage(parm.page, parm.limit);
                res.success = true;
                res.message = "获取成功！";
                res.data =  query;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyStatusAsync(ErpBackGoods parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = (int)ApiEnum.Error };
            try
            {
                //根据退货查询积分变动表里面是否存在用户积分变动信息
                var pointLogModel = ErpUserPointLogDb.GetSingle(m=>m.OperateGuid==parm.Guid);
                var model = ErpBackGoodsDb.GetSingle(m => m.Guid == parm.Guid);
                if (model != null)
                {
                    //查询加盟商库存中，该条商品的信息
                    var shopGoods = ErpShopSkuDb.GetSingle(m => m.ShopGuid == model.ShopGuid && m.SkuGuid == model.GoodsGuid);
                    if (model.Status == 1)
                    {
                        //如果状态修改为非正常   需要减少加盟商库存
                        model.Status = 2;
                        //需要判断加盟商库存是否足够
                        if (shopGoods.Stock < model.BackCount)
                        {
                            res.message = "加盟商库存不足！";
                            return await Task.Run(() => res);
                        }
                        //加盟商减少
                        shopGoods.Stock -= model.BackCount;
                    }
                    else
                    {
                        //如果状态修改为正常   需要增加加盟商库存
                        model.Status = 1;
                        //加盟商增加
                        shopGoods.Stock += model.BackCount;
                    }

                    var result = Db.Ado.UseTran(() =>
                    {
                        //修改加盟商信息
                        Db.Updateable(shopGoods).ExecuteCommand();
                        //修改返货商品
                        Db.Updateable(model).ExecuteCommand();
                        //如果用户存在积分变更，则根据状态，修改积分变更数值
                        if (pointLogModel!=null)
                        {
                            if (model.Status == 1)
                            {
                                Db.Updateable<ErpShopUser>().UpdateColumns(m => m.Points == m.Points+pointLogModel.Point).Where(m=>m.Guid==pointLogModel.UserGuid).ExecuteCommand();
                            }
                            else
                            {
                                Db.Updateable<ErpShopUser>().UpdateColumns(m => m.Points == m.Points-pointLogModel.Point).Where(m => m.Guid == pointLogModel.UserGuid).ExecuteCommand();
                            }
                        }
                    });
                    if (!result.IsSuccess)
                    {
                        res.message = result.ErrorMessage;
                    }
                    res.statusCode = (int)ApiEnum.Status;

                }
                else
                {
                    res.message = "没有查询到该条数据~";
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }
    }
}
