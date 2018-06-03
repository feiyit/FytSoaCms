using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Sys
{ 
    [Authorize]
    public class MenuModifyModel : PageModel
    {
        private readonly ISysMenuService _sysMenuService;
        public MenuModifyModel(ISysMenuService sysMenuService)
        {
            _sysMenuService = sysMenuService;
        }

        [BindProperty]
        public SysMenu MenuModel { get; set; }
        public void OnGet(string guid)
        {
            MenuModel = _sysMenuService.GetByGuidAsync(guid).Result.data;
        }
    }
}