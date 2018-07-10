using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.App
{
    [Authorize]
    public class SettingModifyModel : PageModel
    {
        private readonly IErpAppSettingService _settingService;
        public SettingModifyModel(IErpAppSettingService settingService)
        {
            _settingService = settingService;
        }

        [BindProperty]
        public ErpAppSetting SettingModel { get; set; }
        
        public void OnGet(string guid)
        {
            SettingModel = _settingService.GetByGuidAsync(guid).Result.data;
        }
    }
}