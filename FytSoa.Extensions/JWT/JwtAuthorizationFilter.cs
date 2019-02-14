using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FytSoa.Extensions
{
    /// <summary>
    /// 授权中间件
    /// </summary>
    public class JwtAuthorizationFilter
    {
        private readonly RequestDelegate _next;
        public JwtAuthorizationFilter(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext httpContext)
        {
            //检测是否包含'Authorization'请求头，如果不包含则直接放行
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return _next(httpContext);
            }
            var tokenHeader = httpContext.Request.Headers["Authorization"];
            tokenHeader = tokenHeader.ToString().Substring("Bearer ".Length).Trim();

            TokenModel tm = JwtHelper.SerializeJWT(tokenHeader);

            //将tokenModel存入缓存中

            //授权
            var claimList = new List<Claim>();
            var claim = new Claim(ClaimTypes.Role, tm.Role);
            claimList.Add(claim);
            var identity = new ClaimsIdentity(claimList);
            var principal = new ClaimsPrincipal(identity);
            httpContext.User = principal;

            return _next(httpContext);
        }
    }
}
