using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Sys;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FytSoa.Web.Pages.FytAdmin
{
    
    public class LoginModel : PageModel
    {
        [BindProperty]
        public List<string> RsaKey { get; set; }
        public void OnGet()
        {            
            var auth = HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //auth.Result.Succeeded;
            if (auth.Status.ToString()!= "Faulted")
            {
                RedirectToPage("Index");
            }
            RsaKey = RSACrypt.GetKey();
            //获得公钥和私钥
            MemoryCacheService.Default.SetCache("LOGINKEY", RsaKey);
        }

    }
}