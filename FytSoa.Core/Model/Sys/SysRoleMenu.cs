using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Sys
{
    ///<summary>
    /// 权限角色管理菜单表
    ///</summary>
    public partial class SysRoleMenu
    {
        public SysRoleMenu()
        {


        }
        /// <summary>
        /// Desc:角色Guid
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string RoleGuid { get; set; }

        /// <summary>
        /// Desc:菜单Guid
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string MenuGuid { get; set; }

    }
}
