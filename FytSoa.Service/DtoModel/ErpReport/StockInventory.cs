using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 库存盘点
    /// </summary>
    public class StockInventory
    {
        /// <summary>
        /// 条形码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 总库存库存数
        /// </summary>
        public int TotalStock { get; set; }
        /// <summary>
        /// 剩余库存数
        /// </summary>
        public int Stock { get; set; }
        /// <summary>
        /// 出库数
        /// </summary>
        public int OutStock { get; set; }
        /// <summary>
        /// 调拨数
        /// </summary>
        public int Transfer { get; set; }
        /// <summary>
        /// 返货数
        /// </summary>
        public int Return { get; set; }
        /// <summary>
        /// 退货数
        /// </summary>
        public int Back { get; set; }
        /// <summary>
        /// 销售数
        /// </summary>
        public int Sale { get; set; }
    }

    /// <summary>
    /// 库存剩余数量
    /// </summary>
    public class StockSaleNum
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 商品Sku编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 商品品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 商品款式
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 商品销售数量
        /// </summary>
        public int Sale { get; set; }
        /// <summary>
        /// 商品库存数量
        /// </summary>
        public int Stock { get; set; }
        /// <summary>
        /// 销售和库存数量的百分比
        /// </summary>
        public int Perc { get; set; }
    }
}
