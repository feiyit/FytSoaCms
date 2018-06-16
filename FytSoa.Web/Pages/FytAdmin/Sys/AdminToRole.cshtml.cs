using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Sys
{
    public class AdminToRoleModel : PageModel
    {
        [BindProperty]
        public string adminGuids { get; set; }
        public void OnGet(string roid)
        {
            adminGuids = roid;
        }
    }
}