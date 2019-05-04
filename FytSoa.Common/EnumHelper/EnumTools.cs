using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Common
{
    public enum LogEnum
    {
        /// <summary>
        /// 保存或添加
        /// </summary>
        [Text("保存或添加")]
        ADD = 1,

        /// <summary>
        /// 更新
        /// </summary>
        [Text("更新/修改")]
        UPDATE = 2,

        /// <summary>
        /// 更新
        /// </summary>
        [Text("审核")]
        AUDIT = 3,

        /// <summary>
        /// 删除
        /// </summary>
        [Text("删除")]
        DELETE = 4,

        /// <summary>
        /// 读取/查询
        /// </summary>
        [Text("读取/查询")]
        RETRIEVE = 5,

        /// <summary>
        /// 登录
        /// </summary>
        [Text("登录")]
        LOGIN = 6,

        /// <summary>
        /// 查看
        /// </summary>
        [Text("查看")]
        LOOK = 7,

        /// <summary>
        /// 更改状态
        /// </summary>
        [Text("更改状态")]
        STATUS = 8,

        /// <summary>
        /// 授权
        /// </summary>
        [Text("授权")]
        AUTHORIZE = 9,

        /// <summary>
        /// 退出登录
        /// </summary>
        [Text("退出登录")]
        LOGOUT = 10,

        /// <summary>
        /// 同步到微信
        /// </summary>
        [Text("同步到微信")]
        ASYWX = 11
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
