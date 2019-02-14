using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 关联ErpGoodsSku
    /// </summary>
    public class GoodsSkuDto
    {
        public string Guid { get; set; }

        public string Code { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string BrankName { get; set; }
        /// <summary>
        /// 款式
        /// </summary>
        public string StyleName { get; set; }
        /// <summary>
        /// 季节
        /// </summary>
        public string SeasonName { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string SalePrice { get; set; }
        /// <summary>
        /// 折扣价格
        /// </summary>
        public string DisPrice { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int StockSum { get; set; }
        /// <summary>
        /// 销售数量
        /// </summary>
        public int SaleSum { get; set; }
        public DateTime AddDate { get; set; }
    }
}
