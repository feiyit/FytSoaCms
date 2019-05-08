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
    public class CodeTypeController : Controller
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
        public List<SysCodeTypeTree> GetListPage()
        {
            return _sysCodeTypeService.GetListTreeAsync().Result.data;
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("add"), ApiAuthorize(Modules = "Key", Methods = "Add", LogType = LogEnum.ADD)]
        public async Task<ApiResult<string>> AddCodeType([FromBody]SysCodeType parm)
        {
            return await _sysCodeTypeService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete"), ApiAuthorize(Modules = "Key", Methods = "Delete", LogType = LogEnum.DELETE)]
        public async Task<ApiResult<string>> DeleteCode([FromBody]ParmString obj)
        {
            return await _sysCodeTypeService.DeleteAsync(obj.parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit"), ApiAuthorize(Modules = "Key", Methods = "Update", LogType = LogEnum.UPDATE)]
        public async Task<ApiResult<string>> EditCode([FromBody]SysCodeType parm)
        {
            return await _sysCodeTypeService.ModifyAsync(parm);
        }
    }
}