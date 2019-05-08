using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages
{
    public class TestModel : PageModel
    {
        public WxAccessToken token { get; set; }
        public string Mater { get; set; }
        public void OnGet()
        {
            token = WxTools.GetAccess("wx7797e2efb5dc9502", "9bfed87a0157250c26479a084c207b12");
            var tests = WxTools.GetMediaList(token.access_token);
        }
    }
}