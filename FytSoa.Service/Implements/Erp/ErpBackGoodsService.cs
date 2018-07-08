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
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //判断是否存在
                var isExt = ErpBackGoodsDb.IsAny(m => m.ShopGuid == parm.ShopGuid && m.GoodsGuid==parm.GoodsGuid && m.OrderNumber==parm.OrderNumber);
                if (isExt)
                {
                    res.statusCode = (int)ApiEnum.ParameterError;
                    res.message = "该退货信息已存在~";
                }
                else
                {
                    var dbres = ErpBackGoodsDb.Insert(parm);
                    if (!dbres)
                    {
                        res.statusCode = (int)ApiEnum.Error;
                        res.message = "插入数据失败~";
                    }
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
        public async Task<ApiResult<Page<BackGoodsDto>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<BackGoodsDto>>();
            try
            {
                var query = Db.Queryable<ErpBackGoods,ErpGoodsSku,ErpShops,ErpStaff>((ebg,eg,es,est)=>new object[] {
                    JoinType.Left,ebg.GoodsGuid==eg.Guid,
                    JoinType.Left,es.Guid==ebg.ShopGuid,
                    JoinType.Left,ebg.AdminGuid==est.Guid})
                        .WhereIF(!string.IsNullOrEmpty(parm.guid), (ebg, eg, es, est) => ebg.ShopGuid == parm.guid)
                        .WhereIF(!string.IsNullOrEmpty(parm.key), (ebg, eg, es, est) => ebg.Number == parm.key || ebg.GoodsGuid == parm.key)
                        .Select((ebg, eg, es, est) => new BackGoodsDto() {
                            Code=eg.Code,
                            OrderNumber=ebg.OrderNumber,
                            GoodsName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == eg.BrankGuid).Select(g => g.Name) +
                            SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == eg.SeasonGuid).Select(g => g.Name) +
                            SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == eg.StyleGuid).Select(g => g.Name),
                            ShopName =es.ShopName,
                            Operator=est.TrueName,
                            Mobile=est.Mobile,
                            BackCount =ebg.BackCount,
                            Money=ebg.BackMoney,
                            Summary=ebg.Summary,
                            AddDate =ebg.AddDate
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
        public async Task<ApiResult<string>> ModifyAsync(ErpBackGoods parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var dbres = ErpBackGoodsDb.Update(parm);
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
    }
}
