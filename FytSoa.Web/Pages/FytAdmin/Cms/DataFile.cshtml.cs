using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Cms
{
    [Authorize]
    public class DataFileModel : PageModel
    {
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnGetDown(string fileName)
        {
            var dpwnPath = FileHelperCore.MapPath("/wwwroot/db_back/" + fileName);
            FileStream fs = new FileStream(dpwnPath, FileMode.Open);
            return File(fs, "application/vnd.android.package-archive", fileName);
        }
    }
}