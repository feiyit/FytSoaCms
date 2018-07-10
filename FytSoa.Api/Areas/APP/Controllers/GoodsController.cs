using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


    }
}