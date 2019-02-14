using System;
using System.Threading.Tasks;
using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using SqlSugar;

namespace FytSoa.Service.Implements
{
    /// <summary>
    /// 字典值实现
    /// </summary>
    public class SysCodeService : DbContext, ISysCodeService
    {
        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<SysCode>> GetByGuidAsync(string parm)
        {
            var model = SysCodeDb.GetSingle(m => m.Guid == parm);
            var res = new ApiResult<SysCode>
            {
                statusCode = 200,
                data = model != null ? model : new SysCode() { }
            };
            if (model == null)
            {
                var pmdel = Db.Queryable<SysCode>().OrderBy(m => m.Sort, OrderByType.Desc).First();
                res.data = new SysCode() { Status = true, Sort = pmdel?.Sort + 1 ?? 1 };
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(SysCode parm)
        {
            var res = new ApiResult<string>() {statusCode = 200 };
            try
            {
                //判断是否存在
                var isExt = SysCodeDb.IsAny(m => m.Name == parm.Name);
                if (isExt)
                {
                    res.statusCode = (int)ApiEnum.ParameterError;
                    res.message = "该名称已存在~";
                }
                else
                {
                    parm.Guid = Guid.NewGuid().ToString();
                    var dbres = SysCodeDb.Insert(parm);
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
        /// 删除一条记录
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var list = Utils.StrToListString(parm);
            var isok = SysCodeDb.Delete(m => list.Contains(m.Guid));
            var res = new ApiResult<string>
            {
                statusCode = isok ? (int)ApiEnum.Status : (int)ApiEnum.Error,
                data = isok ? "1" : "0",
                message = isok ? "删除成功~" : "删除失败~"
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 查询列表，根据条件
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<SysCode>>> GetPagesAsync(SysCodePostPage parm)
        {
            var res = new ApiResult<Page<SysCode>>();
            try
            {
                using (Db)
                {
                    var query = Db.Queryable<SysCode>()
                        .WhereIF(!string.IsNullOrEmpty(parm.guid), m => m.ParentGuid == parm.guid)
                        .OrderBy(m => m.Sort).ToPageAsync(parm.page, parm.limit);
                    res.success = true;
                    res.message = "获取成功！";
                    res.data = await query;
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 修改一条记录
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyAsync(SysCode parm)
        {
            var isok = SysCodeDb.Update(
                m => new SysCode()
                {
                    Name = parm.Name,
                    CodeType = parm.CodeType,
                    Summary = parm.Summary,
                    Status = parm.Status,
                    EditTime = DateTime.Now
                }, m => m.Guid == parm.Guid);
            var res = new ApiResult<string>
            {
                success = isok,
                statusCode = isok? (int)ApiEnum.Status : (int)ApiEnum.Error,
                data = isok ? "1" : "0"
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyStatusAsync(SysCode parm)
        {
            var isok = SysCodeDb.Update(
                m => new SysCode()
                {
                    Status = parm.Status,
                    EditTime = DateTime.Now
                }, m => m.Guid == parm.Guid);
            var res = new ApiResult<string>
            {
                success = isok,
                statusCode = isok ? (int)ApiEnum.Status : (int)ApiEnum.Error,
                data = isok ? "1" : "0"
            };
            return await Task.Run(() => res);
        }
    }
}
