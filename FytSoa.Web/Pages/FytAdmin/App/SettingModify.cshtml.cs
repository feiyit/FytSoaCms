using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.App
{
    [Authorize]
    public class SettingModifyModel : PageModel
    {
        private readonly ISysAppSettingService _settingService;
        public SettingModifyModel(ISysAppSettingService settingService)
        {
            _settingService = settingService;
        }

        [BindProperty]
        public SysAppSetting SettingModel { get; set; }
        
        public void OnGet(string guid)
        {
            if (!string.IsNullOrEmpty(guid))
            {
                SettingModel = _settingService.GetModelAsync(guid).Result.data;
            }
            else
            {
                SettingModel = new SysAppSetting();
            }
        }
    }
}