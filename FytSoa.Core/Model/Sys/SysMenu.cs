using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Sys
{
    ///<summary>
    /// 系统菜单表
    ///</summary>
    [SugarTable("Sys_Menu")]
    public partial class SysMenu
    {
        public SysMenu()
        {


        }
        /// <summary>
        /// Desc:唯一标识Guid
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:所属站点或公司菜单
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SiteGuid { get; set; }

        /// <summary>
        /// Desc:菜单父级Guid
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ParentGuid { get; set; }

        /// <summary>
        /// 父级名称
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// Desc:菜单名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:菜单名称标识
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string NameCode { get; set; }

        /// <summary>
        /// Desc:所属父级的集合
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ParentGuidList { get; set; }

        /// <summary>
        /// Desc:菜单深度
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Layer { get; set; }

        /// <summary>
        /// Desc:菜单Url
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Urls { get; set; }

        /// <summary>
        /// Desc:菜单图标Class
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Icon { get; set; }

        /// <summary>
        /// Desc:菜单排序
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Sort { get; set; }

        /// <summary>
        /// Desc:菜单状态 true=正常 false=不显示
        /// Default:b'1'
        /// Nullable:False
        /// </summary>           
        public bool Status { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime EditTime { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddTIme { get; set; }

    }
}
