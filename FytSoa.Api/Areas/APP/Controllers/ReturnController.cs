using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Erp;
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
                m.Guid,
                m.Number,
                m.Counts,
                AddDate=m.AddDate.ToString().Replace("/","-").Replace("T"," ")
            });
            return Json(new { statusCode = 200, msg = "success", count = res.data.TotalPages, data = list });
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
                m.BrandName,
                m.StyleName,
                m.SeasonName,
                m.Counts
            });
            return Json(new { statusCode = 200, msg = "success", count = res.data.TotalPages, data = list });
        }

        /// <summary>
        /// 添加返货信息，包括返货订单和返货订单里面的商品
        /// </summary>
        /// <param name="parm">订单信息</param>
        /// <param name="goodsJson">返货订单商品Json字符串</param>
        /// <returns></returns>
        [HttpPost("add/order")]
        public Task<ApiResult<string>> AddReturnOrder(ErpReturnOrder parm,string goodsJson)
        {
            return _orderService.AddAsync(parm, goodsJson);
        }
    }
}