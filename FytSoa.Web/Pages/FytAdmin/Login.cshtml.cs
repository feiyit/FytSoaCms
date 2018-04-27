using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin
{
    public class LoginModel : PageModel
    {
        private readonly ISysAdminService _sysAdminService;
        public LoginModel(ISysAdminService sysAdminService)
        {
            _sysAdminService = sysAdminService;
        }
        public void OnGet()
        {
            var auth = HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (auth.Result.Succeeded)
            {
                RedirectToPage("Index");
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostLoginAsync(SysAdminLogin parm)
        {
            var apiRes = _sysAdminService.LoginAsync(parm);
            try
            {
                var user = apiRes.Result.data;
                if (apiRes.Result.statusCode == 200)
                {
                    var identity = new ClaimsPrincipal(
                     new ClaimsIdentity(new[]
                         {
                              new Claim(ClaimTypes.Sid,user.Guid),
                              new Claim(ClaimTypes.Role,"超级管理员"),
                              new Claim(ClaimTypes.Thumbprint,user.HeadPic),
                              new Claim(ClaimTypes.Name,user.TrueName),
                              new Claim(ClaimTypes.UserData,user.UpLoginDate.ToString()),
                         }, CookieAuthenticationDefaults.AuthenticationScheme)
                    );
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, identity, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
                        IsPersistent = true,
                        AllowRefresh = false
                    });
                }

            }
            catch (Exception ex)
            {
                apiRes.Result.message = ex.Message;
                apiRes.Result.statusCode = (int)ApiEnum.Error;
            }

            return new JsonResult(new ApiResult<string>() { statusCode = apiRes.Result.statusCode, message = apiRes.Result.message });
        }
    }
}