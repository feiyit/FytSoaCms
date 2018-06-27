using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Stock
{
    public class TransferGoodsModel : PageModel
    {
        private readonly ISysCodeService _codeService;
        public TransferGoodsModel(ISysCodeService codeService)
        {
            _codeService = codeService;
        }

        public List<SysCode> codeList { get; set; }
        [BindProperty]
        public string TransferGuid { get; set; }
        public void OnGet(string transferGuid)
        {
            TransferGuid = transferGuid;
            codeList = _codeService.GetPagesAsync(new Service.DtoModel.SysCodePostPage() { limit = 10000, guid = "7b664e3e-f58a-4e66-8c0f-be1458541d14" }).Result.data?.Items;
        }
    }
}