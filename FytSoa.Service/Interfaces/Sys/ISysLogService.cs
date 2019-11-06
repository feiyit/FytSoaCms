﻿using System;
using FytSoa.Common;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /// <summary>
    /// 系统日志业务接口
    /// </summary>
    public interface ISysLogService : IBaseService<SysLog>
    {
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<SysLog>>> GetPagesAsync(PageParm parm);
        
    }
}
