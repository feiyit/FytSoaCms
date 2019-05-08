using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Sys
{
    [Authorize]
    public class CodesModifyModel : PageModel
    {
        private readonly ISysCodeTypeService _sysCodeTypeService;
        public CodesModifyModel(ISysCodeTypeService sysCodeTypeService)
        {
            _sysCodeTypeService = sysCodeTypeService;
        }

        [BindProperty]
        public SysCodeTypeDto CodeType { get; set; }

        [BindProperty]
        public List<SysCodeType> SelectList { get; private set; }

        public void OnGet(string guid)
        {
            CodeType = _sysCodeTypeService.GetByGuidAsync(guid).Result.data;
            //获得列表
            SelectList = _sysCodeTypeService.GetListAsync().Result.data;
        }

    }
}