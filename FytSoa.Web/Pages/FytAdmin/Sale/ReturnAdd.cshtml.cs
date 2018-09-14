using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Sale
{
    public class ReturnAddModel : PageModel
    {
        [BindProperty]
        public string shopGuid { get; set; }
        public void OnGet(string shopid)
        {
            shopGuid = shopid;
        }
    }
}