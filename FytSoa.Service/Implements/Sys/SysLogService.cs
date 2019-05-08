using FytSoa.Service.Extensions;
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
    public class SysLogService : BaseServer<SysLog>, ISysLogService
    {
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<SysLog>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<SysLog>>();
            try
            {
                string beginTime = string.Empty, endTime = string.Empty;
                if (!string.IsNullOrEmpty(parm.time))
                {
                    var timeRes = Utils.SplitString(parm.time, '-');
                    beginTime = timeRes[0].Trim();
                    endTime = timeRes[1].Trim();
                }
                res.data =await Db.Queryable<SysLog>()
                    .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.User.Contains(parm.key) || m.Logger.Contains(parm.key))
                    .WhereIF(!string.IsNullOrEmpty(parm.where), m => m.Level == parm.where)
                    .WhereIF(!string.IsNullOrEmpty(parm.time), m => m.Logged >= Convert.ToDateTime(beginTime) && m.Logged <= Convert.ToDateTime(endTime))
                    .OrderBy(m => m.Logged, OrderByType.Desc)
                    .Mapper((it, cache) =>
                    {
                        if (!string.IsNullOrEmpty(it.UserName))
                        {
                            it.User = it.UserName;
                        }
                    })
                    .ToPageAsync(parm.page, parm.limit);
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
                Logger.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return res;
        }
    }
}
