using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IErpReturnGoodsService _returnService;
        public ReturnController(IErpReturnGoodsService returnService)
        {
            _returnService = returnService;
        }

        /// <summary>
        /// 返货列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("list")]
        public JsonResult BackGoodsList(PageParm parm)
        {
            var res = _returnService.GetPagesAsync(parm).Result;

            return Json(new { statusCode = 200, msg = "success", count = res.data.Items?.Count ?? 0, data = res.data.Items });
        }
    }
}