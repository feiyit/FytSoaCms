using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Extensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("Any")]
    public class TokenController : Controller
    {
        #region Token
        /// <summary>
        /// 模拟登录，获取JWT
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getAdmin")]
        public JsonResult GetJWTAdmin()
        {
            var tm = new TokenModel()
            {
                Uid = Guid.NewGuid().ToString(),
                Role = "Admin",
                Project = "Manage",
                TokenType = "Web"
            };
            return Json(JwtHelper.IssueJWT(tm));
        }
        #endregion

        #region Token
        /// <summary>
        /// 模拟登录，获取JWT
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getApp")]
        public JsonResult GetJWTApp()
        {
            var tm = new TokenModel()
            {
                Uid = Guid.NewGuid().ToString(),
                Role = "App",
                Project = "APp",
                TokenType = "App"
            };
            return Json(JwtHelper.IssueJWT(tm));
        }
        #endregion
    }
}