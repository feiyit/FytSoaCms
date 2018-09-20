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

namespace FytSoa.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class StockController : Controller
    {
        private readonly IErpInOutLogService _inOutLogService;
        private readonly IErpPackLogService _packLogService;
        private readonly IErpBackGoodsService _backGoodsService;
        private readonly IErpReturnOrderService _returnOrderService;
        private readonly IErpReturnGoodsService _returnGoodsService;
        private readonly IErpTransferService _transferService;
        private readonly IErpTransferGoodsService _transferGoodsService;
        public StockController(IErpInOutLogService inOutLogService,
            IErpPackLogService packLogService,
            IErpBackGoodsService backGoodsService,
            IErpReturnOrderService returnOrderService,
            IErpReturnGoodsService returnGoodsService,
            IErpTransferService transferService,
            IErpTransferGoodsService transferGoodsService)
        {
            _inOutLogService = inOutLogService;
            _packLogService = packLogService;
            _backGoodsService = backGoodsService;
            _returnOrderService = returnOrderService;
            _returnGoodsService = returnGoodsService;
            _transferService = transferService;
            _transferGoodsService = transferGoodsService;
        }

        #region 出入库管理Api
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("inoutlist")]
        public async Task<JsonResult> GetInPages(PageParm parm, SearchParm searchParm)
        {
            var res = await _inOutLogService.GetPagesAsync(parm,searchParm);
            return Json(new { code = 0, msg = "success", count = res.data?.TotalItems, data = res.data?.Items });
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("listbyshopguid")]
        public async Task<JsonResult> GetInPagesByShopGuid(PageParm parm, string outShopGuid)
        {
            var res = await _inOutLogService.GetByInOutShopPagesAsync(parm, outShopGuid);
            return Json(new { code = 0, msg = "success", count = res.data?.TotalItems, data = res.data?.Items });
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <returns></returns>
        [HttpPost("batchaddinout")]
        public async Task<ApiResult<string>> BatchAddInOutAsync(string guid, string json, string adminGuid)
        {
            return await _inOutLogService.AddBatchAsync(guid,json,adminGuid);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost("addinout")]
        public async Task<ApiResult<string>> AddInOutAsync(ErpInOutLog parm)
        {
            return await _inOutLogService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delinout")]
        public async Task<ApiResult<string>> DeleteIn(string parm)
        {
            return await _inOutLogService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("editinout")]
        public async Task<ApiResult<string>> EditIn(ErpInOutLog parm)
        {
            return await _inOutLogService.ModifyAsync(parm);
        }
        #endregion

        #region 打包日志管理Api
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("packloglist")]
        public async Task<ApiResult<Page<PackLogDto>>> GetPackLogPages(PageParm parm)
        {
            return await _packLogService.GetPagesAsync(parm);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost("addpacklog")]
        public async Task<ApiResult<string>> AddPackLogAsync(ErpPackLog parm)
        {
            return await _packLogService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delpacklog")]
        public async Task<ApiResult<string>> DeletePackLog(string parm)
        {
            return await _packLogService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("editpacklog")]
        public async Task<ApiResult<string>> EditPackLog(ErpPackLog parm)
        {
            return await _packLogService.ModifyAsync(parm);
        }
        #endregion

        #region 调拨单管理Api
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("transferlist")]
        public async Task<JsonResult> GetTransferPages(PageParm parm)
        {
            var res = await _transferService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data?.TotalItems, data = res.data?.Items });
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost("addtransfer")]
        public async Task<ApiResult<string>> AddTransferAsync(ErpTransfer parm)
        {
            return await _transferService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("deltransfer")]
        public async Task<ApiResult<string>> DeleteTransfer(string parm)
        {
            return await _transferService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edittransfer")]
        public async Task<ApiResult<string>> EditTransfer(ErpTransfer parm)
        {
            return await _transferService.ModifyAsync(parm);
        }
        #endregion

        #region 调拨单商品管理Api
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("transfergoodslist")]
        public async Task<JsonResult> GetTransferGoodsPages(PageParm parm)
        {
            var res = await _transferGoodsService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data?.TotalItems, data = res.data?.Items });
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost("addtransfergoods")]
        public async Task<ApiResult<string>> AddTransferGoodsAsync(ErpTransferGoods parm,List<TransferGoods> list)
        {
            return await _transferGoodsService.AddAsync(parm,list);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("deltransfergoods")]
        public async Task<ApiResult<string>> DeleteTransferGoods(string parm)
        {
            return await _transferGoodsService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edittransfergoods")]
        public async Task<ApiResult<string>> EditTransferGoods(ErpTransferGoods parm)
        {
            return await _transferGoodsService.ModifyAsync(parm);
        }
        #endregion

        #region 退货
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("backlist")]
        public async Task<JsonResult> GetBackPages(PageParm parm, SearchParm searchParm)
        {
            var res = await _backGoodsService.GetPagesAsync(parm, searchParm);
            return Json(new { code = 0, msg = "success", count = res.data?.TotalItems, data = res.data?.Items });
        }
        /// <summary>
        /// 修改退货状态
        /// </summary>
        /// <param name="parm">返货商品信息</param>
        /// <returns></returns>
        [HttpPost("back/updatestatus")]
        public async Task<ApiResult<string>> UpdateBackGoodsStatus(ErpBackGoods parm)
        {
            return await _backGoodsService.ModifyStatusAsync(parm);
        }
        #endregion

        #region 返货订单列表
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("return/order")]
        public async Task<ApiResult<Page<ReturnOrderDto>>> GetReturnOrderPages(PageParm parm)
        {
            return await _returnOrderService.GetPagesAsync(parm);
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("return/goods")]
        public async Task<JsonResult> GetReturnGoodsPages(PageParm parm, SearchParm searchParm)
        {
            var res = await _returnGoodsService.GetPagesAsync(parm, searchParm);
            return Json(new { code = 0, msg = "success", count = res.data?.TotalItems, data = res.data?.Items });
        }

        /// <summary>
        /// 添加返货信息，包括返货订单和返货订单里面的商品
        /// </summary>
        /// <param name="parm">订单信息</param>
        /// <param name="goodsJson">返货订单商品Json字符串</param>
        /// <returns></returns>
        [HttpPost("return/add")]
        public Task<ApiResult<string>> AddReturnOrder(ErpReturnOrder parm, string goodsJson)
        {
            return _returnOrderService.AddAsync(parm, goodsJson);
        }

        /// <summary>
        /// 修改返货商品状态
        /// </summary>
        /// <param name="parm">返货商品信息</param>
        /// <returns></returns>
        [HttpPost("return/updatestatus")]
        public Task<ApiResult<string>> UpdateReturnGoodsStatus(ErpReturnGoods parm)
        {
            return _returnGoodsService.ModifyStatusAsync(parm);
        }
        #endregion
    }
}