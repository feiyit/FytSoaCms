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

namespace FytSoa.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Role")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly ISysRoleService _roleService;
        public RoleController(ISysRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("getpages")]
        public async Task<JsonResult> GetPages(PageParm parm)
        {
            var res = await _roleService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data.TotalItems, data = res.data.Items });
        }

        /// <summary>
        /// 查询授权列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("torolelist")]
        public async Task<JsonResult> GetToRolePages(string key,string adminGuid)
        {
            var res = await _roleService.GetPagesToRoleAsync(key,adminGuid);
            return Json(new { code = 0, msg = "success", count = res.data.TotalItems, data = res.data.Items });
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ApiResult<string>> AddRole(SysRole parm)
        {
            return await _roleService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<ApiResult<string>> DeleteRole(string parm)
        {
            return await _roleService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<ApiResult<string>> EditRole(SysRole parm)
        {
            return await _roleService.ModifyAsync(parm);
        }
    }
}