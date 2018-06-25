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
    public class ImportModifyModel : PageModel
    {
        private readonly IErpInOutLogService _inService;
        public ImportModifyModel(IErpInOutLogService inService)
        {
            _inService = inService;
        }

        [BindProperty]
        public ErpInOutLog InoutModel { get; set; }
        public void OnGet(string guid,string packguid)
        {
            InoutModel = _inService.GetByGuidAsync(guid).Result.data;
            if (string.IsNullOrEmpty(InoutModel.Guid))
            {
                InoutModel.PackGuid = packguid;
                InoutModel.GoodsSum = 1;
                InoutModel.AdminGuid = @User.Identities.First(u => u.IsAuthenticated).FindFirst(ClaimTypes.Sid).Value;
            }
        }
    }
}