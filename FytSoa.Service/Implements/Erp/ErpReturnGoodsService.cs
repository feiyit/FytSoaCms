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
    public class ErpReturnGoodsService : DbContext, IErpReturnGoodsService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(List<ErpReturnGoods> parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var dbres = ErpReturnGoodsDb.InsertRange(parm.ToArray());
                if (!dbres)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "插入数据失败~";
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
                var dbres = ErpReturnGoodsDb.Delete(m => list.Contains(m.Guid));
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
        public async Task<ApiResult<ErpReturnGoods>> GetByGuidAsync(string parm)
        {
            var model = ErpReturnGoodsDb.GetById(parm);
            var res = new ApiResult<ErpReturnGoods>
            {
                statusCode = 200,
                data = model ?? new ErpReturnGoods() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<ReturnGoodsDto>>> GetPagesAsync(PageParm parm, SearchParm searchParm)
        {
            var res = new ApiResult<Page<ReturnGoodsDto>>();
            try
            {
                var query = Db.Queryable<ErpReturnGoods,ErpGoodsSku>((erg,egs)=>new object[] {JoinType.Left,erg.GoodsGuid==egs.Guid })
                        .WhereIF(!string.IsNullOrEmpty(parm.guid), (erg, egs) => erg.OrderGuid == parm.guid)
                        .WhereIF(!string.IsNullOrEmpty(searchParm.shopGuid), (erg, egs) => erg.ShopGuid == searchParm.shopGuid)
                        .WhereIF(!string.IsNullOrEmpty(searchParm.brank), (erg, egs) => egs.BrankGuid == searchParm.brank)
                        .Select((erg, egs)=>new ReturnGoodsDto() {
                            Guid=erg.Guid,
                            Code=egs.Code,
                            BrandName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.BrankGuid).Select(g => g.Name),
                            SeasonName=SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.SeasonGuid).Select(g => g.Name),
                            StyleName=SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.StyleGuid).Select(g => g.Name),
                            Counts = erg.ReturnCount,
                            Summary=erg.Summary,
                            Status=erg.Status
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

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyAsync(ErpReturnGoods parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var dbres = ErpReturnGoodsDb.Update(parm);
                if (!dbres)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "修改数据失败~";
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
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyStatusAsync(ErpReturnGoods parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = (int)ApiEnum.Error };
            try
            {
                var model = ErpReturnGoodsDb.GetSingle(m=>m.Guid== parm.Guid);
                if (model!=null)
                {
                    //查询加盟商库存中，该条商品的信息
                    var shopGoods = ErpShopSkuDb.GetSingle(m=>m.ShopGuid==model.ShopGuid && m.SkuGuid== model.GoodsGuid);
                    //查询平台  该条商品的信息
                    var platformGoods = ErpGoodsSkuDb.GetSingle(m=>m.Guid==model.GoodsGuid);
                    //查询返货订单信息
                    var returnOrderModel = ErpReturnOrderDb.GetSingle(m=>m.Guid==model.OrderGuid);
                    if (model.Status==1)
                    {
                        //如果状态修改为非正常   需要增加加盟商库存   减少平台库存
                        model.Status = 2;
                        //加盟商增加
                        shopGoods.Stock += model.ReturnCount;
                        //需要判断平台库存是否足够
                        if (platformGoods.StockSum<model.ReturnCount)
                        {
                            res.message = "平台库存不足！";
                            return await Task.Run(() => res);
                        }
                        //平台库存减少
                        platformGoods.StockSum -= model.ReturnCount;
                        //减少返货订单总数
                        returnOrderModel.GoodsSum -= model.ReturnCount;
                    }
                    else
                    {
                        //如果状态修改为非正常   需要减少加盟商库存  增加平台库存
                        model.Status = 1;
                        //需要判断加盟商库存是否足够
                        if (shopGoods.Stock < model.ReturnCount)
                        {
                            res.message = "加盟商库存不足！";
                            return await Task.Run(() => res);
                        }
                        //加盟商减少
                        shopGoods.Stock -= model.ReturnCount;
                        //平台库存增加
                        platformGoods.StockSum += model.ReturnCount;
                        //增加返货订单总数
                        returnOrderModel.GoodsSum += model.ReturnCount;
                    }
                    
                    var result = Db.Ado.UseTran(() =>
                    {
                        //修改加盟商信息
                        Db.Updateable(shopGoods).ExecuteCommand();
                        //修改平台库存
                        Db.Updateable(platformGoods).ExecuteCommand();
                        //修改返货订单信息
                        Db.Updateable(returnOrderModel).ExecuteCommand();
                        //修改返货商品
                        Db.Updateable(model).ExecuteCommand();
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
