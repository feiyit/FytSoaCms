using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Areas.APP.Controllers
{
    /// <summary>
    /// 库存相关，出入库
    /// </summary>
    [Route("app/api/[controller]")]
    [Produces("application/json")]
    public class StockController : Controller
    {
        private readonly IErpInOutLogService _inOutLogService;
        private readonly IErpPackLogService _packService;
        private readonly IInventoryService _inventoryService;
        public StockController(IErpInOutLogService inOutLogService, IErpPackLogService packService,
            IInventoryService inventoryService)
        {
            _inOutLogService = inOutLogService;
            _packService = packService;
            _inventoryService = inventoryService;
        }

        /// <summary>
        /// 出库单打包列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("outpack")]
        public JsonResult OutPackList(PageParm parm)
        {
            var res = _packService.GetPagesAsync(parm).Result;
            var list = res.data.Items?.Select(m => new {
                m.Guid,
                m.PackName,
                m.GoodsSum,
                AddDate=m.AddDate.ToShortDateString().Replace("/","-")
            });
            return Json(new { statusCode = 200, msg = "success", count = res.data.TotalPages, data = list });
        }

        /// <summary>
        /// 出库单-商品列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("outpack/list")]
        public JsonResult OutPackGoodsList(PageParm parm, SearchParm searchParm)
        {
            var res = _inOutLogService.GetPagesAsync(parm, searchParm).Result;
            return Json(new { statusCode = 200, msg = "success", count = res.data.TotalPages, data = res.data.Items });
        }

        /// <summary>
        /// 库存盘点、统计报表，可以公用
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("inventory")]
        public JsonResult Inventory(PageParm parm)
        {
            var res = _inventoryService.GetStockNumByShopAsync(parm).Result;
            return Json(new { statusCode = 200, msg = "success", count = res.data?.TotalPages, data = res.data?.Items });
        }

    }
}