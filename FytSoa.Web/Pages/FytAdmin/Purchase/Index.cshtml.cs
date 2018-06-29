using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Purchase
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int Type { get; set; }
        public void OnGet(int type)
        {
            Type = type;
        }
    }
}