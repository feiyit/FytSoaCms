using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Erp;
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
    /// 返货报损记录，服务接口实现
    /// </summary>
    public class ErpReturnLossService : DbContext, IErpReturnLossService
    {
        /// <summary>
        /// 添加返货报损记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpReturnLoss parm)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                //判断登录账号是否存在
                parm.Guid = Guid.NewGuid().ToString();
                parm.AddDate = DateTime.Now;
                var dbres = ErpReturnLossDb.Insert(parm);
                if (!dbres)
                {
                    res.message = "插入数据失败~";
                }
                else
                {
                    res.statusCode = (int)ApiEnum.Status;
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 删除一条或者多条返货报损记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var res = new ApiResult<string>() { statusCode = 200 };
            try
            {
                var list = Utils.StrToListString(parm);
                var dbres = ErpReturnLossDb.Delete(m => list.Contains(m.Guid));
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
        /// 根据编号获得返货报损记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<ErpReturnLoss>> GetByGuidAsync(string parm)
        {
            var model = ErpReturnLossDb.GetById(parm);
            var res = new ApiResult<ErpReturnLoss>
            {
                statusCode = 200,
                data = model ?? new ErpReturnLoss() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页获得返货报损记录
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="searchParm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<ErpReturnLoss>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<ErpReturnLoss>>();
            try
            {
                var query = Db.Queryable<ErpReturnLoss>()
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.CodeName.Contains(parm.key) || m.Summary.Contains(parm.key))
                        .OrderBy(m=>m.AddDate,OrderByType.Desc)
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
        /// 修改一条返货报损记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyStatusAsync(ErpReturnLoss parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                ErpReturnLossDb.Update(m => new ErpReturnLoss()
                {
                    CodeName=parm.CodeName,
                    Count=parm.Count,
                    Summary=parm.Summary
                }, m => m.Guid == parm.Guid);
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
