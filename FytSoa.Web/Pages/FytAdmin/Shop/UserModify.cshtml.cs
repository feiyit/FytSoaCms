using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.Interfaces;

namespace FytSoa.Web.Pages.FytAdmin.Shop
{
    public class UserModifyModel : PageModel
    {
        private readonly IErpShopUserService _userService;
        public UserModifyModel(IErpShopUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public ErpShopUser userModel { get; set; }
        public void OnGet(string guid,string shop)
        {
            userModel = _userService.GetByGuidAsync(guid).Result.data;
            if (!string.IsNullOrEmpty(shop))
            {
                userModel.ShopGuid = shop;
            }
        }
    }
}