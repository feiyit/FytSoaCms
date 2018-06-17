using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Common
{
    public enum LogEnum
    {
        /// <summary>
        /// 登录日志
        /// </summary>
        [Text("登录日志")]
        Login = 1,

        /// <summary>
        /// 操作日志
        /// </summary>
        [Text("操作日志")]
        Operation = 2
    }

    public enum PermissionsEnum
    {
        /// <summary>
        /// 菜单归属角色
        /// </summary>
        [Text("菜单归属角色")]
        MenuToRole = 1,

        /// <summary>
        /// 管理员归属角色
        /// </summary>
        [Text("管理员归属角色")]
        AdminToRole = 2,

        /// <summary>
        /// 菜单上面的按钮功能
        /// </summary>
        [Text("菜单上面的按钮功能")]
        MenuToBtnFun = 3,
    }
}
