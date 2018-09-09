using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 销售订单详细表
    ///</summary>
    public partial class ErpSaleOrderGoods
    {
        public ErpSaleOrderGoods()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:加盟商编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ShopGuid { get; set; }

        /// <summary>
        /// Desc:订单编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string OrderNumber { get; set; }

        /// <summary>
        /// Desc:订单商品编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GoodsGuid { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int Counts { get; set; } = 0;

        /// <summary>
        /// 当前订单的商品退货数
        /// </summary>
        public int BackCounts { get; set; } = 0;

        /// <summary>
        /// 订单商品金额
        /// </summary>
        public decimal Money { get; set; } = 0;

    }
}
