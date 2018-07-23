using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 加盟商条形码
    /// </summary>
    public class ShopSkuDto
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 条形码编号
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 条形码
        /// </summary>
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
        public int Stock { get; set; }
        /// <summary>
        /// 销售数量
        /// </summary>
        public int Sale { get; set; }
    }
}
