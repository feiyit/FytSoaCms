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
    [Route("app/api/[controller]")]
    [Produces("application/json")]
    public class IndexController : Controller
    {
        private readonly IErpReturnGoodsService _returnService;
        private readonly ISysCodeService _codeService;
        public IndexController(IErpReturnGoodsService returnService, ISysCodeService codeService)
        {
            _returnService = returnService;
            _codeService = codeService;
        }

        /// <summary>
        /// 查询品牌
        /// </summary>
        /// <returns></returns>
        [HttpPost("brank")]
        public JsonResult GetBrank()
        {
            var res = _codeService.GetPagesAsync(new Service.DtoModel.SysCodePostPage() { limit = 10000, guid = "7b664e3e-f58a-4e66-8c0f-be1458541d14" })
                .Result.data?.Items
                .Select(m=>new {
                    m.Guid,
                    m.Name
                });
            return Json(new { statusCode = 200, data = res });
        }
    }
}