using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Implements
{
    /// <summary>
    /// 默认页报表中心服务实现
    /// </summary>
    public class DefaultReportService : DbContext, IDefaultReportService
    {
        /// <summary>
        /// 查询今日待办事项，销售统计
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<BackLogReport>> GetBackLogReport()
        {
            var res = new ApiResult<BackLogReport>() { statusCode= (int)ApiEnum.Error };
            try
            {
                res.data = new BackLogReport();
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return Task.Run(() => res);
        }

        /// <summary>
        /// 查询本周，每天的销售额和订单数
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<WeekSaleReport>> GetWeekSaleReport()
        {
            var res = new ApiResult<WeekSaleReport>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var moneyList = new List<decimal>();
                var orderList = new List<int>();
                for (int i = 0; i < 7; i++)
                {
                    moneyList.Add(i);
                    orderList.Add(i);
                }
                res.data = new WeekSaleReport() { Money = moneyList, Order= orderList };
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return Task.Run(() => res);
        }
    }
}
