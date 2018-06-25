using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Stock
{
    public class PackLogModel : PageModel
    {
        private readonly IErpPackLogService _packLogService;
        public PackLogModel(IErpPackLogService packLogService)
        {
            _packLogService = packLogService;
        }

        [BindProperty]
        public ErpPackLog PackModel { get; set; }
        public void OnGet(string guid,string types)
        {
            PackModel = _packLogService.GetByGuidAsync(guid).Result.data;
            if (string.IsNullOrEmpty(PackModel.Number))
            {
                PackModel.Types = Convert.ToByte(types);
                PackModel.Number = Utils.GetOrderNumber();
            }
        }
    }
}