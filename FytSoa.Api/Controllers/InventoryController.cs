using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        /// <summary>
        /// 库存盘点
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("stock")]
        public async Task<JsonResult> GetBarCodePages(PageParm parm)
        {
            var res = await _inventoryService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data?.TotalItems, data = res.data?.Items });
        }

        /// <summary>
        /// 商家营业额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("shopturnover")]
        public async Task<JsonResult> GetShopTurnover(PageParm parm)
        {
            var res = await _inventoryService.GetShopTurnover(parm);
            return Json(new { code = 0, msg = "success", count = res.data?.TotalItems, data = res.data?.Items });
        }

        /// <summary>
        /// 月份营业额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("monthturnover")]
        public async Task<JsonResult> GetMonthTurnover(PageParm parm)
        {
            var res = await _inventoryService.GetMonthTurnover(parm);
            return Json(new { code = 0, msg = "success", count =1, res.data });
        }

        /// <summary>
        /// 获得加盟商列表，包含库存总数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("shopstock")]
        public async Task<JsonResult> GetShopStockReport(PageParm parm)
        {
            var res = await _inventoryService.GetShopStockReport(parm);
            return Json(new { code = 0, msg = "success", count = 1, res.data });
        }
    }
}