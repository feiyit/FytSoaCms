using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Sys
{
    public class CodesValModifyModel : PageModel
    {
        private readonly ISysCodeService _sysCodeService;
        public CodesValModifyModel(ISysCodeService sysCodeService)
        {
            _sysCodeService = sysCodeService;
        }

        [BindProperty]
        public SysCode CodeModel { get; set; }
        public void OnGet(string parm)
        {
            CodeModel = _sysCodeService.GetByGuidAsync(parm).Result.data;
            if (string.IsNullOrEmpty(CodeModel.Guid))
            {
                CodeModel.ParentGuid = parm;
            }
        }
    }
}