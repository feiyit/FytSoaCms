using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Sys
{
    ///<summary>
    /// 权限角色表
    ///</summary>
    [SugarTable("Sys_Role")]
    public partial class SysRole
    {
        public SysRole()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:部门Guid
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string DepartmentGuid { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 归属于角色组
        /// </summary>
        public string DepartmentGroup { get; set; }

        /// <summary>
        /// Desc:部门名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:部门编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Codes { get; set; }

        /// <summary>
        /// Desc:是否为超级管理员
        /// Default:b'0'
        /// Nullable:False
        /// </summary>           
        public bool IsSystem { get; set; }

        /// <summary>
        /// Desc:部门描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddTime { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime EditTime { get; set; }

    }
}
