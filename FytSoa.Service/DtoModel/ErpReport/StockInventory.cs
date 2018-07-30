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
        /// 返货数量
        /// </summary>
        public int returnSum { get; set; } = 0;
    }

    /// <summary>
    /// 营业额
    /// </summary>
    public class DayTurnover
    {
        /// <summary>
        /// 今日/本月订单数据
        /// </summary>
        public int OrderSum { get; set; }
        /// <summary>
        /// 对应的订单总金额
        /// </summary>
        public decimal Money { get; set; }
    }

    /// <summary>
    /// 店铺营业额统计=核算总账
    /// </summary>
    public class ShopTurnover
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 店铺负责人
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// 店铺负责人联系方式
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 订单总数
        /// </summary>
        public int OrderCount { get; set; }
        /// <summary>
        /// 营业额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 返货数
        /// </summary>
        public int ReturnCount { get; set; }

        /// <summary>
        /// 退货数
        /// </summary>
        public int BackCount { get; set; }

        /// <summary>
        /// 退货金额
        /// </summary>
        public decimal BackMoney { get; set; }
    }

    /// <summary>
    /// 加盟商库存
    /// </summary>
    public class ShopStockReport
    {
        /// <summary>
        /// 加盟商唯一编号
        /// </summary>
        public string ShopGuid { get; set; }
        /// <summary>
        /// 加盟商列表
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 当前库存数
        /// </summary>
        public int Stock { get; set; }
    }
}
