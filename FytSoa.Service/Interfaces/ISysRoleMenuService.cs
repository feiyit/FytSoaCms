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
    public interface ISysRoleMenuService
    {
        /// <summary>
        /// 保存角色菜单信息
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> SaveAsync(SysRoleMenu parm);

        /// <summary>
        /// 用户授权角色
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> AdminToRoleAsync(SysRoleMenu parm, bool status);

        /// <summary>
        /// 保存角色菜单信息
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<SysRoleMenu>>> GetListAsync(string roleGuid);
    }
}
