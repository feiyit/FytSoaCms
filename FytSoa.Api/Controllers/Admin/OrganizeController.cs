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
    [Route("api/Organize")]
    [JwtAuthorize(Roles = "Admin")]
    public class OrganizeController : Controller
    {
        private readonly ISysOrganizeService _sysOrganizeService;
        public OrganizeController(ISysOrganizeService sysOrganizeService)
        {
            _sysOrganizeService = sysOrganizeService;
        }

        /// <summary>
        /// 获得组织结构Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("gettree")]
        public List<SysOrganizeTree> GetListPage()
        {
            return _sysOrganizeService.GetListTreeAsync().Result.data;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("getpages")]
        public async Task<JsonResult> GetPages(PageParm parm)
        {
            var res = await _sysOrganizeService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data.TotalItems, data = res.data.Items });
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("add"), ApiAuthorize(Modules = "Department", Methods = "Add", LogType = LogEnum.ADD)]
        public async Task<ApiResult<string>> AddOrganize([FromBody]SysOrganize parm)
        {
            return await _sysOrganizeService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete"), ApiAuthorize(Modules = "Department", Methods = "Delete", LogType = LogEnum.DELETE)]
        public async Task<ApiResult<string>> DeleteOrganize([FromBody]ParmString parm)
        {
            return await _sysOrganizeService.DeleteAsync(parm.parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit"),ApiAuthorize(Modules = "Department", Methods = "Update", LogType = LogEnum.UPDATE)]
        public async Task<ApiResult<string>> EditOrganize([FromBody]SysOrganize parm)
        {
            return await _sysOrganizeService.ModifyAsync(parm);
        }
    }
}