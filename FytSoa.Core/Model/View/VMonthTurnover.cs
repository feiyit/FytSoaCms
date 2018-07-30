using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    /// <summary>
    /// 统计Erp  月份的报表
    /// </summary>
    public class VMonthTurnover
    {
        /// <summary>
        /// 月份
        /// </summary>
        public string Months { get; set; }
        /// <summary>
        /// 实收金额 不包含退货的
        /// </summary>
        public decimal RealMoney { get; set; } = 0;
        /// <summary>
        /// 销售订单数量
        /// </summary>
        public int Counts { get; set; } = 0;
        /// <summary>
        /// 退货商品数量  不等于退货订单数量
        /// </summary>
        public int BackCount { get; set; } = 0;
        /// <summary>
        /// 退货金额
        /// </summary>
        public decimal BackMoney { get; set; } = 0;
        /// <summary>
        /// 返货商品数量  不等于返货的订单数量
        /// </summary>
        public int ReturnCount { get; set; } = 0;
    }
}
