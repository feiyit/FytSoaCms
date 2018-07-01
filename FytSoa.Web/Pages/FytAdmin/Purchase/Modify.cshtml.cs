using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FytSoa.Core.Model.Erp;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FytSoa.Web.Pages.FytAdmin.Purchase
{
    [Authorize]
    public class ModifyModel : PageModel
    {
        private readonly IErpPurchaseService _purchaseService;
        private readonly ISysCodeService _codeService;
        private readonly IErpPurchaseGoodsService _goodsService;
        public ModifyModel(IErpPurchaseService purchaseService,ISysCodeService codeService, IErpPurchaseGoodsService goodsService)
        {
            _purchaseService = purchaseService;
            _codeService = codeService;
            _goodsService = goodsService;
        }

        [BindProperty]
        public ErpPurchase Purchase { get; set; }

        [BindProperty]
        public List<SysCode> codeList { get; set; }

        [BindProperty]
        public string GoodsJson { get; set; }

        public void OnGet(string guid)
        {
            Purchase = _purchaseService.GetByGuidAsync(guid).Result.data;
            if (string.IsNullOrEmpty(Purchase.Guid))
            {
                Purchase.AdminGuid = User.Identities.First(u => u.IsAuthenticated).FindFirst(ClaimTypes.Sid).Value;
            }
            else
            {
                var goodsList = _goodsService.GetPagesAsync(new Service.DtoModel.PageParm() { limit = 1000, guid = guid }).Result.data?.Items
                    .OrderBy(m=>m.Number)
                    .Select(m=>new {
                        m.Number,m.Name,m.Specification,m.Unit,m.Quantity,m.Price,m.Summary
                });
                if (goodsList.Any())
                {
                    GoodsJson = JsonConvert.SerializeObject(goodsList);
                }
            }                    
            codeList = _codeService.GetPagesAsync(new Service.DtoModel.SysCodePostPage() { limit = 10000, guid = "7088d9b9-6692-4fc7-a83c-da580f1407c3" }).Result.data?.Items;
        }
    }
}