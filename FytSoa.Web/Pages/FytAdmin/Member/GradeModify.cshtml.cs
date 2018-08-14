using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Member
{
    public class GradeModifyModel : PageModel
    {
        private readonly IErpUserGradeService _gradeService;
        public GradeModifyModel(IErpUserGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [BindProperty]
        public ErpUserGrade GradeModel { get; set; }

        public void OnGet(string guid)
        {
            GradeModel = _gradeService.GetByGuidAsync(guid).Result.data;
        }
    }
}