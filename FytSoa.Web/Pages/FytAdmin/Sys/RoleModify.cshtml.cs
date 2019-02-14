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
    public class RoleModifyModel : PageModel
    {
        private readonly ISysRoleService _roleService;
        public RoleModifyModel(ISysRoleService roleService)
        {
            _roleService = roleService;
        }

        [BindProperty]
        public SysRole RoleModel { get; set; }
        public void OnGet(string guid)
        {
            RoleModel = _roleService.GetByGuidAsync(guid).Result.data;
        }
    }
}