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
    public class AdvClassModel : PageModel
    {
        private readonly ICmsAdvClassService _classService;
        public AdvClassModel(ICmsAdvClassService classService)
        {
            _classService = classService;
        }

        [BindProperty]
        public CmsAdvClass Class { get; set; }

        public List<CmsAdvClass> List { get; set; }
        public void OnGet(string guid)
        {
            Class = _classService.GetModelAsync(m => m.Guid == guid).Result.data;
            List = _classService.GetListAsync().Result.data;
        }
    }
}