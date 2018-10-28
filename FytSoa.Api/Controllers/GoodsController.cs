using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Erp;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class GoodsController : Controller
    {
        private readonly IErpGoodsSkuService _skuGoodsService;
        private readonly IErpGoodsService _goodsService;
        private readonly IErpSkuLossService _lossService;
        private readonly IErpSkuLossOrderService _lossOrderService;
        public GoodsController(IErpGoodsSkuService skuGoodsService,
            IErpGoodsService goodsService, IErpSkuLossService lossService, IErpSkuLossOrderService lossOrderService)
        {
            _skuGoodsService = skuGoodsService;
            _goodsService = goodsService;
            _lossService = lossService;
            _lossOrderService = lossOrderService;
        }

        #region 条形码Api
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("barlist")]
        public async Task<JsonResult> GetBarCodePages(PageParm parm)
        {
            var res = await _skuGoodsService.GetPagesAsync(parm);            
            return Json(new { code = 0, msg = "success", count = res.data?.TotalItems, data = res.data?.Items });
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("addbarcode")]
        public async Task<ApiResult<string>> AddMenu(ErpGoodsSku parm)
        {
            return await _skuGoodsService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delbarcode")]
        public async Task<ApiResult<string>> DeleteBarCode(string parm)
        {
            return await _skuGoodsService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("editbarcode")]
        public async Task<ApiResult<string>> EditBarCode(ErpGoodsSku parm)
        {
            return await _skuGoodsService.ModifyAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("bycode")]
        public async Task<ApiResult<GoodsSkuDto>> GetCodeByShopSku(string shopGuid, string code)
        {
            return await _skuGoodsService.GetByCodeAsync(shopGuid, code);
        }

        /// <summary>
        /// 根据条形码获得条形码信息，不需要根据加盟商
        /// </summary>
        /// <returns></returns>
        [HttpPost("getsku")]
        public async Task<ApiResult<ErpGoodsSku>> GetSkuByCode(string code)
        {
            return await _skuGoodsService.GetByGuidAsync(code);
        }
        #endregion

        #region 商品管理Api
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("goodslist")]
        public async Task<JsonResult> GetGoodsPages(PageParm parm)
        {
            var res = await _goodsService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data.TotalItems, data = res.data.Items });
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("addgoods")]
        public async Task<ApiResult<string>> AddGoods(ErpGoods parm)
        {
            return await _goodsService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delgoods")]
        public async Task<ApiResult<string>> DeleteGoods(string parm)
        {
            return await _goodsService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("editgoods")]
        public async Task<ApiResult<string>> EditGoods(ErpGoods parm)
        {
            return await _goodsService.ModifyAsync(parm);
        }
        #endregion

        #region 报损Api
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("loss/list")]
        public async Task<JsonResult> GetLossPages(PageParm parm)
        {
            var res = await _lossService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data.TotalItems, data = res.data.Items });
        }

        /// <summary>
        /// 添加报损
        /// </summary>
        /// <returns></returns>
        [HttpPost("addloss")]
        public async Task<ApiResult<string>> AddLoss(ErpSkuLoss parm)
        {
            return await _lossService.AddAsync(parm);
        }

        /// <summary>
        /// 添加报损
        /// </summary>
        /// <returns></returns>
        [HttpPost("modifyloss")]
        public async Task<ApiResult<string>> ModifyLoss(ErpSkuLoss parm)
        {
            return await _lossService.ModifyAsync(parm);
        }

        #endregion

        #region 报损订单Api
        /// <summary>
        /// 查询列表报损订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("lossorder/list")]
        public async Task<JsonResult> GetLossOrderPages(PageParm parm)
        {
            var res = await _lossOrderService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data.TotalItems, data = res.data.Items });
        }

        /// <summary>
        /// 添加报损订单
        /// </summary>
        /// <returns></returns>
        [HttpPost("addlossorder")]
        public async Task<ApiResult<string>> AddLossOrder(ErpSkuLossOrder parm)
        {
            return await _lossOrderService.AddAsync(parm);
        }

        /// <summary>
        /// 修改报损订单
        /// </summary>
        /// <returns></returns>
        [HttpPost("modifylossorder")]
        public async Task<ApiResult<string>> ModifyLossOrder(ErpSkuLossOrder parm)
        {
            return await _lossOrderService.ModifyAsync(parm);
        }

        /// <summary>
        /// 删除报损订单
        /// </summary>
        /// <returns></returns>
        [HttpPost("dellossorder")]
        public async Task<ApiResult<string>> DeleteLossOrder(string parm)
        {
            return await _lossOrderService.DeleteAsync(parm);
        }
        #endregion
    }
}