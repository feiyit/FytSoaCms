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
    /// 系统菜单业务接口
    /// </summary>
    public interface ISysMenuService : IBaseServer<SysMenu>
    {
        /// <summary>
        /// 获得菜单列表，提供给权限管理，根据角色查询所有菜单
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<SysMenuDto>>> GetMenuByRole(string role);

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<SysMenu>>> GetPagesAsync(PageParm parm);

        /// <summary>
        /// 获得树列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<SysMenuTree>>> GetListTreeAsync(string roleGuid);

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<SysMenu>> GetByGuidAsync(string parm);

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> AddAsync(SysMenu parm,List<string> btnfun);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> ModifyAsync(SysMenu parm, List<string> btnfun);
    }
}
