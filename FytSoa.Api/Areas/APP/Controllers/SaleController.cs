using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace FytSoa.Api.Areas.APP.Controllers
{
    /// <summary>
    /// 销售
    /// </summary>
    [Route("app/api/[controller]")]
    [Produces("application/json")]
    public class SaleController : Controller
    {
        private readonly IErpSaleOrderService _orderService;
        private readonly IErpSaleOrderGoodsService _goodsService;
        static NLog.Logger _log = LogManager.GetCurrentClassLogger();
        public SaleController(IErpSaleOrderService orderService, IErpSaleOrderGoodsService goodsService)
        {
            _orderService = orderService;
            _goodsService = goodsService;
        }

        /// <summary>
        /// 销售订单列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("order/list")]
        public JsonResult SaleOrderList(PageParm parm,AppSearchParm searchParm)
        {
            var res = _orderService.GetPagesAsync(parm, searchParm).Result;
            var list = res.data.Items?.Select(m => new {
                m.Number,
                m.Counts,
                m.RealMoney,
                m.Goods,
                AddDate = m.AddDate.ToString().Replace("/", "-").Replace("T"," ")
            });
            return Json(new { statusCode = 200, msg = "success", count = res.data.TotalPages, data = list });
        }

        /// <summary>
        /// 销售订单商品列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("goods/list")]
        public JsonResult SaleOrderGoodsList(PageParm parm,SearchParm searchParm)
        {
            var res = _goodsService.GetPagesAsync(parm, searchParm).Result;
            return Json(new { statusCode = 200, msg = "success", count = res.data.TotalPages, data = res.data.Items });
        }

        /// <summary>
        /// 添加返货信息，包括返货订单和返货订单里面的商品
        /// </summary>
        /// <param name="parm">订单信息</param>
        /// <param name="goodsJson">返货订单商品Json字符串</param>
        /// <returns></returns>
        [HttpPost("add/order")]
        public Task<ApiResult<string>> AddSaleOrder(ErpSaleOrder parm, string goodsJson)
        {
            return _orderService.AddAsync(parm, goodsJson);
        }

        /// <summary>
        /// 根据编号，查询订单信息
        /// </summary>
        /// <param name="number">订单编号</param>
        /// <returns></returns>
        [HttpPost("bynumber")]
        public Task<ApiResult<SaleOrderApp>> GetSaleOrderByNumber(string number)
        {
            return _orderService.GetByNumberAsync(number);
        }

        /// <summary>
        /// 根据编号，查询订单信息
        /// </summary>
        /// <param name="number">订单编号</param>
        /// <returns></returns>
        [HttpPost("uploadimg")]
        public ApiResult<string> UploadImg(string orderNumber)
        {
            //_log.Info("参数："+imgId+"----------"+imgtype);
            var res = new ApiResult<string>();
            try
            {
                var file = Request.Form.Files[0];
                string upload_path = Directory.GetCurrentDirectory() + "/wwwroot";
                var imgPath = upload_path + "/app/sale/";
                if (!Directory.Exists(imgPath))
                {
                    //如果不存在就创建file文件夹
                    Directory.CreateDirectory(imgPath);
                }
                var path = Path.Combine(imgPath, file.FileName);
                //_log.Info(path);
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    //修改数据
                    _orderService.UpdateOrderAddImage(orderNumber, "/app/sale/"+file.FileName);
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }
    }
}