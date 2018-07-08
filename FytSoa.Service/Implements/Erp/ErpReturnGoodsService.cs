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
        public async Task<ApiResult<string>> AddAsync(ErpReturnGoods parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var dbres = ErpReturnGoodsDb.Insert(parm);
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
        public async Task<ApiResult<Page<ReturnGoodsDto>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<ReturnGoodsDto>>();
            try
            {
                var query = Db.Queryable<ErpReturnGoods,ErpGoodsSku>((erg,egs)=>new object[] {JoinType.Left,erg.GoodsGuid==egs.Guid })
                        .WhereIF(!string.IsNullOrEmpty(parm.guid), (erg, egs) => erg.OrderGuid == parm.guid)
                        .Select((erg, egs)=>new ReturnGoodsDto() {
                            Code=egs.Code,
                            GoodsName= SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.BrankGuid).Select(g => g.Name)+
                            SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.SeasonGuid).Select(g => g.Name)+
                            SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.StyleGuid).Select(g => g.Name),
                            Counts = erg.ReturnCount,
                            Summary=erg.Summary
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
    }
}
