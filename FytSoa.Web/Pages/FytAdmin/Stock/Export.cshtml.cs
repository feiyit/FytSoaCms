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
    public class ExportModel : PageModel
    {
        private readonly ISysCodeService _codeService;
        private readonly IErpShopsService _shopsService;
        public ExportModel(ISysCodeService codeService, IErpShopsService shopsService)
        {
            _codeService = codeService;
            _shopsService = shopsService;
        }

        public List<SysCode> codeList { get; set; }
        public List<ErpShops> shopList { get; set; }
        public void OnGet(string guid)
        {
            shopList = _shopsService.GetPagesAsync(new Service.DtoModel.PageParm() { limit = 2000 }).Result.data.Items;
            codeList = _codeService.GetPagesAsync(new Service.DtoModel.SysCodePostPage() { limit = 10000, guid = "7b664e3e-f58a-4e66-8c0f-be1458541d14" }).Result.data?.Items;
        }
    }
}