using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 退货信息
    /// </summary>
    public class BackGoodsDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string OrderNumber { get; set; }
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
        /// 加盟商
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
        /// 退货数量
        /// </summary>
        public int BackCount { get; set; }
        /// <summary>
        /// 退货金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime AddDate { get; set; }
    }
}
