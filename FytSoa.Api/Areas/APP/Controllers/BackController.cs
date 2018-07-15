using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Erp;
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
        public JsonResult BackGoodsList(PageParm parm, SearchParm searchParm)
        {
            var res = _backService.GetPagesAsync(parm,searchParm).Result;
            var list = res.data.Items?.Select(m => new {
                m.Code,
                m.BackCount,
                GoodsName = m.BrandName + m.StyleName + m.SeasonName,
                AddDate = m.AddDate.ToShortDateString().Replace("/", "-")
            });
            return Json(new { statusCode = 200, msg = "success", count = res.data.TotalPages, data = list });
        }

        /// <summary>
        /// 添加返货信息，包括返货订单和返货订单里面的商品
        /// </summary>
        /// <param name="parm">订单信息</param>
        /// <param name="goodsJson">返货订单商品Json字符串</param>
        /// <returns></returns>
        [HttpPost("add/goods")]
        public Task<ApiResult<string>> AddBackGoods(ErpBackGoods parm)
        {
            return _backService.AddAsync(parm);
        }
    }
}