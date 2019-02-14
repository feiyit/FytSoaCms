using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Sys
{
    ///<summary>
    /// 字典类型
    ///</summary>
    [SugarTable("Sys_CodeType")]
    public partial class SysCodeType
    {
        public SysCodeType()
        {

        }
        /// <summary>
        /// Desc:唯一标号Guid
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:字典类型父级
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ParentGuid { get; set; }

        /// <summary>
        /// Desc:深度
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Layer { get; set; }

        /// <summary>
        /// Desc:字典类型名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:字典类型排序
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Sort { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddTime { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime EditTime { get; set; }

        /// <summary>
        /// Desc:归属公司或站点
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SiteGuid { get; set; }

    }
}
