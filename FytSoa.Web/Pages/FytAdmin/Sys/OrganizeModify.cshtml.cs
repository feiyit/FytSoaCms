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
    public class OrganizeModifyModel : PageModel
    {
        private readonly ISysOrganizeService _sysOrganizeService;
        public OrganizeModifyModel(ISysOrganizeService sysOrganizeService)
        {
            _sysOrganizeService = sysOrganizeService;
        }

        [BindProperty]
        public SysOrganize OrganizeModel { get; set; }
        public void OnGet(string guid)
        {
            OrganizeModel = _sysOrganizeService.GetByGuidAsync(guid).Result.data;            
        }
    }
}