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
    [Route("app/api/[controller]")]
    [Produces("application/json")]
    public class GoodsController : Controller
    {
        private readonly IErpGoodsSkuService _goodsService;
        private readonly IErpShopActivityService _activityService;
        public GoodsController(IErpGoodsSkuService goodsService, IErpShopActivityService activityService)
        {
            _goodsService = goodsService;
            _activityService = activityService;
        }

        /// <summary>
        /// 根据条形码获得商品信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("bycode")]
        public JsonResult GetGoodsByCode(string shopGuid,string code)
        {
            //根据条形码，查询是商品
            var goods=_goodsService.GetByCodeAsync(shopGuid, code).Result.data;
            //查询活动，包括店铺和全部加盟商活动 只查询最新添加的一条
            var activity = _activityService.GetByShopsAsync(shopGuid).Result.data;
            return Json(new { statusCode = 200,good=goods,activity });
        }
    }
}