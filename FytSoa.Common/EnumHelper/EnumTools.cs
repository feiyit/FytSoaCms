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

    /// <summary>
    /// 店铺活动类型
    /// </summary>
    public enum ActivityTypeEnum
    {
        /// <summary>
        /// 商铺
        /// </summary>
        [Text("商铺")]
        Shops = 1,

        /// <summary>
        /// 商品
        /// </summary>
        [Text("商品")]
        Goods = 2,

        /// <summary>
        /// 地区
        /// </summary>
        [Text("地区")]
        City = 2
    }

    /// <summary>
    /// 店铺活动方式
    /// </summary>
    public enum ActivityMethodEnum
    {
        /// <summary>
        /// 打折
        /// </summary>
        [Text("打折")]
        Discount = 1,

        /// <summary>
        /// 满减
        /// </summary>
        [Text("满减")]
        Full = 2
    }

    /// <summary>
    /// 店铺活动方式
    /// </summary>
    public enum DbOrderEnum
    {
        /// <summary>
        /// 打折
        /// </summary>
        [Text("排序Asc")]
        Asc = 1,

        /// <summary>
        /// 满减
        /// </summary>
        [Text("排序Desc")]
        Desc = 2
    }
}
