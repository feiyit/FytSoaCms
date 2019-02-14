using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Sys
{
    ///<summary>
    /// 权限角色管理菜单表
    ///</summary>
    [SugarTable("Sys_Permissions")]
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
        /// 管理员编号
        /// </summary>
        public string AdminGuid { get; set; }

        /// <summary>
        /// Desc:菜单Guid
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string MenuGuid { get; set; }

        /// <summary>
        /// 按钮功能编号
        /// </summary>
        public string BtnFunGuid { get; set; }

        /// <summary>
        /// 授权类型1=角色-菜单 2=用户-角色 3=角色-菜单-按钮功能
        /// 默认=1
        /// </summary>
        public int Types { get; set; } = 1;

    }
}
