using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Purchase
{
    [Authorize]
    public class SupplierModifyModel : PageModel
    {
        private readonly IErpSupplierService _supplierService;
        public SupplierModifyModel(IErpSupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [BindProperty]
        public ErpSupplier Supplier { get; set; }
        public void OnGet(string guid)
        {
            Supplier = _supplierService.GetByGuidAsync(guid).Result.data;
        }
    }
}