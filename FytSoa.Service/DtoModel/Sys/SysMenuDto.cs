using FytSoa.Core.Model.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 权限管理，授权菜单参数
    /// </summary>
    public class SysMenuAuthorization
    {
        /// <summary>
        /// 菜单列表
        /// </summary>
        public List<SysMenuDto> list { get; set; }

        /// <summary>
        /// 授权角色
        /// </summary>
        public string roleGuid { get; set; }
    }

    /// <summary>
    /// 管理员登录，获得菜单权限列表
    /// </summary>
    public class SysMenuDto
    {
        /// <summary>
        /// Desc:唯一标识Guid
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string guid { get; set; }

        /// <summary>
        /// Desc:菜单父级Guid
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string parentGuid { get; set; }

        /// <summary>
        /// 父级名称
        /// </summary>
        public string parentName { get; set; }

        /// <summary>
        /// Desc:菜单名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string name { get; set; }

        /// <summary>
        /// Desc:菜单名称标识
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string nameCode { get; set; }

        /// <summary>
        /// Desc:所属父级的集合
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string parentGuidList { get; set; }

        /// <summary>
        /// Desc:菜单深度
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int layer { get; set; }

        /// <summary>
        /// Desc:菜单Url
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string urls { get; set; }

        /// <summary>
        /// Desc:菜单图标Class
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string icon { get; set; }

        /// <summary>
        /// Desc:菜单图标Class
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string btnJson { get; set; }

        /// <summary>
        /// Desc:菜单排序
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int sort { get; set; }

        /// <summary>
        /// Desc:权限操作是否选中
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool isChecked { get; set; } = false;

        /// <summary>
        /// 当前菜单的功能列表
        /// </summary>
        public List<SysCodeDto> btnFun { get; set; }
    }
    
    /// <summary>
    /// 菜单生成树
    /// </summary>
    public class SysMenuTree
    {
        public string guid { get; set; }
        public string name { get; set; }
        public bool isChecked { get; set; }
        public int layer { get; set; }
        public string parentGuid { get; set; }
        public List<SysMenuTree> children { get; set; }
        public bool open { get; set; } = true;
        public int sort { get; set; }
    }
}
