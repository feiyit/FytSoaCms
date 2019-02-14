using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Sys
{
    ///<summary>
    /// 系统操作表
    ///</summary>
    [SugarTable("Sys_Log")]
    public partial class SysLog
    {
        public SysLog()
        {


        }
        /// <summary>
        /// Desc:唯一标识——Guid
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:日志操作ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string LoginName { get; set; }

        /// <summary>
        /// Desc:日志操作人所属部门Guid
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DepartName { get; set; }

        /// <summary>
        /// Desc:操作表名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string OptionTable { get; set; }

        /// <summary>
        /// Desc:日志操作内容
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:日志操作IP地址
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string IP { get; set; }

        /// <summary>
        /// Desc:日志操作Mac地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MacUrl { get; set; }

        /// <summary>
        /// Desc:日志操作类型
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int LogType { get; set; }

        /// <summary>
        /// Desc:日志操作Url
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Urls { get; set; }

        /// <summary>
        /// Desc:日志添加时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddTime { get; set; }

    }
}
