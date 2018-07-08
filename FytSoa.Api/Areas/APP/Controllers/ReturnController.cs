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
    /// 返货
    /// </summary>
    [Route("app/api/[controller]")]
    [Produces("application/json")]
    public class ReturnController : Controller
    {
        private readonly IErpReturnOrderService _orderService;
        private readonly IErpReturnGoodsService _goodsService;
        public ReturnController(IErpReturnOrderService orderService,IErpReturnGoodsService goodsService)
        {
            _orderService = orderService;
            _goodsService = goodsService;
        }

        /// <summary>
        /// 返货订单列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("order/list")]
        public JsonResult ReturnOrderList(PageParm parm)
        {
            var res = _orderService.GetPagesAsync(parm).Result;
            var list = res.data.Items?.Select(m => new {
                m.Number,
                m.Counts,
                AddDate = m.AddDate.ToShortDateString().Replace("/", "-")
            });
            return Json(new { statusCode = 200, msg = "success", count = res.data.Items?.Count ?? 0, data = list });
        }

        /// <summary>
        /// 返货订单里面的商品列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("goods/list")]
        public JsonResult ReturnOrderGoodsList(PageParm parm)
        {
            var res = _goodsService.GetPagesAsync(parm).Result;
            var list = res.data.Items?.Select(m => new {
                m.Code,
                m.GoodsName,
                m.Counts
            });
            return Json(new { statusCode = 200, msg = "success", count = res.data.Items?.Count ?? 0, data = list });
        }
    }
}