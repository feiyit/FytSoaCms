using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Shop
{
    [Authorize]
    public class ShopModifyModel : PageModel
    {
        private readonly IErpShopsService _shopService;
        public ShopModifyModel(IErpShopsService shopService)
        {
            _shopService = shopService;
        }

        [BindProperty]
        public ErpShops shopModel { get; set; }
        public void OnGet(string guid)
        {
            shopModel = _shopService.GetByGuidAsync(guid).Result.data;
            //密码解密
            shopModel.LoginPwd = DES3Encrypt.DecryptString(shopModel.LoginPwd);
        }
    }
}