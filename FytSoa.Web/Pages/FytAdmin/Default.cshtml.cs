using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FytSoa.Core.Model.Cms;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin
{
    [Authorize]
    public class DefaultModel : PageModel
    {
        private readonly ICmsSiteService _siteService;
        private readonly ISysAdminService _adminService;
        public DefaultModel(ICmsSiteService siteService, ISysAdminService adminService)
        {
            _siteService = siteService;
            _adminService = adminService;
        }
        [BindProperty]
        public CmsSite Site { get; set; }
        [BindProperty]
        public SysAdmin Admin { get; set; }
        public void OnGet()
        {
            Site = _siteService.GetModelAsync("78756a6c-50c8-47a5-b898-5d6d24a20327").Result.data;
            var adminGuid = User.Identities.First(u => u.IsAuthenticated).FindFirst(ClaimTypes.Sid).Value;
            Admin = _adminService.GetModelAsync(adminGuid).Result.data;
        }
    }
}