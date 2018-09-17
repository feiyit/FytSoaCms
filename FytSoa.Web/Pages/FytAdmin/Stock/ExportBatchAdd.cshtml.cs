using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FytSoa.Core.Model.Erp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Stock
{
    public class ExportBatchAddModel : PageModel
    {
       
        [BindProperty]
        public ErpInOutLog InoutModel { get; set; }

        public void OnGet(string packguid, string shopguid)
        {
            InoutModel = new ErpInOutLog()
            {
                PackGuid = packguid,
                GoodsSum = 1,
                Types = 2,
                AdminGuid = @User.Identities.First(u => u.IsAuthenticated).FindFirst(ClaimTypes.Sid).Value,
                ShopGuid = shopguid
            };
        }
    }
}