using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Erp;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Goods
{
    public class BarCodeModifyModel : PageModel
    {
        private readonly IErpGoodsSkuService _skuService;
        private readonly ISysCodeService _codeService;
        public BarCodeModifyModel(IErpGoodsSkuService skuService, ISysCodeService codeService)
        {
            _skuService = skuService;
            _codeService = codeService;
        }

        [BindProperty]
        public ErpGoodsSku skuModel { get; set; }

        public List<SysCode> codeList { get; set; }
        public void OnGet(string guid)
        {
            skuModel = _skuService.GetByGuidAsync(guid).Result.data;
            codeList = _codeService.GetPagesAsync(new Service.DtoModel.SysCodePostPage() { limit = 10000 }).Result.data?.Items;
        }
    }
}