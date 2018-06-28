using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 采购供应商表
    ///</summary>
    public partial class ErpSupplier
    {
        public ErpSupplier()
        {


        }
        /// <summary>
        /// Desc:采购供应商唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:供应商名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:联系人
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Contacts { get; set; }

        /// <summary>
        /// Desc:联系电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Mobile { get; set; }

        /// <summary>
        /// Desc:联系邮箱
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Email { get; set; }

        /// <summary>
        /// Desc:属性=自定义 Json对象
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Attribute { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; }

    }
}
