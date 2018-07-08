using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 销售订单表
    ///</summary>
    public partial class ErpSaleOrder
    {
        public ErpSaleOrder()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:销售订单编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Number { get; set; }

        /// <summary>
        /// Desc:店铺编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ShopGuid { get; set; }

        /// <summary>
        /// Desc:操作员编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string AdminGuid { get; set; }

        /// <summary>
        /// Desc:会员编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserGuid { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// Desc:订单类型 1=普通销售/2=打折销售/3=满减销售
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public byte Types { get; set; }

        /// <summary>
        /// Desc:订单总件数
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Counts { get; set; }

        /// <summary>
        /// Desc:订单金额
        /// Default:0.00
        /// Nullable:False
        /// </summary>           
        public decimal Money { get; set; }

        /// <summary>
        /// Desc:下单日期
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; }

    }
}
