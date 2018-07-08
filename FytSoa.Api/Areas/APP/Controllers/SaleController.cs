using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Areas.APP.Controllers
{
    /// <summary>
    /// 销售
    /// </summary>
    [Route("app/api/[controller]")]
    [Produces("application/json")]
    public class SaleController : Controller
    {
        private readonly IErpSaleOrderService _orderService;
        private readonly IErpSaleOrderGoodsService _goodsService;
        public SaleController(IErpSaleOrderService orderService, IErpSaleOrderGoodsService goodsService)
        {
            _orderService = orderService;
            _goodsService = goodsService;
        }

        /// <summary>
        /// 销售订单列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("order/list")]
        public JsonResult SaleOrderList(PageParm parm)
        {
            var res = _orderService.GetPagesAsync(parm).Result;
            var list = res.data.Items?.Select(m => new {
                m.Number,
                m.Counts,
                m.Money,
                AddDate = m.AddDate.ToShortDateString().Replace("/", "-")
            });
            return Json(new { statusCode = 200, msg = "success", count = res.data.Items?.Count ?? 0, data = list });
        }

        /// <summary>
        /// 销售订单商品列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("goods/list")]
        public JsonResult SaleOrderGoodsList(PageParm parm)
        {
            var res = _goodsService.GetPagesAsync(parm).Result;
            return Json(new { statusCode = 200, msg = "success", count = res.data.Items?.Count ?? 0, data = res.data.Items });
        }
    }
}