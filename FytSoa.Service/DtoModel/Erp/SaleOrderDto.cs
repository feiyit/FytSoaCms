using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 销售订单列表
    /// </summary>
    public class SaleOrderDto
    {
        /// <summary>
        /// Desc:销售订单编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Number { get; set; }

        /// <summary>
        /// Desc:店铺名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ShopName { get; set; }

        /// <summary>
        /// Desc:订单类型 1=普通销售/2=打折销售/3=满减销售
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public string ActivityTypes { get; set; }

        /// <summary>
        /// Desc:销售类型 1=正常销售/2=残次品销售
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public string SaleType { get; set; }

        /// <summary>
        /// Desc:订单总件数
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Counts { get; set; }

        /// <summary>
        /// Desc:活动名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ActivityName { get; set; }

        /// <summary>
        /// Desc:订单金额
        /// Default:0.00
        /// Nullable:False
        /// </summary>           
        public decimal Money { get; set; }

        /// <summary>
        /// Desc:实际收入金额
        /// Default:0.00
        /// Nullable:False
        /// </summary>           
        public decimal RealMoney { get; set; }

        /// <summary>
        /// Desc:下单日期
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; }

        public List<SaleOrderGoodsDto> Goods { get; set; }
    }


    /// <summary>
    /// 订单商品详情列表
    /// </summary>
    public class SaleOrderGoodsDto
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 条形码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; } = 0;

        /// <summary>
        /// 数量
        /// </summary>
        public int Counts { get; set; }
    }

    /// <summary>
    /// 提供根据订单编号查询的内容
    /// </summary>
    public class SaleOrderApp
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 条形码
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Counts { get; set; }
    }
}
