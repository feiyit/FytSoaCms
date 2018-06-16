using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RoleMenuController : Controller
    {
        private readonly ISysRoleMenuService _roleMenu;
        public RoleMenuController(ISysRoleMenuService roleMenu)
        {
            _roleMenu = roleMenu;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<JsonResult> GetPages(string key)
        {
            var res = await _roleMenu.GetListAsync(key);
            return Json(new { code = 0, msg = "success", count = 100, res.data });
        }

        /// <summary>
        /// 角色授权菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ApiResult<string>> SaveRoleMenu(SysRoleMenu parm)
        {
            return await _roleMenu.SaveAsync(parm);
        }

        /// <summary>
        /// 用户授权角色
        /// </summary>
        /// <returns></returns>
        [HttpPost("torole")]
        public async Task<ApiResult<string>> AdminToRole(SysRoleMenu parm,bool status)
        {
            return await _roleMenu.AdminToRoleAsync(parm,status);
        }
    }
}