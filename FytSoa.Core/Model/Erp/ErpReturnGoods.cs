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
        /// Desc:返货描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

    }
}
