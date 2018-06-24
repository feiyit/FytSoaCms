using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : Controller
    {
        private readonly IErpInOutLogService _inOutLogService;
        private readonly IErpPackLogService _packLogService;
        private readonly IErpBackGoodsService _backGoodsService;
        private readonly IErpReturnGoodsService _returnGoodsService;
        public StockController(IErpInOutLogService inOutLogService,
            IErpPackLogService packLogService,
            IErpBackGoodsService backGoodsService,
            IErpReturnGoodsService returnGoodsService)
        {
            _inOutLogService = inOutLogService;
            _packLogService = packLogService;
            _backGoodsService = backGoodsService;
            _returnGoodsService = returnGoodsService;
        }
    }
}