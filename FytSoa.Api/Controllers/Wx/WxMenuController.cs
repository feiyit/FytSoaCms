using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Wx;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers.Wx
{
    [Produces("application/json")]
    [Route("api/wx/menu")]
    [Authorize(Roles = "Admin")]
    //[ApiController]
    public class WxMenuController : Controller
    {
        private readonly IWxSettingService _settingService;
        public WxMenuController(IWxSettingService settingService)
        {
            _settingService = settingService;
        }

        /// <summary>
        /// 修改自定义菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<ApiResult<string>> DeleteRole(int id, string menu)
        {
            var model = _settingService.GetModelAsync(m => m.Id == id).Result.data;
            model.MenuJson = menu;
            return await _settingService.UpdateAsync(model);
        }

        /// <summary>
        /// 修改自定义菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("model")]
        public async Task<ApiResult<WxSetting>> GetModel(int id)
        {
            return await _settingService.GetModelAsync(m=>m.Id==id);
        }
    }
}