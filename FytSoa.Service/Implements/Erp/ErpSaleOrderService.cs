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
                DateTime dayTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString() + " 00:00:00");
                parm.Guid = Guid.NewGuid().ToString();
                //判断销售订单字符串是否为空
                if (string.IsNullOrEmpty(goodsJson))
                {
                    res.message = "销售订单里面的商品不能为空~";
                    return await Task.Run(() => res);
                }
                //判断用户信息存在，如果存在需要根据用户查询编号
                ErpShopUser userModel = null;
                if (!string.IsNullOrEmpty(parm.UserGuid))
                {
                    //判断是否存在
                    userModel = ErpShopUserDb.GetSingle(m=>m.Mobile==parm.UserGuid);
                    if (userModel==null)
                    {
                        res.message = "会员不存在~";
                        return await Task.Run(() => res);
                    }
                    parm.UserGuid = userModel.Guid;
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
                    //出库  库存=返货+销售的数量
                    var saleStock= Db.Queryable<ErpSaleOrderGoods>()
                        .Where(m => m.ShopGuid == parm.ShopGuid && m.GoodsGuid == item.GoodsGuid)
                        .Sum(m => m.Counts);
                    var returnStock = Db.Queryable<ErpReturnGoods>()
                        .Where(m => m.ShopGuid == parm.ShopGuid && m.GoodsGuid == item.GoodsGuid)
                        .Sum(m => m.ReturnCount);
                    var stockSum = shopStockSum - (saleStock + returnStock);
                    if (stockSum < item.Counts)
                    {
                        isStockSuccess = false;
                    }
                }
                if (!isStockSuccess)
                {
                    res.message = "商品库存数量";
                    return await Task.Run(() => res);
                }

                //根据活动编号，查询活动详情，处理对应金额
                ErpShopActivity activityModel = null;
                if (!string.IsNullOrEmpty(parm.ActivityName))
                {
                    //查询活动
                    activityModel = ErpShopActivityDb.GetById(parm.ActivityName);
                    if (activityModel != null)
                    {
                        parm.ActivityGuid = parm.ActivityName;
                        parm.ActivityName = activityModel.Method == 1 ? "打折" : "满减";
                        parm.ActivityTypes = activityModel.Method == 1 ? Convert.ToByte(2) : Convert.ToByte(3);
                    }
                }
                //根据活动算好订单金额，如果有活动，满减或者打折，最终金额会根据活动而变

                //获得商品的所有id
                var goodIds = roGoodsList.Select(m => m.GoodsGuid).ToList();
                //根据商品获得列表
                var goodList = ErpGoodsSkuDb.GetList(m => goodIds.Contains(m.Guid));
                foreach (var item in goodList)
                {
                    foreach (var roitem in roGoodsList)
                    {
                        if (roitem.GoodsGuid==item.Guid)
                        {
                            //修改商品的销售数量
                            item.SaleSum += roitem.Counts;
                            //获得商品原价*购买商品的数量
                            parm.Money += Convert.ToDecimal(item.SalePrice)*roitem.Counts;
                            //整除销售计算价格，残次品价格是前端传过来的
                            if (parm.SaleType==1)
                            {
                                //这里面只处理打折的，并且是按品牌的
                                if(activityModel!=null && activityModel.Method==1 && activityModel.Types==2)
                                {
                                    if (item.BrankGuid == activityModel.BrandGuid)
                                    {
                                        //品牌打折
                                        var tempMoney = Convert.ToDecimal(item.DisPrice) * roitem.Counts;
                                        tempMoney = tempMoney * (Convert.ToDecimal(activityModel.CountNum) / 100);
                                        parm.RealMoney += tempMoney;
                                    }
                                    else
                                    {
                                        //不是该品牌部打折
                                        parm.RealMoney += Convert.ToDecimal(item.DisPrice) * roitem.Counts;
                                    }
                                }
                                else
                                {
                                    parm.RealMoney += Convert.ToDecimal(item.DisPrice) * roitem.Counts;
                                }
                            }                            
                        }
                    }                    
                }
                if (activityModel!=null && parm.SaleType==1)
                {
                    //====打折/满减
                    if (activityModel.Method == 1)
                    {
                        //打折   实收金额=实收金额*（折扣值/100）
                        if (activityModel.Types == 0)
                        {
                            //全部商铺，也就是所有金额
                            parm.RealMoney = Convert.ToDecimal(parm.RealMoney * (activityModel.CountNum / 100));
                        }                        
                    }
                    else
                    {
                        //满减======序列号满减对象
                        var fullJson = JsonConvert.DeserializeObject<List<ShopActivity>>(activityModel.FullBack);
                        //循环判断符合满减对象
                        foreach (var item in fullJson.OrderByDescending(m=>m.fullbegin).ToList())
                        {
                            if (parm.RealMoney>item.fullbegin)
                            {
                                parm.RealMoney = parm.RealMoney - item.fullend;
                                break;
                            }
                        }
                    }
                }

                
                //根据实付金额，计算积分值
                if (userModel!=null)
                {
                    userModel.Points = Convert.ToInt32(parm.RealMoney / 10);
                }

                //查询今天销售数量
                var dayCount = ErpSaleOrderDb.Count(m => SqlFunc.DateIsSame(m.AddDate, dayTime));
                parm.Number = "SO-" + DateTime.Now.ToString("yyyyMMdd") + "-" + (1001 + dayCount);
                res.data = parm.Number;
                foreach (var item in roGoodsList)
                {
                    item.OrderNumber =  parm.Number;
                    item.Guid = Guid.NewGuid().ToString();
                    item.ShopGuid = parm.ShopGuid;
                }

                var result = Db.Ado.UseTran(() =>
                {                    
                    //添加订单
                    Db.Insertable(parm).ExecuteCommand();
                    //添加订单商品
                    Db.Insertable(roGoodsList).ExecuteCommand();
                    //根据商品修改商品的销售数量
                    Db.Updateable(goodList).ExecuteCommand();
                    if (userModel!=null)
                    {
                        //修改用户积分
                        Db.Updateable<ErpShopUser>().UpdateColumns(m => m.Points == m.Points + userModel.Points).Where(m => m.Guid == userModel.Guid).ExecuteCommand();
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
        /// 查询多条记录
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<Page<SaleOrderDto>>> GetPagesAsync(PageParm parm, AppSearchParm searchParm)
        {
            var res = new ApiResult<Page<SaleOrderDto>>();
            try
            {
                var query = Db.Queryable<ErpSaleOrder,ErpShops>((eso,es)=>new object[] { JoinType.Left,eso.ShopGuid==es.Guid})
                    .WhereIF(!string.IsNullOrEmpty(parm.guid), (eso, es) =>eso.ShopGuid==parm.guid)
                    .WhereIF(parm.types!=0, (eso, es) => eso.ActivityTypes == parm.types)
                    .WhereIF(searchParm.saleType != 0, (eso, es) => eso.SaleType == searchParm.saleType)
                    .WhereIF(searchParm.activityTypes != 0, (eso, es) => eso.ActivityTypes == searchParm.activityTypes)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.btime) && !string.IsNullOrEmpty(searchParm.etime),
                    (eso, es) => eso.AddDate >= Convert.ToDateTime(searchParm.btime) && eso.AddDate <= Convert.ToDateTime(searchParm.etime))
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
                //循环商品订单，查询订单下面的商品
                var orderNumbers = query.Items?.Select(m=>m.Number).ToList();
                var orderGood = ErpSaleOrderGoodsDb.GetList(m => orderNumbers.Contains(m.OrderNumber));
                foreach (var item in query.Items)
                {
                    var list = new List<SaleOrderGoodsDto>();                   
                    foreach (var row in orderGood)
                    {
                        if (item.Number==row.OrderNumber)
                        {
                            var goodSku = ErpGoodsSkuDb.GetById(row.GoodsGuid);
                            list.Add(new SaleOrderGoodsDto()
                            {
                                Counts = row.Counts,
                                Code = goodSku.Code,
                                GoodsName = SysCodeDb.GetById(goodSku.BrankGuid).Name+ SysCodeDb.GetById(goodSku.StyleGuid).Name
                            });
                        }                        
                    }
                    item.Goods = list;
                }
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
        /// 获得一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<SaleOrderApp>> GetByNumberAsync(string parm)
        {
            var model = ErpSaleOrderDb.GetSingle(m=>m.Number==parm);
            var res = new ApiResult<SaleOrderApp>
            {
                statusCode = 200,
                data = null
            };
            res.data = new SaleOrderApp()
            {
                Number = model.Number,
                Counts = model.Counts,
                ActivityName=!string.IsNullOrEmpty(model.ActivityName)?model.ActivityName:"",
                Money = model.RealMoney
            };
            return await Task.Run(() => res);
        }
    }
}
