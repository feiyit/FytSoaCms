using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FytSoa.Extensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        #region Token
        /// <summary>
        /// 模拟登录，获取JWT
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        [HttpGet("Admin")]
        public IActionResult GetJWTAdmin()
        {
            var tm = new TokenModel()
            {
                Uid = Guid.NewGuid().ToString(),
                UserName="User",
                Role="Admin",
                TokenType = "Web"
            };
            return Ok(JwtHelper.IssueJWT(tm));
        }
        #endregion

        #region Token
        /// <summary>
        /// 模拟登录，获取JWT
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        [HttpGet("App")]
        public IActionResult GetJWTApp()
        {
            var tm = new TokenModel()
            {
                Uid = Guid.NewGuid().ToString(),
                UserName = "User",
                Role = "App",
                TokenType = "App"
            };
            return Ok(JwtHelper.IssueJWT(tm));
        }
        #endregion
    }
}