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
    public class SettingController : Controller
    {
        private readonly ISysAppSettingService _settingService;
        public SettingController(ISysAppSettingService settingService)
        {
            _settingService = settingService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("getpages")]
        public async Task<JsonResult> GetPages(PageParm parm)
        {
            var res = await _settingService.GetPagesAsync(parm,m=>!m.IsDel,m=>m.UpdateDate,DbOrderEnum.Desc);
            return Json(new { code = 0, msg = "success", count = res.data.TotalItems, data = res.data.Items });
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ApiResult<string>> AddRole(SysAppSetting parm)
        {
            parm.Guid= Guid.NewGuid().ToString();
            return await _settingService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<ApiResult<string>> DeleteRole(string parm)
        {
            var list = Utils.StrToListString(parm);
            return await _settingService.DeleteAsync(m => new SysAppSetting() { IsDel = true }, m => list.Contains(m.Guid));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<ApiResult<string>> EditRole(SysAppSetting parm)
        {
            return await _settingService.UpdateAsync(parm);
        }
    }
}