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
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class RoleMenuController : Controller
    {
        private readonly ISysPermissionsService _roleMenu;
        public RoleMenuController(ISysPermissionsService roleMenu)
        {
            _roleMenu = roleMenu;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("list"), Log("RoleMenu：list", LogType = LogEnum.RETRIEVE)]
        public async Task<JsonResult> GetPages(string key)
        {
            var res = await _roleMenu.GetListAsync(key);
            return Json(new { code = 0, msg = "success", count = 100, res.data });
        }

        /// <summary>
        /// 角色授权菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("add"), ApiAuthorize(Modules = "Role", Methods = "Authorize", LogType = LogEnum.AUTHORIZE)]
        public async Task<ApiResult<string>> SaveRoleMenu(SysPermissions parm)
        {
            return await _roleMenu.SaveAsync(parm);
        }

        /// <summary>
        /// 角色授权菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("add/authorization"), ApiAuthorize(Modules = "Role", Methods = "Authorize", LogType = LogEnum.AUTHORIZE)]
        public async Task<ApiResult<string>> SaveAuthorization(List<SysMenuDto> list,string roleGuid)
        {
            return await _roleMenu.SaveAuthorization(list,roleGuid);
        }

        /// <summary>
        /// 用户授权角色
        /// </summary>
        /// <returns></returns>
        [HttpPost("torole"), ApiAuthorize(Modules = "Admin", Methods = "Authorize", LogType = LogEnum.AUTHORIZE)]
        public async Task<ApiResult<string>> AdminToRole(SysPermissions parm,bool status)
        {
            return await _roleMenu.ToRoleAsync(parm,status);
        }

        /// <summary>
        /// 菜单授权-菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost("tubtnfun"), ApiAuthorize(Modules = "Role", Methods = "Authorize", LogType = LogEnum.AUTHORIZE)]
        public async Task<ApiResult<string>> RoleMenuToFun(SysPermissionsParm parm)
        {
            return await _roleMenu.RoleMenuToFunAsync(parm);
        }
    }
}