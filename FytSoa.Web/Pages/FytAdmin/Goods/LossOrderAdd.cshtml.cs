using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Goods
{
    [Authorize]
    public class LossOrderAddModel : PageModel
    {
        private readonly IErpSkuLossOrderService _lossOrderService;
        public LossOrderAddModel(IErpSkuLossOrderService lossOrderService)
        {
            _lossOrderService = lossOrderService;
        }

        [BindProperty]
        public ErpSkuLossOrder OrderModel { get; set; }

        public void OnGet(string guid, string sku)
        {
            OrderModel = _lossOrderService.GetByGuidAsync(guid).Result.data;
            if (string.IsNullOrEmpty(guid))
            {
                OrderModel.SkuList = sku;
            }
        }
    }
}