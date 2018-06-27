using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Erp;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Stock
{
    [Authorize]
    public class TransferModel : PageModel
    {
        private readonly IErpShopsService _shopService;
        public TransferModel(IErpShopsService shopService)
        {
            _shopService = shopService;
        }

        public List<ErpShops> List { get; set; }
        public void OnGet(string guid)
        {
            List = _shopService.GetPagesAsync(new Service.DtoModel.PageParm() { limit = 1000 }).Result.data.Items;
        }
    }
}