using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 采购商品表
    ///</summary>
    public partial class ErpPurchaseGoods
    {
        public ErpPurchaseGoods()
        {


        }
        /// <summary>
        /// Desc:采购商品表唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:物品属于哪个采购单
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PurchaseGuid { get; set; }

        /// <summary>
        /// Desc:物品编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Number { get; set; }

        /// <summary>
        /// Desc:物品名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:规格型号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Specification { get; set; }

        /// <summary>
        /// Desc:单位
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Unit { get; set; }

        /// <summary>
        /// Desc:采购数量
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Quantity { get; set; }

        /// <summary>
        /// Desc:单价
        /// Default:0.00
        /// Nullable:False
        /// </summary>           
        public decimal Price { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

    }
}
