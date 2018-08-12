using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Shop
{
    public class StaffModifyModel : PageModel
    {
        private readonly IErpStaffService _staffService;
        public StaffModifyModel(IErpStaffService staffService)
        {
            _staffService = staffService;
        }

        [BindProperty]
        public ErpStaff staffModel { get; set; }
        public void OnGet(string guid, string shop)
        {
            staffModel = _staffService.GetByGuidAsync(guid).Result.data;
            if (staffModel!=null)
            {
                //密码解密
                staffModel.LoginPwd = DES3Encrypt.DecryptString(staffModel.LoginPwd);
            }
            if (!string.IsNullOrEmpty(shop))
            {
                staffModel.ShopGuid = shop;
            }
        }
    }
}