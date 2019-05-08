using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Sys
{
    ///<summary>
    /// APP版本更新表
    ///</summary>
    [SugarTable("Sys_AppSetting")]
    public partial class SysAppSetting
    {
        public SysAppSetting()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:安卓版本号
        /// Default:0.0
        /// Nullable:False
        /// </summary>           
        public string AndroidVersion { get; set; }

        /// <summary>
        /// Desc:更新文件
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string AndroidFile { get; set; }

        /// <summary>
        /// Desc:Ios版本号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string IosVersion { get; set; }

        /// <summary>
        /// Desc:Ios更新文件地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string IosFile { get; set; }

        /// <summary>
        /// Desc:Ios审核开关  0=关/1=开
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public byte IosAudit { get; set; }

        /// <summary>
        /// 删除 0=不删除/1=删除
        /// </summary>
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// Desc:更新时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime UpdateDate { get; set; } = DateTime.Now;

    }
}
