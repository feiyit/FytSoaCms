using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 返货表
    ///</summary>
    public partial class ErpReturnGoods
    {
        public ErpReturnGoods()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:返货订单编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string OrderGuid { get; set; }

        /// <summary>
        /// Desc:加盟商编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ShopGuid { get; set; }

        /// <summary>
        /// Desc:返货的是哪件衣服
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GoodsGuid { get; set; }

        /// <summary>
        /// Desc:返货的数量
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int ReturnCount { get; set; } = 0;

        /// <summary>
        /// 1=正常  2=作废
        /// 主要解决返货出现问题后，可以人工干预库存数对不上问题
        /// </summary>
        public int Status { get; set; } = 1;

        /// <summary>
        /// Desc:返货描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

    }
}
