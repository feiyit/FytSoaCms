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
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        new Task<ApiResult<Page<SysMenu>>> GetPagesAsync(PageParm parm);

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
        new Task<ApiResult<string>> AddAsync(SysMenu parm);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> ModifyAsync(SysMenu parm);
    }
}
