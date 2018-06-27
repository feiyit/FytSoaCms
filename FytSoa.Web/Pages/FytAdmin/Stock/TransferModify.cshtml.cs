using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Stock
{
    public class TransferModifyModel : PageModel
    {
        private readonly IErpTransferService _transferService;
        private readonly IErpShopsService _shopService;
        public TransferModifyModel(IErpTransferService transferService, IErpShopsService shopService)
        {
            _transferService = transferService;
            _shopService = shopService;
        }

        [BindProperty]
        public ErpTransfer TransferModel { get; set; }

        [BindProperty]
        public List<ErpShops> List { get; set; }
        public void OnGet(string guid)
        {
            TransferModel = _transferService.GetByGuidAsync(guid).Result.data;
            List = _shopService.GetPagesAsync(new Service.DtoModel.PageParm() {limit=1000 }).Result.data.Items;
            if (string.IsNullOrEmpty(TransferModel.Guid))
            {
                TransferModel.Number = Utils.GetOrderNumber();
                TransferModel.AdminGuid = @User.Identities.First(u => u.IsAuthenticated).FindFirst(ClaimTypes.Sid).Value;
            }
        }
    }
}