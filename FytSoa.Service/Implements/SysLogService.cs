using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
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
    /// 系统日志接口实现
    /// </summary>
    public class SysLogService : DbContext, ISysLogService
    {
        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var res = new ApiResult<string>() { data = "ok", statusCode = 200 };
            try
            {
                var list = Utils.StrToListString(parm);
                var isok = SysLogDb.Delete(m => list.Contains(m.Guid));
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<SysLog>>> GetPagesAsync(string key,string time)
        {
            var res = new ApiResult<Page<SysLog>>();
            try
            {
                using (Db)
                {
                    string beginTime = string.Empty, endTime = string.Empty;
                    if (!string.IsNullOrEmpty(time))
                    {
                        var timeRes = Utils.SplitString(time, '-');
                        beginTime = timeRes[0].Trim();
                        endTime = timeRes[1].Trim();
                    }
                    var query = Db.Queryable<SysLog>()
                        .WhereIF(!string.IsNullOrEmpty(key), m => m.LoginName.Contains(key))
                        .WhereIF(!string.IsNullOrEmpty(time), m => m.AddTime>=Convert.ToDateTime(beginTime) && m.AddTime<=Convert.ToDateTime(endTime))
                        .OrderBy(m => m.AddTime).ToPageAsync(1, 15);
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
    }
}
