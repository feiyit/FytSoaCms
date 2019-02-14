using FytSoa.Common;
using FytSoa.Core.Model.Sys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /// <summary>
    /// 角色菜单业务接口
    /// </summary>
    public interface ISysPermissionsService
    {
        /// <summary>
        /// 保存角色菜单信息
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> SaveAsync(SysPermissions parm);

        /// <summary>
        /// 用户授权角色
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> ToRoleAsync(SysPermissions parm, bool status);

        /// <summary>
        /// 保存角色菜单信息
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<SysPermissions>>> GetListAsync(string roleGuid);
    }
}
