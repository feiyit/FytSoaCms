using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserReportController : Controller
    {
        private readonly IUserReportServer _reportService;
        public UserReportController(IUserReportServer reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// 查询12个月，每个月的注册统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("regreport")]
        public async Task<ApiResult<List<UserRegReport>>> GetUserRegReport(PageParm parm)
        {
            return await _reportService.GetUserRegReport(parm);
        }

        /// <summary>
        /// 用户注册按性别统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("sexreport")]
        public async Task<ApiResult<List<UserRegReport>>> GetUserSexRegReport(PageParm parm)
        {
            return await _reportService.GetUserSexRegReport(parm);
        }
    }
}