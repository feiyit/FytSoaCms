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
        private readonly IErpShopsService _shopService;
        public PackLogModel(IErpPackLogService packLogService, IErpShopsService shopService)
        {
            _packLogService = packLogService;
            _shopService = shopService;
        }

        [BindProperty]
        public ErpPackLog PackModel { get; set; }
        [BindProperty]
        public List<ErpShops> List { get; set; }
        public void OnGet(string guid,string types)
        {
            PackModel = _packLogService.GetByGuidAsync(guid).Result.data;
            if (string.IsNullOrEmpty(PackModel.Number))
            {
                PackModel.Types = Convert.ToByte(types);
                PackModel.Number = Utils.GetOrderNumber();
            }
            //出库的时候，查询店铺列表
            if (!string.IsNullOrEmpty(types) && types=="2")
            {
                List = _shopService.GetPagesAsync(new Service.DtoModel.PageParm() { limit = 10000 }).Result.data.Items;
            }
        }
    }
}