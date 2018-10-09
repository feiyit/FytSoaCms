using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/default")]
    public class DefaultReportController : Controller
    {
        private readonly IDefaultReportService _defaultService;
        public DefaultReportController(IDefaultReportService defaultService)
        {
            _defaultService = defaultService;
        }

        /// <summary>
        /// 待办事项-销售统计
        /// </summary>
        /// <returns></returns>
        [HttpPost("backlog")]
        public async Task<ApiResult<BackLogReport>> BackLogReportAsync()
        {
            return await _defaultService.GetBackLogReport();
        }

        /// <summary>
        /// 查询本周，每天的销售额和订单数
        /// </summary>
        /// <returns></returns>
        [HttpPost("weeksale")]
        public async Task<ApiResult<WeekSaleReport>> WeekSaleReportAsync()
        {
            return await _defaultService.GetWeekSaleReport();
        }

        /// <summary>
        /// 查询加盟商销售排行 top 7
        /// </summary>
        /// <returns></returns>
        [HttpPost("shoptop")]
        public async Task<ApiResult<List<ShopSaleTop>>> ShopSaleTopReportAsync()
        {
            return await _defaultService.GetShopSaleTopReport();
        }

        /// <summary>
        /// 查询品牌销售排行 top 20
        /// </summary>
        /// <returns></returns>
        [HttpPost("brandtop")]
        public async Task<ApiResult<List<BrandSaleTop>>> BrandSaleTopReportAsync()
        {
            return await _defaultService.GetBrandSaleTopReport();
        }
    }
}