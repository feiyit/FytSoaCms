using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Sale
{
    public class ReturnValidationModel : PageModel
    {
        [BindProperty]
        public string orderGuid { get; set; }
        public void OnGet(string order)
        {
            orderGuid = order;
        }
    }
}