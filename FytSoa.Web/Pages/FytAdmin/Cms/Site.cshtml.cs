using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Cms
{
    [Authorize]
    public class SiteModel : PageModel
    {
        private readonly ICmsSiteService _siteService;
        public SiteModel(ICmsSiteService siteService)
        {
            _siteService = siteService;
        }
        [BindProperty]
        public CmsSite Site { get; set; }
        public void OnGet()
        {
            Site = _siteService.GetModelAsync("78756a6c-50c8-47a5-b898-5d6d24a20327").Result.data;
        }
    }
}