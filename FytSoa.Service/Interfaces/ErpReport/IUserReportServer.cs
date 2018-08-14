using FytSoa.Common;
using FytSoa.Service.DtoModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /// <summary>
    /// 用户注册统计报表服务接口
    /// </summary>
    public interface IUserReportServer
    {
        /// <summary>
        /// 查询12个月，每个月的注册统计
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        Task<ApiResult<List<UserRegReport>>> GetUserRegReport(PageParm parm);

        /// <summary>
        /// 用户注册按性别统计
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        Task<ApiResult<List<UserRegReport>>> GetUserSexRegReport(PageParm parm);
    }
}
