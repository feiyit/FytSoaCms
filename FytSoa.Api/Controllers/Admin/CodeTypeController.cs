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
    [Route("api/codetype")]
    [Authorize(Roles = "Admin")]
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
        [HttpPost("add")]
        public async Task<ApiResult<string>> AddCodeType(SysCodeType parm)
        {
            return await _sysCodeTypeService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<ApiResult<string>> DeleteCode(string guid)
        {
            return await _sysCodeTypeService.DeleteAsync(guid);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<ApiResult<string>> EditCode(SysCodeType parm)
        {
            return await _sysCodeTypeService.ModifyAsync(parm);
        }
    }
}