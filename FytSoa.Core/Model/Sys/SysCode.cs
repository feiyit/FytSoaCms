using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Sys
{
    ///<summary>
    /// 字典值
    ///</summary>
    [SugarTable("Sys_Code")]
    public partial class SysCode
    {
        public SysCode()
        {

        }
        /// <summary>
        /// Desc:唯一标号Guid
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:字典类型标识
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ParentGuid { get; set; }

        /// <summary>
        /// Desc:字典值——类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CodeType { get; set; }

        /// <summary>
        /// Desc:字典值——名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:字典值——英文名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string EnName { get; set; }

        /// <summary>
        /// Desc:字典值——排序
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Sort { get; set; } = 0;

        /// <summary>
        /// Desc:字典值——状态
        /// Default:b'1'
        /// Nullable:False
        /// </summary>           
        public bool Status { get; set; } = true;

        /// <summary>
        /// Desc:字典值——描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:字典值——添加时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Desc:字典值——修改时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime EditTime { get; set; } = DateTime.Now;

    }
}
