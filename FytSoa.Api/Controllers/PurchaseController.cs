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
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PurchaseController : Controller
    {
        private readonly IErpPurchaseService _purchaseService;
        private readonly IErpPurchaseGoodsService _purchaseGoodsService;
        private readonly IErpSupplierService _supplierService;
        public PurchaseController(IErpPurchaseService purchaseService
            , IErpPurchaseGoodsService purchaseGoodsService
            , IErpSupplierService supplierService)
        {
            _purchaseService = purchaseService;
            _purchaseGoodsService = purchaseGoodsService;
            _supplierService = supplierService;
        }

        #region 采购单Api
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("purchaselist")]
        public async Task<JsonResult> GetPurchasePages(PageParm parm)
        {
            var res = await _purchaseService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data?.Items.Count, data = res.data?.Items });
        }

        /// <summary>
        /// 添加一条菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost("addpurchase")]
        public async Task<ApiResult<string>> AddPurchase(ErpPurchase parm)
        {
            return await _purchaseService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("deletepurchase")]
        public async Task<ApiResult<string>> DeletePurchase(string parm)
        {
            return await _purchaseService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("editpurchase")]
        public async Task<ApiResult<string>> EditPurchase(ErpPurchase parm)
        {
            return await _purchaseService.ModifyAsync(parm);
        }

        /// <summary>
        /// 修改采购单状态
        /// </summary>
        /// <returns></returns>
        [HttpPost("modifystatus")]
        public async Task<ApiResult<string>> ModifyStatusAsync(string parm)
        {
            return await _purchaseService.ModifyStatusAsync(parm);
        }
        #endregion

        #region 采购单商品Api
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("goodslist")]
        public async Task<JsonResult> GetGoodsPages(PageParm parm)
        {
            var res = await _purchaseGoodsService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data.Items.Count, data = res.data.Items });
        }

        /// <summary>
        /// 添加一条菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost("addgoods")]
        public async Task<ApiResult<string>> AddGoods(ErpPurchaseGoods parm)
        {
            return await _purchaseGoodsService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("deletegoods")]
        public async Task<ApiResult<string>> DeleteGoods(string parm)
        {
            return await _purchaseGoodsService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("editgoods")]
        public async Task<ApiResult<string>> EditGoods(ErpPurchaseGoods parm)
        {
            return await _purchaseGoodsService.ModifyAsync(parm);
        }
        #endregion

        #region 供货商Api
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("supplierlist")]
        public async Task<JsonResult> GetSupplierPages(PageParm parm)
        {
            var res = await _supplierService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data.Items.Count, data = res.data.Items });
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("supplierlist")]
        public async Task<ApiResult<Page<ErpSupplier>>> GetSupplierList(PageParm parm)
        {
            return await _supplierService.GetPagesAsync(parm);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("supplierbyid")]
        public async Task<ApiResult<ErpSupplier>> GetSupplierByGuid(string parm)
        {
            return await _supplierService.GetByGuidAsync(parm);
        }

        /// <summary>
        /// 添加一条菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost("addsupplier")]
        public async Task<ApiResult<string>> AddSupplier(ErpSupplier parm)
        {
            return await _supplierService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("deletesupplier")]
        public async Task<ApiResult<string>> DeleteSupplier(string parm)
        {
            return await _supplierService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("editsupplier")]
        public async Task<ApiResult<string>> EditSupplier(ErpSupplier parm)
        {
            return await _supplierService.ModifyAsync(parm);
        }
        #endregion
    }
}