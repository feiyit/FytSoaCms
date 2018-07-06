using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Areas.APP.Controllers
{
    /// <summary>
    /// 首页/今日订单/今日营业数据统计
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class IndexController : Controller
    {
        private readonly IErpReturnGoodsService _returnService;
        public IndexController(IErpReturnGoodsService returnService)
        {
            _returnService = returnService;
        }
    }
}