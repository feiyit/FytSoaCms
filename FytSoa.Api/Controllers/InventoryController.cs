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
    }
}