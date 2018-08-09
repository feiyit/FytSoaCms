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
    public class InventoryController : Controller
    {
        private readonly IErpPackLogService _packLogService;
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService, IErpPackLogService packLogService)
        {
            _inventoryService = inventoryService;
            _packLogService = packLogService;
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
        [HttpPost("shopstock")]
        public async Task<ApiResult<List<ShopStockReport>>> GetShopStockReport(PageParm parm)
        {
            return await _inventoryService.GetShopStockReport(parm);
        }

        /// <summary>
        /// 商家营业额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("stockbyshop")]
        public async Task<JsonResult> GetStockByShop(PageParm parm, AppSearchParm searchParm)
        {
            var res = await _inventoryService.GetStockNumByShopAsync(parm, searchParm);
            return Json(new { code = 0, msg = "success", count = res.data?.TotalItems, data = res.data?.Items });
        }

        /// <summary>
        /// 平台入库统计报表，入库 总数，可根据年份查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("platforminstock")]
        public async Task<ApiResult<List<PlatformInOutStockReport>>> GetPlatformInStockReport(PageParm parm)
        {
            return await _inventoryService.GetPlatformInStockReport(parm);
        }

        /// <summary>
        /// 平台入库统计报表，入库单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("packloglist")]
        public async Task<JsonResult> GetPackLogPages(PageParm parm)
        {
            var res = await _packLogService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data?.TotalItems, data = res.data?.Items });
        }

        /// <summary>
        /// 加盟商退货统计报表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("shopbackreport")]
        public async Task<JsonResult> GetShopBackReport(PageParm parm)
        {
            var res = await _inventoryService.GetShopBackReport(parm);
            return Json(new { code = 0, msg = "success", count = 1, res.data });
        }

        /// <summary>
        /// 加盟商返货统计报表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("shopreturnreport")]
        public async Task<JsonResult> GetShopReturnReport(PageParm parm)
        {
            var res = await _inventoryService.GetShopReturnReport(parm);
            return Json(new { code = 0, msg = "success", count = 1, res.data });
        }
    }
}