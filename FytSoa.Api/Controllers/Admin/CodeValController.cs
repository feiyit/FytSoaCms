using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytIms.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/code")]
    [Authorize(Roles = "Admin")]
    public class CodeValController : Controller
    {
        private readonly ISysCodeService _sysCodeService;
        public CodeValController(ISysCodeService sysCodeService)
        {
            _sysCodeService = sysCodeService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("getpages")]
        public async Task<JsonResult> GetPages(SysCodePostPage request)
        {
            var res= await _sysCodeService.GetPagesAsync(request);
            return Json(new { code = 0, msg = "success", count = res.data.TotalItems, data = res.data.Items });
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ApiResult<string>> AddCodeType(SysCode parm)
        {
            return await _sysCodeService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<ApiResult<string>> DeleteCode(string parm)
        {
            return await _sysCodeService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<ApiResult<string>> EditCode(SysCode parm)
        {
            return await _sysCodeService.ModifyAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("editstatus")]
        public async Task<ApiResult<string>> EditStatusCode(SysCode parm)
        {
            return await _sysCodeService.ModifyStatusAsync(parm);
        }
    }
}