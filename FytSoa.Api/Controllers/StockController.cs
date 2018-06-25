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
    }
}