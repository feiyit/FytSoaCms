using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FytSoa.Core.Model.Erp;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Stock
{
    public class TransferGoodsModel : PageModel
    {
        private readonly ISysCodeService _codeService;
        private readonly IErpTransferService _transferService;
        public TransferGoodsModel(ISysCodeService codeService, IErpTransferService transferService)
        {
            _codeService = codeService;
            _transferService = transferService;
        }

        public List<SysCode> codeList { get; set; }
        [BindProperty]
        public ErpTransfer TransferModel { get; set; }

        public string AdminGuid { get; set; }
        public void OnGet(string transferGuid)
        {
            AdminGuid = User.Identities.First(u => u.IsAuthenticated).FindFirst(ClaimTypes.Sid).Value;
            TransferModel = _transferService.GetByGuidAsync(transferGuid).Result.data;
            codeList = _codeService.GetPagesAsync(new Service.DtoModel.SysCodePostPage() { limit = 10000, guid = "7b664e3e-f58a-4e66-8c0f-be1458541d14" }).Result.data?.Items;
        }
    }
}