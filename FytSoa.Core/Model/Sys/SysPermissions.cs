using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Sys
{
    ///<summary>
    /// 权限表
    ///</summary>
    public partial class SysPermissions
    {
        public SysPermissions()
        {


        }
        /// <summary>
        /// Desc:角色Guid
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string RoleGuid { get; set; }

        /// <summary>
        /// Desc:认证的标识【CURD】
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string AuthorizeGuid { get; set; }

    }
}
