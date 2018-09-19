using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Stock
{
    public class ImportValidationModel : PageModel
    {
        [BindProperty]
        public string PackGuid { get; set; }
        public void OnGet(string pack)
        {
            PackGuid = pack;
        }
    }
}