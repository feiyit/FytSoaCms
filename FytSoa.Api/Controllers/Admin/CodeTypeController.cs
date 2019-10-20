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
    [Route("api/codetype")]
    [JwtAuthorize(Roles = "Admin")]
    public class CodeTypeController : ControllerBase
    {
        private readonly ISysCodeTypeService _sysCodeTypeService;
        public CodeTypeController(ISysCodeTypeService sysCodeTypeService)
        {
            _sysCodeTypeService = sysCodeTypeService;
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("gettree")]
        public async Task<IActionResult> GetListPage()
        {
            var res = await _sysCodeTypeService.GetListTreeAsync();
            return Ok(res.data);
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("add"), ApiAuthorize(Modules = "Key", Methods = "Add", LogType = LogEnum.ADD)]
        public async Task<IActionResult> AddCodeType([FromBody]SysCodeType parm)
        {
            return Ok(await _sysCodeTypeService.AddAsync(parm));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete"), ApiAuthorize(Modules = "Key", Methods = "Delete", LogType = LogEnum.DELETE)]
        public async Task<IActionResult> DeleteCode([FromBody]ParmString obj)
        {
            return Ok(await _sysCodeTypeService.DeleteAsync(obj.parm));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit"), ApiAuthorize(Modules = "Key", Methods = "Update", LogType = LogEnum.UPDATE)]
        public async Task<IActionResult> EditCode([FromBody]SysCodeType parm)
        {
            return Ok(await _sysCodeTypeService.ModifyAsync(parm));
        }
    }
}