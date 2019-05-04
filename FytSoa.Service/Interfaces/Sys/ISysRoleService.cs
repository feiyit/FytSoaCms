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
    /// 角色业务接口
    /// </summary>
    public interface ISysRoleService
    {
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<SysRole>>> GetPagesAsync(PageParm parm);

        /// <summary>
        /// 查询列表，并获得权限值状态
        /// </summary>
        /// <param name="key">父级</param>
        /// <param name="adminGuid">用户的唯一编号</param>
        /// <returns></returns>
        Task<ApiResult<Page<SysRoleDto>>> GetPagesToRoleAsync(string key, string adminGuid);

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<SysRole>> GetByGuidAsync(string parm);

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> AddAsync(SysRole parm);

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> DeleteAsync(string parm);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> ModifyAsync(SysRole parm);
    }
}
