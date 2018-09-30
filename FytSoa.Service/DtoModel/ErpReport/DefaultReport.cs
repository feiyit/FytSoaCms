using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 默认页——待办事项
    /// </summary>
    public class BackLogReport
    {
        /// <summary>
        /// 返货处理
        /// </summary>
        public int ReturnCount { get; set; } = 0;

        /// <summary>
        /// 退货处理
        /// </summary>
        public int BackCount { get; set; } = 0;

        /// <summary>
        /// 库存报警
        /// </summary>
        public int StockPoliceCount { get; set; } = 0;

        /// <summary>
        /// 加入会员
        /// </summary>
        public int JoinUserCount { get; set; } = 0;

        /// <summary>
        /// 今日销售金额
        /// </summary>
        public decimal DaySaleMoney { get; set; } = 0;

        /// <summary>
        /// 本周销售金额
        /// </summary>
        public decimal WeekSaleMoney { get; set; } = 0;

        /// <summary>
        /// 本周销售金额
        /// </summary>
        public decimal MonthSaleMoney { get; set; } = 0;
    }

    /// <summary>
    /// 本周销售报表
    /// </summary>
    public class WeekSaleReport
    {
        public List<decimal> Money { get; set; }
        public List<int> Order { get; set; }
    }

    /// <summary>
    /// 本周销售金额报表
    /// </summary>
    public class WeekSaleMoneyReport
    {
        /// <summary>
        /// 周一
        /// </summary>
        public decimal Monday { get; set; } = 0;

        /// <summary>
        /// 周二
        /// </summary>
        public decimal Tuesday { get; set; } = 0;

        /// <summary>
        /// 周三
        /// </summary>
        public decimal Wednesday { get; set; } = 0;

        /// <summary>
        /// 周四
        /// </summary>
        public decimal Thursday { get; set; } = 0;

        /// <summary>
        /// 周五
        /// </summary>
        public decimal Friday { get; set; } = 0;

        /// <summary>
        /// 周六
        /// </summary>
        public decimal Saturday { get; set; } = 0;

        /// <summary>
        /// 周日
        /// </summary>
        public decimal Sunday { get; set; } = 0;
    }

    /// <summary>
    /// 本周销售订单报表
    /// </summary>
    public class WeekSaleOrderReport
    {
        /// <summary>
        /// 周一
        /// </summary>
        public int Monday { get; set; } = 0;

        /// <summary>
        /// 周二
        /// </summary>
        public int Tuesday { get; set; } = 0;

        /// <summary>
        /// 周三
        /// </summary>
        public int Wednesday { get; set; } = 0;

        /// <summary>
        /// 周四
        /// </summary>
        public int Thursday { get; set; } = 0;

        /// <summary>
        /// 周五
        /// </summary>
        public int Friday { get; set; } = 0;

        /// <summary>
        /// 周六
        /// </summary>
        public int Saturday { get; set; } = 0;

        /// <summary>
        /// 周日
        /// </summary>
        public int Sunday { get; set; } = 0;
    }
}
