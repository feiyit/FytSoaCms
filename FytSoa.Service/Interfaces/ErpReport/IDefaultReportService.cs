using FytSoa.Common;
using FytSoa.Service.DtoModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /// <summary>
    /// 默认页报表中心服务接口
    /// </summary>
    public interface IDefaultReportService
    {
        /// <summary>
        /// 查询今日待办事项，销售统计
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<BackLogReport>> GetBackLogReport();

        /// <summary>
        /// 查询本周，每天的销售额和订单数
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<WeekSaleReport>> GetWeekSaleReport();
    }
}
