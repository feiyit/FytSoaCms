using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 根据订单，商品返货详细列表
    /// </summary>
    public class ReturnGoodsDto
    {
        public string Guid { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// 款式名称
        /// </summary>
        public string StyleName { get; set; }
        /// <summary>
        /// 季节名称
        /// </summary>
        public string SeasonName { get; set; }
        /// <summary>
        /// 返货数量
        /// </summary>
        public int Counts { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 返货商品订单列表
    /// </summary>
    public class ReturnOrderDto
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作人联系方式
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 返货数量
        /// </summary>
        public int Counts { get; set; } = 0;
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 返货时间
        /// </summary>
        public DateTime AddDate { get; set; }
    }
}
