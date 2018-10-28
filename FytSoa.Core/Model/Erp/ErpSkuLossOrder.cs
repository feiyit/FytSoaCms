using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    /// <summary>
    /// 条形码报损销售订单
    /// </summary>
    public class ErpSkuLossOrder
    {
        /// <summary>
        /// Desc:唯一ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:订单编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Number { get; set; }

        /// <summary>
        /// Desc:当前订单金额
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Decimal Money { get; set; } = 0;

        /// <summary>
        /// Desc:客户姓名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string CustomerName { get; set; }

        /// <summary>
        /// Desc:客户电话
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string CustomerMobile { get; set; }

        /// <summary>
        /// Desc:订单备注
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:条形码集合
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string SkuList { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;

    }
}
