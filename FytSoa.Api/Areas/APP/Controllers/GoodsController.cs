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
        public GoodsController(IErpGoodsSkuService goodsService)
        {
            _goodsService = goodsService;
        }

        /// <summary>
        /// 根据条形码获得商品信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("bycode")]
        public Task<ApiResult<GoodsSkuDto>> GetGoodsByCode(string shopGuid,string code)
        {
            return _goodsService.GetByCodeAsync(shopGuid,code);
        }
    }
}