﻿using FytSoa.Common;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /// <summary>
    /// 管理员接口
    /// </summary>
    public interface ISysAdminService : IBaseService<SysAdmin>
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        Task<ApiResult<SysAdminMenuDto>> LoginAsync(SysAdminLogin parm);

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<SysAdmin>>> GetPagesAsync(PageParm parm);

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> AddAsync(SysAdmin parm);

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> DeleteAsync(string parm);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> ModifyAsync(SysAdmin parm);
    }
}
