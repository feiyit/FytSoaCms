using System;
using System.Collections.Generic;
using System.Text;
using FytSoa.Core.Model.Sys;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 角色授权，入参
    /// </summary>
    public class SysPermissionsParm
    {
        /// <summary>
        /// 角色
        /// </summary>
        public string role { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public string menu { get; set; }

        /// <summary>
        /// 按钮的值
        /// </summary>
        public string btnfun { get; set; }

        /// <summary>
        /// 状态 1=授权  0=取消
        /// </summary>
        public int status { get; set; } = 1;
    }

    /// <summary>
    /// 权限参数
    /// </summary>
    public class AuthorityMenuParam
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 授权菜单列表
        /// </summary>
        public List<AuthorityMenu> Menus { get; set; }
    }

    /// <summary>
    /// 授权菜单列表
    /// </summary>
    public class AuthorityMenu
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单按钮权限
        /// </summary>
        public List<SysBtnFun> BtnFun { get; set; }
    }
}
