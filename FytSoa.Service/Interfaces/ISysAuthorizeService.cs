using FytSoa.Common;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /// <summary>
    /// 根据登录账号，获得相应权限服务接口
    /// </summary>
    public interface ISysAuthorizeService
    {
        /// <summary>
        /// 根据登录账号，获得相应权限服务接口
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<SysMenu>>> GetAuthorizeAsync(string admin);
    }
}
