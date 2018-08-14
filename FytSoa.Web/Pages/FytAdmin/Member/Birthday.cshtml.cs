using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Member
{
    [Authorize]
    public class BirthdayModel : PageModel
    {
        private readonly IErpShopsService _shopsService;
        public BirthdayModel(IErpShopsService shopsService)
        {
            _shopsService = shopsService;
        }
        public string shopGuid { get; set; }
        public List<ErpShops> shopList { get; set; }
        public void OnGet(string shop)
        {
            shopGuid = !string.IsNullOrEmpty(shop) ? shop : "all";
            shopList = _shopsService.GetPagesAsync(new Service.DtoModel.PageParm() { limit = 2000 }).Result.data.Items;
        }
    }
}