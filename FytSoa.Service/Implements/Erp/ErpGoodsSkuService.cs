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
    /// 商品条形码服务接口实现
    /// </summary>
    public class ErpGoodsSkuService : DbContext, IErpGoodsSkuService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpGoodsSku parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //判断是否存在
                var isExt = ErpGoodsSkuDb.IsAny(m => m.Code == parm.Code);
                if (isExt)
                {
                    res.statusCode = (int)ApiEnum.ParameterError;
                    res.message = "该条形码已存在~";
                }
                else
                {
                    //构建Code
                    var codeType = SysCodeTypeDb.GetList(m => m.ParentGuid == "8d3158d6-e179-4046-99e9-53eb8c04ddb1").Select(m => m.Guid).ToList();
                    var codeList = SysCodeDb.GetList(m => codeType.Contains(m.ParentGuid));
                    parm.Code = codeList.Find(m => m.Guid == parm.YearGuid).CodeType + codeList.Find(m => m.Guid == parm.BrankGuid).CodeType
                        + codeList.Find(m => m.Guid == parm.SeasonGuid).CodeType
                        + codeList.Find(m => m.Guid == parm.StyleGuid).CodeType
                        + codeList.Find(m => m.Guid == parm.BatchGuid).CodeType
                        + codeList.Find(m => m.Guid == parm.SizeGuid).CodeType
                        + parm.SalePrice
                        +"-"+ parm.DisPrice;

                    //构建商品标题
                    var brankName = codeList.Find(m => m.Guid == parm.BrankGuid).Name;
                    var styleName = codeList.Find(m => m.Guid == parm.StyleGuid).Name;
                    var goodsName = brankName + codeList.Find(m => m.Guid == parm.SeasonGuid).Name + styleName;

                    //构建商品信息
                    var goodsModel = new ErpGoods()
                    {
                        Guid = Guid.NewGuid().ToString(),
                        Title = goodsName,
                        Brank = brankName,
                        Style = styleName
                    };

                    parm.GoodsGuid = goodsModel.Guid;
                    parm.Guid = Guid.NewGuid().ToString();

                    //事务保证数据一致性
                    Db.Ado.BeginTran();
                    var goodsres = ErpGoodsDb.Insert(goodsModel);
                    if (goodsres)
                    {
                        var dbres = ErpGoodsSkuDb.Insert(parm);
                        if (!dbres)
                        {
                            res.statusCode = (int)ApiEnum.Error;
                            res.message = "插入数据失败~";
                        }
                    }
                    Db.Ado.CommitTran();
                }
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
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
                var dbres = ErpGoodsSkuDb.Update(m => new ErpGoodsSku() { IsDel = true }, m => list.Contains(m.Guid));
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
        public async Task<ApiResult<ErpGoodsSku>> GetByGuidAsync(string parm)
        {
            var model = ErpGoodsSkuDb.GetSingle(m=>m.Code==parm);
            var res = new ApiResult<ErpGoodsSku>
            {
                statusCode = 200,
                data = model ?? new ErpGoodsSku() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 获得一条数据,根据店铺，和出库信息
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<GoodsSkuDto>> GetByCodeAsync(string shopGuid, string code)
        {
            var res = new ApiResult<GoodsSkuDto>();
            try
            {
                var model = Db.Queryable<ErpGoodsSku, ErpInOutLog>((egs, elog) => new object[] {
                JoinType.Left,egs.Guid==elog.GoodsGuid
            })
                .Where((egs, elog) => egs.Code == code && elog.ShopGuid == shopGuid && elog.Types == 2 && elog.GoodsSum > 0)
                .Select((egs, elog) => new GoodsSkuDto()
                {
                    Guid = egs.Guid,
                    Code = egs.Code,
                    BrankName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.BrankGuid).Select(g => g.Name),
                    StyleName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.StyleGuid).Select(g => g.Name),
                    SalePrice = egs.SalePrice,
                    DisPrice = egs.DisPrice,
                    StockSum = elog.GoodsSum
                }).First();
                //出库  库存=返货+销售的数量
                var saleStock = Db.Queryable<ErpSaleOrderGoods, ErpGoodsSku>((eso, egs) => new object[] { JoinType.Left, eso.GoodsGuid == egs.Guid })
                    .Where((eso, egs) => eso.ShopGuid == shopGuid && egs.Code == code)
                    .Sum((eso, egs) => eso.Counts);
                var returnStock = Db.Queryable<ErpReturnGoods, ErpGoodsSku>((eso, egs) => new object[] { JoinType.Left, eso.GoodsGuid == egs.Guid })
                    .Where((eso, egs) => eso.ShopGuid == shopGuid && egs.Code == code)
                    .Sum((eso, egs) => eso.ReturnCount);
                model.StockSum = model.StockSum - (saleStock + returnStock);
                res.data = model;

            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
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
                var query = Db.Queryable<ErpGoodsSku>()
                        .Where(m => !m.IsDel)
                        .WhereIF(parm.types == 1, m => m.StockSum > 0)
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.Code.Contains(parm.key))
                        .WhereIF(!string.IsNullOrEmpty(parm.guid), m => m.BrankGuid == parm.guid)
                        .Select(m => new GoodsSkuDto()
                        {
                            Guid = m.Guid,
                            Code = m.Code,
                            BrankName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == m.BrankGuid).Select(g => g.Name),
                            StyleName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == m.StyleGuid).Select(g => g.Name),
                            SeasonName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == m.SeasonGuid).Select(g => g.Name),
                            SalePrice = m.SalePrice,
                            DisPrice = m.DisPrice,
                            StockSum = m.StockSum,
                            SaleSum = m.SaleSum,
                            AddDate = m.AddDate
                        })
                        .OrderBy(m => m.AddDate, OrderByType.Desc).ToPageAsync(parm.page, parm.limit);
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
        public async Task<ApiResult<string>> ModifyAsync(ErpGoodsSku parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //构建Code
                var codeType = SysCodeTypeDb.GetList(m => m.ParentGuid == "8d3158d6-e179-4046-99e9-53eb8c04ddb1").Select(m => m.Guid).ToList();
                var codeList = SysCodeDb.GetList(m => codeType.Contains(m.ParentGuid));
                parm.Code = codeList.Find(m => m.Guid == parm.BrankGuid).CodeType
                    + codeList.Find(m => m.Guid == parm.SeasonGuid).CodeType
                    + codeList.Find(m => m.Guid == parm.StyleGuid).CodeType
                    + codeList.Find(m => m.Guid == parm.BatchGuid).CodeType
                    + codeList.Find(m => m.Guid == parm.SizeGuid).CodeType
                    + parm.SalePrice
                    + parm.DisPrice;

                //构建商品标题
                var brankName = codeList.Find(m => m.Guid == parm.BrankGuid).Name;
                var styleName = codeList.Find(m => m.Guid == parm.StyleGuid).Name;
                var goodsName = brankName + codeList.Find(m => m.Guid == parm.SeasonGuid).Name + styleName;

                //构建商品信息
                var goodsModel = new ErpGoods()
                {
                    Guid = parm.GoodsGuid,
                    Title = goodsName,
                    Brank = brankName,
                    Style = styleName
                };
                //判断是否存在
                var isExt = ErpGoodsSkuDb.IsAny(m => m.Code == parm.Code && m.Guid != parm.Guid);
                if (isExt)
                {
                    res.statusCode = (int)ApiEnum.ParameterError;
                    res.message = "该条形码已存在~";
                }
                else
                {
                    //事务保证数据一致性
                    Db.Ado.BeginTran();
                    var goodsRes = ErpGoodsDb.Update(goodsModel);
                    if (goodsRes)
                    {
                        var dbres = ErpGoodsSkuDb.Update(parm);
                        if (!dbres)
                        {
                            res.statusCode = (int)ApiEnum.Error;
                            res.message = "修改数据失败~";
                        }
                    }
                    Db.Ado.CommitTran();
                }
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }
    }
}
