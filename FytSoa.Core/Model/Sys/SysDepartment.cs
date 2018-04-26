using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Sys
{
    ///<summary>
    /// 部门表
    ///</summary>
    public partial class SysDepartment
    {
        public SysDepartment()
        {


        }
        /// <summary>
        /// Desc:唯一标识
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:部门编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Codes { get; set; }

        /// <summary>
        /// Desc:所属公司
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ParentGuid { get; set; }

        /// <summary>
        /// Desc:父级部门的集合
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ParentGuidList { get; set; }

        /// <summary>
        /// Desc:所属公司Guid
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string CompanyGuid { get; set; }

        /// <summary>
        /// Desc:部门层级
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Layer { get; set; }

        /// <summary>
        /// Desc:部门名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:部门备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:部门办公地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Address { get; set; }

        /// <summary>
        /// Desc:部门电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DepartmentTel { get; set; }

        /// <summary>
        /// Desc:排序字段
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Sort { get; set; }

        /// <summary>
        /// Desc:删除状态
        /// Default:b'0'
        /// Nullable:False
        /// </summary>           
        public bool DelStatus { get; set; }

        /// <summary>
        /// Desc:添加部门
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
