using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Stock
{
    public class ExportModifyModel : PageModel
    {
        private readonly IErpInOutLogService _inService;
        public ExportModifyModel(IErpInOutLogService inService)
        {
            _inService = inService;
        }

        [BindProperty]
        public ErpInOutLog InoutModel { get; set; }

        [BindProperty]
        public List<ErpShops> List { get; set; }
        public void OnGet(string guid, string packguid,string shopguid)
        {
            InoutModel = _inService.GetByGuidAsync(guid).Result.data;
            if (string.IsNullOrEmpty(InoutModel.Guid))
            {
                InoutModel.PackGuid = packguid;
                InoutModel.GoodsSum = 1;
                InoutModel.Types = 2;
                InoutModel.AdminGuid = @User.Identities.First(u => u.IsAuthenticated).FindFirst(ClaimTypes.Sid).Value;
                InoutModel.ShopGuid = shopguid;
            }
        }
    }
}