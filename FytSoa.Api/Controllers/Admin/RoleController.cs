using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Sys;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Role")]
    [JwtAuthorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly ISysRoleService _roleService;
        public RoleController(ISysRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("getpages")]
        public async Task<IActionResult> GetPages([FromQuery]PageParm parm)
        {
            var res = await _roleService.GetPagesAsync(parm);
            return Ok(new { code = 0, msg = "success", count = res.data.TotalItems, data = res.data.Items });
        }

        /// <summary>
        /// 查询授权列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet("torolelist")]
        public async Task<IActionResult> GetToRolePages([FromQuery]RoleByAdminParam param)
        {
            var res = await _roleService.GetPagesToRoleAsync(param.key, param.adminGuid);
            return Ok(new { code = 0, msg = "success", count = res.data.TotalItems, data = res.data.Items });
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("add"), ApiAuthorize(Modules = "Role", Methods = "Add", LogType = LogEnum.ADD)]
        public async Task<IActionResult> AddRole([FromBody]SysRole parm)
        {
            return Ok(await _roleService.AddAsync(parm));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete"), ApiAuthorize(Modules = "Role", Methods = "Delete", LogType = LogEnum.DELETE)]
        public async Task<IActionResult> DeleteRole([FromBody]ParmString obj)
        {
            return Ok(await _roleService.DeleteAsync(obj.parm));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit"), ApiAuthorize(Modules = "Role", Methods = "Update", LogType = LogEnum.UPDATE)]
        public async Task<IActionResult> EditRole([FromBody]SysRole parm)
        {
            return Ok(await _roleService.ModifyAsync(parm));
        }

        /// <summary>
        /// 根据编号查询角色信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("bymodel")]
        public async Task<IActionResult> GetModelByGuid([FromBody]ParmString parm)
        {
            return Ok(await _roleService.GetModelAsync(m=>m.Guid==parm.parm));
        }
    }
}