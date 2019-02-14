using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
        [BindProperty]
        public List<string> RsaKey { get; set; }
        public void OnGet()
        {
            RsaKey = RSACrypt.GetKey();
            var auth = HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //auth.Result.Succeeded;
            if (auth.Status.ToString()!= "Faulted")
            {
                RedirectToPage("Index");
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="parm">登录信息参数</param>
        /// <param name="privateKey">加密私钥</param>
        /// <param name="publicKey">加密公钥</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostLoginAsync(SysAdminLogin parm, string privateKey, string publicKey)
        {
            var apiRes = new ApiResult<Core.Model.Sys.SysAdmin>();
            var token = "";
            try
            {
                //Ras解密密码
                var ras = new RSACrypt(privateKey, publicKey);
                parm.password=ras.Decrypt(parm.password);
                //查询登录结果
                apiRes = _sysAdminService.LoginAsync(parm).Result;
                var user = apiRes.data;
                if (apiRes.statusCode == 200)
                {
                    var identity = new ClaimsPrincipal(
                     new ClaimsIdentity(new[]
                         {
                              new Claim(ClaimTypes.Sid,user.Guid),
                              new Claim(ClaimTypes.Role,user.DepartmentName),
                              new Claim(ClaimTypes.Thumbprint,user.HeadPic),
                              new Claim(ClaimTypes.Name,user.TrueName),
                              new Claim(ClaimTypes.WindowsAccountName,user.LoginName),
                              new Claim(ClaimTypes.UserData,user.UpLoginDate.ToString()),
                         }, CookieAuthenticationDefaults.AuthenticationScheme)
                    );
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, identity, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddHours(2),
                        IsPersistent = true,
                        AllowRefresh = false
                    });
                    var tm = new TokenModel()
                    {
                        Uid = Guid.NewGuid().ToString(),
                        Role = "Admin",
                        Project = "Manage",
                        TokenType = "Web"
                    };
                    token=JwtHelper.IssueJWT(tm);
                }

            }
            catch (Exception ex)
            {
                apiRes.message = ex.Message;
                apiRes.statusCode = (int)ApiEnum.Error;
            }

            return new JsonResult(new ApiResult<string>() { statusCode = apiRes.statusCode, message = apiRes.message,data=token });
        }
    }
}