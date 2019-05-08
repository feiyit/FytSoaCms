using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FytSoa.Extensions
{
    /// <summary>
    /// 新增一个公司的认证策略
    /// </summary>
    public class CompanyAuthorizeAttribute: AuthorizeAttribute
    {
        public const string CompanyAuthenticationScheme = "CompanyAuthenticationScheme";

        public CompanyAuthorizeAttribute()
        {
            this.AuthenticationSchemes = CompanyAuthenticationScheme;
        }
    }
}
