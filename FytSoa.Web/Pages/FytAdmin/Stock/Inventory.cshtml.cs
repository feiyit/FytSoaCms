using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Stock
{
    [Authorize]
    public class InventoryModel : PageModel
    {
        private readonly ISysCodeService _codeService;
        public InventoryModel(ISysCodeService codeService)
        {
            _codeService = codeService;
        }

        public List<SysCode> codeList { get; set; }
        public List<SysCode> yearCodeList { get; set; }
        public List<SysCode> seasonCodeList { get; set; }
        public List<SysCode> sizeCodeList { get; set; }
        public void OnGet(string guid)
        {
            codeList = _codeService.GetPagesAsync(new Service.DtoModel.SysCodePostPage() { limit = 10000, guid = "7b664e3e-f58a-4e66-8c0f-be1458541d14" }).Result.data?.Items;
            yearCodeList = _codeService.GetPagesAsync(new Service.DtoModel.SysCodePostPage() { limit = 10000, guid = "1942d4fd-3203-42b1-a955-4a84a532b2a2" }).Result.data?.Items;
            seasonCodeList = _codeService.GetPagesAsync(new Service.DtoModel.SysCodePostPage() { limit = 10000, guid = "8cb134d5-979b-40e2-b453-aeee265f4ab2" }).Result.data?.Items;
            sizeCodeList = _codeService.GetPagesAsync(new Service.DtoModel.SysCodePostPage() { limit = 10000, guid = "e86cf108-dc4d-4532-8cce-fdb041363902" }).Result.data?.Items;
        }
    }
}