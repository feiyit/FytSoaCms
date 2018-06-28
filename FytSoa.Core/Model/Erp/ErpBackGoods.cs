using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 退货表
    ///</summary>
    public partial class ErpBackGoods
    {
        public ErpBackGoods()
        {


        }
        /// <summary>
        /// Desc:唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:订单号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Number { get; set; }

        /// <summary>
        /// Desc:退货涉及的店铺
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ShopGuid { get; set; }

        /// <summary>
        /// Desc:退货涉及的订单号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string OrderGuid { get; set; }

        /// <summary>
        /// Desc:谁提交的退货
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string AdminGuid { get; set; }

        /// <summary>
        /// Desc:退货的商品
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GoodsGuid { get; set; }

        /// <summary>
        /// Desc:退货数量
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int BackCount { get; set; } = 0;

        /// <summary>
        /// Desc:退货的金额
        /// Default:0.00
        /// Nullable:False
        /// </summary>           
        public decimal BackMoney { get; set; } = 0;

        /// <summary>
        /// Desc:退货的状态 1=提交退货 2=受理 3=完成 4=其他
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public byte Status { get; set; } = 0;

        /// <summary>
        /// Desc:退货原因
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:提交退货的时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;

    }
}
