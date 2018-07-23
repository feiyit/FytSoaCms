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
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SaleController : Controller
    {
        private readonly IErpSaleOrderService _orderService;
        private readonly IErpSaleOrderGoodsService _goodService;
        public SaleController(IErpSaleOrderService orderService,
            IErpSaleOrderGoodsService goodService)
        {
            _orderService = orderService;
            _goodService = goodService;
        }

        #region 销售订单列表
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("order")]
        public async Task<ApiResult<Page<SaleOrderDto>>> GetSaleOrderPages(PageParm parm,AppSearchParm searchParm)
        {
            return await _orderService.GetPagesNoGoodsAsync(parm, searchParm);
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("goods")]
        public async Task<JsonResult> GetSaleGoodsPages(PageParm parm, SearchParm searchParm)
        {
            var res = await _goodService.GetPagesAsync(parm, searchParm);
            return Json(new { code = 0, msg = "success", count = res.data?.TotalItems, data = res.data?.Items });
        }
        #endregion
    }
}