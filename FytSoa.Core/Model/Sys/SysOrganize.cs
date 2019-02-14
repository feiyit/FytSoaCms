using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Sys
{
    ///<summary>
    /// 组织表
    ///</summary>
    [SugarTable("Sys_Organize")]
    public partial class SysOrganize
    {
        public SysOrganize()
        {


        }
        /// <summary>
        /// Desc:唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// 站点编码
        /// </summary>
        public string SiteGuid { get; set; }

        /// <summary>
        /// Desc:父节点
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ParentGuid { get; set; }

        /// <summary>
        /// Desc:组织名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:父节名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ParentName { get; set; }

        /// <summary>
        /// Desc:层级
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Layer { get; set; }

        /// <summary>
        /// Desc:父节点集合
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ParentGuidList { get; set; }

        /// <summary>
        /// Desc:排序
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int Sort { get; set; } = 1;

        /// <summary>
        /// Desc:状态
        /// Default:b'0'
        /// Nullable:False
        /// </summary>           
        public bool Status { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime EditTime { get; set; }

    }
}
