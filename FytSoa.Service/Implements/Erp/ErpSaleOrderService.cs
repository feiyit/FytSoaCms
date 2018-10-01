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
                    var shopStockSum = Db.Queryable<ErpShopSku>()
                        .Where(m => m.ShopGuid == parm.ShopGuid && m.SkuGuid == item.GoodsGuid).First();
                    if (shopStockSum.Stock < item.Counts)
                    {
                        isStockSuccess = false;
                    }
                }
                if (!isStockSuccess)
                {
                    res.message = "商品库存数量";
                    return await Task.Run(() => res);
                }

                //获得商品的所有id
                var goodIds = roGoodsList.Select(m => m.GoodsGuid).ToList();
                //根据商品获得列表
                var goodList = ErpGoodsSkuDb.GetList(m => goodIds.Contains(m.Guid));
                //根据店铺获得加盟商的库存信息
                var shopGoodsSkuList = ErpShopSkuDb.GetList(m=>m.ShopGuid==parm.ShopGuid && goodIds.Contains(m.SkuGuid));
                //满减变量
                var fullJson = new List<ShopActivity>();
                //根据活动编号，查询活动详情，处理对应金额
                ErpShopActivity activityModel = null;
                if (!string.IsNullOrEmpty(parm.ActivityName))
                {
                    //查询活动
                    activityModel = ErpShopActivityDb.GetById(parm.ActivityName);
                    if (activityModel != null)
                    {
                        parm.ActivityGuid = parm.ActivityName;
                        parm.ActivityName = Utils.GetActivityMethod(activityModel.Method);
                        parm.ActivityTypes = Utils.GetActivityTypes(activityModel.Method);
                        if (!string.IsNullOrEmpty(activityModel.FullBack))
                        {
                            //满减======序列号满减对象
                            fullJson = JsonConvert.DeserializeObject<List<ShopActivity>>(activityModel.FullBack);
                        }
                    }
                }
                //根据活动算好订单金额，如果有活动，满减或者打折，最终金额会根据活动而变
                var roIndex = 0; //定义一个循环的标记
                foreach (var item in roGoodsList)
                {
                    roIndex++;
                    var skuItem = goodList.Find(m=>m.Guid==item.GoodsGuid);
                    //增加到GoodsSku的销售数量
                    goodList.Find(m => m.Guid == item.GoodsGuid).SaleSum += item.Counts;
                    //修改加盟商条形码表中的库存减少，  销售数量增加
                    var shopSkuModel = shopGoodsSkuList.Find(m => m.SkuGuid == skuItem.Guid);
                    shopGoodsSkuList.Find(m => m.SkuGuid == item.GoodsGuid).Stock = shopSkuModel.Stock - item.Counts;
                    //修改加盟商条形码表中的销售数量   增加
                    shopGoodsSkuList.Find(m => m.SkuGuid == item.GoodsGuid).Sale = shopSkuModel.Sale + item.Counts;
                    //获得商品原价*购买商品的数量
                    parm.Money += Convert.ToDecimal(skuItem.SalePrice) * item.Counts;
                    //如果没有活动直接结算金额
                    if (activityModel==null)
                    {
                        parm.RealMoney+= Convert.ToInt32(skuItem.SalePrice) * item.Counts;
                        item.Money = Convert.ToInt32(skuItem.SalePrice);
                    }
                    //整除销售计算价格，残次品价格是前端传过来的
                    if (activityModel != null && parm.SaleType == 1)
                    {
                        //按品牌——这里面只处理打折的，并且是按品牌的
                        if (activityModel.Method == 1 && activityModel.Types == 2)
                        {
                            if (skuItem.BrankGuid == activityModel.BrandGuid)
                            {
                                //品牌打折
                                var tempMoney = Convert.ToDecimal(skuItem.SalePrice) * item.Counts;
                                tempMoney = tempMoney * (Convert.ToDecimal(activityModel.CountNum) / 100);
                                parm.RealMoney += tempMoney;
                                item.Money = parm.RealMoney;
                            }
                            else
                            {
                                //不是该品牌部打折
                                parm.RealMoney += Convert.ToDecimal(skuItem.SalePrice) * item.Counts;
                                item.Money = parm.RealMoney;
                            }
                        }
                        //按品牌——满减
                        else if (activityModel.Method == 2 && activityModel.Types == 2)
                        {
                            //查询该品牌价格是否满足满减需要
                            var brankMoney = goodList.Where(m => m.BrankGuid == activityModel.BrandGuid).Sum(m=>Convert.ToInt32(m.SalePrice)*item.Counts);
                            var brankCount = goodList.Count(m => m.BrankGuid == activityModel.BrandGuid);
                            //是否满足品牌打折的要求
                            var isBrankOk = false;
                            //如果满足打折要求，满减值
                            var fullMoney = 0; 
                            //循环判断符合满减对象
                            foreach (var fullItem in fullJson.OrderByDescending(m => m.fullbegin).ToList())
                            {
                                if (brankMoney >= fullItem.fullbegin)
                                {
                                    isBrankOk = true;
                                    fullMoney = fullItem.fullend;
                                    break;
                                }
                            }
                            //满足打折需求——根据品牌计算单个商品的价格  品牌平均分配价格
                            if (skuItem.BrankGuid == activityModel.BrandGuid && isBrankOk)
                            {
                                //单个商品的金额减满减值/品牌个数=订单详情商品的金额
                                item.Money = Convert.ToDecimal(skuItem.SalePrice)*item.Counts- Convert.ToDecimal(fullMoney / brankCount);
                                parm.RealMoney += item.Money;
                            }
                            else
                            {
                                item.Money = Convert.ToDecimal(skuItem.SalePrice) * item.Counts;
                                parm.RealMoney += item.Money;
                            }
                        }
                        //按品牌——买一增一
                        else if (activityModel.Method == 3 && activityModel.Types == 2)
                        {
                            //按品牌买一增一
                            if (skuItem.BrankGuid == activityModel.BrandGuid)
                            {
                                if (roIndex == 1)
                                {
                                    item.Money = Convert.ToDecimal(skuItem.SalePrice) * item.Counts;
                                    parm.RealMoney += item.Money;
                                }
                                else
                                {
                                    item.Money = 0;
                                }
                            }
                            else
                            {
                                //不是该品牌，不参与活动   既然后是品牌买一赠一活动，实际上不应该出现非品牌的商品出现
                                item.Money = Convert.ToDecimal(skuItem.SalePrice) * item.Counts;
                                parm.RealMoney += item.Money;
                            }
                        }
                        //按全部店铺——打折
                        else if (activityModel.Method == 1 && activityModel.Types != 2)
                        {
                            //全部商铺，也就是所有金额
                            var zhVal = Convert.ToDecimal(activityModel.CountNum) / 100;
                            item.Money = (Convert.ToDecimal(skuItem.SalePrice)*item.Counts) * zhVal;
                        }
                        //按全部店铺——满减
                        else if (activityModel.Method == 2 && activityModel.Types != 2)
                        {
                            var allMoney = goodList.Sum(m => Convert.ToInt32(m.SalePrice) * item.Counts);
                            var allCount = goodList.Count();
                            //是否满足品牌打折的要求
                            var isBrankOk = false;
                            //如果满足打折要求，满减值
                            var fullMoney = 0;
                            //循环判断符合满减对象
                            foreach (var fullItem in fullJson.OrderByDescending(m => m.fullbegin).ToList())
                            {
                                if (allMoney >= fullItem.fullbegin)
                                {
                                    isBrankOk = true;
                                    fullMoney = fullItem.fullend;
                                    break;
                                }
                            }
                            if (isBrankOk)
                            {
                                item.Money = Convert.ToDecimal(skuItem.SalePrice) * item.Counts - Convert.ToDecimal(fullMoney / allCount);
                            }
                        }
                        //按全部店铺——买一赠一
                        else if (activityModel.Method == 3 && activityModel.Types != 2)
                        {
                            item.Money = roIndex == 1 ? Convert.ToDecimal(skuItem.SalePrice) * item.Counts : 0;
                        }
                    }

                }
                //有活动，并且是正常销售的情况下   全部店铺
                if (activityModel!=null && parm.SaleType==1)
                {
                    //计算商品实收金额
                    parm.RealMoney = goodList.Sum(m=>Convert.ToDecimal(m.SalePrice));
                    //====打折/满减
                    if (activityModel.Method == 1 && activityModel.Types != 2)
                    {
                        //全部商铺，也就是所有金额
                        var zhVal = Convert.ToDecimal(activityModel.CountNum) / 100;
                        parm.RealMoney = Convert.ToDecimal(parm.RealMoney * zhVal);
                    }
                    else if(activityModel.Method== 2 && activityModel.Types != 2)
                    {
                        //循环判断符合满减对象
                        foreach (var item in fullJson.OrderByDescending(m=>m.fullbegin).ToList())
                        {
                            if (parm.RealMoney>=item.fullbegin)
                            {
                                parm.RealMoney = parm.RealMoney - item.fullend;
                                break;
                            }
                        }
                    }
                    else if (activityModel.Method== 3 && activityModel.Types != 2)
                    {
                        //买一增一  循环购买商品，只读取第一件，第二个是赠品
                        for (int i = 0; i < roGoodsList.Count; i++)
                        {
                            if (i == 0)
                            {
                                parm.RealMoney = Convert.ToDecimal(goodList.Find(m => m.Guid == roGoodsList[i].GoodsGuid).SalePrice);
                                //给商品明细，第一条商品价格，第二价格为0元，不需要处理
                                roGoodsList[i].Money = parm.RealMoney;
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
                for (int i = 0; i < roGoodsList.Count; i++)
                {
                    var item = roGoodsList[i];
                    item.OrderNumber = parm.Number;
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
                    //修改加盟商商品条形码的销售数量
                    Db.Updateable(shopGoodsSkuList).ExecuteCommand();
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
        /// 获得列表 不包含订单下面的商品列表
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<Page<SaleOrderDto>>> GetPagesNoGoodsAsync(PageParm parm, AppSearchParm searchParm)
        {
            var res = new ApiResult<Page<SaleOrderDto>>();
            try
            {
                string beginTime = string.Empty, endTime = string.Empty;
                if (!string.IsNullOrEmpty(parm.time))
                {
                    var timeRes = Utils.SplitString(parm.time, '-');
                    beginTime = timeRes[0].Trim();
                    endTime = timeRes[1].Trim();
                }
                var query = Db.Queryable<ErpSaleOrder, ErpShops>((eso, es) => new object[] { JoinType.Left, eso.ShopGuid == es.Guid })
                    .WhereIF(!string.IsNullOrEmpty(parm.guid), (eso, es) => eso.ShopGuid == parm.guid)
                    .WhereIF(parm.types != 0, (eso, es) => eso.ActivityTypes == parm.types)
                    .WhereIF(searchParm.saleType != 0, (eso, es) => eso.SaleType == searchParm.saleType)
                    .WhereIF(searchParm.activityTypes != 0, (eso, es) => eso.ActivityTypes == searchParm.activityTypes)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.btime) && !string.IsNullOrEmpty(searchParm.etime),
                    (eso, es) => eso.AddDate >= Convert.ToDateTime(beginTime) && eso.AddDate <= Convert.ToDateTime(endTime))
                    .OrderBy((eso, es) => eso.Number, OrderByType.Desc)
                    .OrderBy((eso, es) => eso.AddDate, OrderByType.Desc)
                    .Select((eso, es) => new SaleOrderDto()
                    {
                        Number = eso.Number,
                        ShopName = es.ShopName,
                        ActivityTypes = SqlFunc.ToString(eso.ActivityTypes),
                        SaleType = SqlFunc.ToString(eso.SaleType),
                        Counts = eso.Counts,
                        ActivityName = eso.ActivityName,
                        Money = eso.Money,
                        RealMoney = eso.RealMoney,
                        AddDate = eso.AddDate
                    })
                    .ToPage(parm.page, parm.limit);
                foreach (var item in query.Items)
                {
                    item.ActivityName = !string.IsNullOrEmpty(item.ActivityName) && item.ActivityName!= "undefined" ? item.ActivityName : "无活动";
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
