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
        /// 本月销售金额
        /// </summary>
        public decimal MonthSaleMoney { get; set; } = 0;

        /// <summary>
        /// 日同比
        /// </summary>
        public double DayOnYear { get; set; } = 0;

        /// <summary>
        /// 周同比
        /// </summary>
        public double WeekOnYear { get; set; } = 0;

        /// <summary>
        /// 月同比
        /// </summary>
        public double MonthOnYear { get; set; } = 0;
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
    /// 根据日期查询，返回的结果
    /// </summary>
    public class WeekDayRes
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string Days { get; set; }
        /// <summary>
        /// 订单总数
        /// </summary>
        public int Counts { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal Money { get; set; }
    }

    /// <summary>
    /// 店铺销售排行榜
    /// </summary>
    public class ShopSaleTop
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 订单总数
        /// </summary>
        public int Counts { get; set; } = 0;
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal Money { get; set; } = 0;
        /// <summary>
        /// 占比
        /// </summary>
        public decimal Ratio { get; set; } = 0;
    }

    /// <summary>
    /// 品牌销售排行榜
    /// </summary>
    public class BrandSaleTop
    {
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// 销售总数
        /// </summary>
        public int Counts { get; set; } = 0;
    }
}
