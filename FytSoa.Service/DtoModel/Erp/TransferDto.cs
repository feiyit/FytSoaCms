using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 返回调拨单列表
    /// </summary>
    public class TransferDto
    {
        public string Guid { get; set; }
        public string Number { get; set; }
        public string InShopName { get; set; }
        public string OutShopName { get; set; }
        public int GoodsSum { get; set; }
        public DateTime AddDate { get; set; }
    }

    /// <summary>
    /// 返回调拨商品列表
    /// </summary>
    public class TransferGoodsDto
    {
        public string Guid { get; set; }
        public string Code { get; set; }
        public string BrankName { get; set; }
        public string StyleName { get; set; }
        public int GoodsSum { get; set; }
    }

    /// <summary>
    /// 前端选择的商品列表
    /// </summary>
    public class TransferGoods
    {
        public string guid { get; set; }
        public int goodsSum { get; set; }
    }
}
