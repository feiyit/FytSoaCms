using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FytSoa.Common;

namespace FytSoa.Web.Pages.FytAdmin.Sys
{
    [Authorize]
    public class AdminModifyModel : PageModel
    {
        private readonly ISysAdminService _adminService;
        public AdminModifyModel(ISysAdminService adminService)
        {
            _adminService = adminService;
        }

        [BindProperty]
        public SysAdmin adminModel { get; set; }
        public void OnGet(string guid)
        {
            adminModel = _adminService.GetByGuidAsync(guid).Result.data;
            //密码解密
            adminModel.LoginPwd = DES3Encrypt.DecryptString(adminModel.LoginPwd);
        }
    }
}