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
    public class StaffModifyModel : PageModel
    {
        private readonly IErpStaffService _staffService;
        public StaffModifyModel(IErpStaffService staffService)
        {
            _staffService = staffService;
        }

        [BindProperty]
        public ErpStaff staffModel { get; set; }
        public void OnGet(string guid)
        {
            staffModel = _staffService.GetByGuidAsync(guid).Result.data;
        }
    }
}