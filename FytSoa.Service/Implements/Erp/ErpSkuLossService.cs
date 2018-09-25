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
    /// <summary>
    /// 条形码报损服务接口实现
    /// </summary>
    public class ErpSkuLossService : DbContext, IErpSkuLossService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpSkuLoss parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = (int)ApiEnum.Error };
            try
            {
                parm.Guid = Guid.NewGuid().ToString();
                //根据条形码，查询条形码信息
                var skuModel = ErpGoodsSkuDb.GetSingle(m=>m.Code==parm.SkuGuid);
                if (skuModel==null)
                {
                    res.message = "条形码不存在~";
                    return await Task.Run(() => res);
                }
                //判断库存数量是否满足报损数量
                if (skuModel.StockSum<parm.Counts)
                {
                    res.message = "报损数量不能大于库存数量~";
                    return await Task.Run(() => res);
                }
                //减少库存
                skuModel.StockSum -= parm.Counts;
                //事务，保证数据一致性
                var result = Db.Ado.UseTran(() =>
                {
                    //执行添加报损信息
                    Db.Insertable(parm).ExecuteCommand();
                    //修改条形码库存
                    Db.Updateable(skuModel).ExecuteCommand();
                });
                res.statusCode = (int)ApiEnum.Status;
                if (!result.IsSuccess)
                {
                    res.message = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<GoodsSkuDto>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<GoodsSkuDto>>();
            try
            {
                var query = Db.Queryable<ErpSkuLoss, ErpGoodsSku>((esl,egs)=>new object[] {JoinType.Left,esl.SkuGuid==egs.Code })
                    .Where((esl, egs) => !egs.IsDel)
                    .WhereIF(parm.types == 1, (esl, egs) => egs.StockSum > 0)
                    .WhereIF(!string.IsNullOrEmpty(parm.key), (esl, egs) => egs.Code.Contains(parm.key))
                    .WhereIF(!string.IsNullOrEmpty(parm.guid), (esl, egs) => egs.BrankGuid == parm.guid)
                    .OrderByIF(string.IsNullOrEmpty(parm.field) || string.IsNullOrEmpty(parm.order),(esl, egs) => esl.AddDate, OrderByType.Desc)
                    .Select((esl, egs) => new GoodsSkuDto()
                    {
                        Guid = esl.Guid,
                        Code = egs.Code,
                        BrankName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.BrankGuid).Select(g => g.Name),
                        StyleName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.StyleGuid).Select(g => g.Name),
                        SeasonName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.SeasonGuid).Select(g => g.Name),
                        SalePrice = egs.SalePrice,
                        DisPrice = egs.DisPrice,
                        StockSum = egs.StockSum,
                        SaleSum = esl.Counts,
                        AddDate = egs.AddDate
                    })
                    .OrderByIF(!string.IsNullOrEmpty(parm.field) && !string.IsNullOrEmpty(parm.order), parm.field + " " + parm.order)
                    .ToPageAsync(parm.page, parm.limit);
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
        public async Task<ApiResult<string>> ModifyAsync(ErpSkuLoss parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = (int)ApiEnum.Error };
            try
            {
                //根据条形码，查询条形码信息
                var skuModel = ErpGoodsSkuDb.GetSingle(m => m.Code == parm.SkuGuid);
                if (skuModel == null)
                {
                    res.message = "条形码不存在~";
                    return await Task.Run(() => res);
                }
                var oldLoss = ErpSkuLossDb.GetSingle(m=>m.Guid==parm.Guid);
                if (oldLoss==null)
                {
                    res.message = "报损信息不存在~";
                    return await Task.Run(() => res);
                }
                //判断库存数量是否满足报损数量
                if (oldLoss.Counts < parm.Counts)
                {
                    res.message = "数量不能大于原报损数量~";
                    return await Task.Run(() => res);
                }
                //修改原报损数量
                oldLoss.Counts -= parm.Counts;
                //减少库存
                skuModel.StockSum += parm.Counts;
                //事务，保证数据一致性
                var result = Db.Ado.UseTran(() =>
                {
                    //执行添加报损信息
                    Db.Updateable(oldLoss).ExecuteCommand();
                    //修改条形码库存
                    Db.Updateable(skuModel).ExecuteCommand();
                });
                res.statusCode = (int)ApiEnum.Status;
                if (!result.IsSuccess)
                {
                    res.message = result.ErrorMessage;
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
