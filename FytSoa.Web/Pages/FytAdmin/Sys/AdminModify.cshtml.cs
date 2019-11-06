using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FytSoa.Common;

namespace FytSoa.Web.Pages.FytAdmin.Sys
{
    [Authorize]
    public class AdminModifyModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}