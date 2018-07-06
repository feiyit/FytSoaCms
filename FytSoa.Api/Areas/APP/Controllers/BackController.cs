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
    /// 退货
    /// </summary>
    [Route("app/api/[controller]")]
    [Produces("application/json")]
    public class BackController : Controller
    {
        private readonly IErpBackGoodsService _backService;
        public BackController(IErpBackGoodsService backService)
        {
            _backService = backService;
        }

        /// <summary>
        /// 退货列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("list")]
        public JsonResult BackGoodsList(PageParm parm)
        {
            var res = _backService.GetPagesAsync(parm).Result;
            
            return Json(new { statusCode = 200, msg = "success", count = res.data.Items?.Count ?? 0, data = res.data.Items });
        }
    }
}