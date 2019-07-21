using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers.Cms
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [JwtAuthorize(Roles = "Admin")]
    public class AdvController : Controller
    {
        private readonly ICmsAdvClassService _classService;
        private readonly ICmsAdvListService _listService;
        public AdvController(ICmsAdvClassService classService, ICmsAdvListService listService)
        {
            _classService = classService;
            _listService = listService;
        }

        #region 广告栏位管理

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("class/page")]
        public async Task<ApiResult<List<CmsAdvClass>>> GetClassPages()
        {
            var siteGuid = SiteTool.CurrentSite?.Guid; ;
            return await _classService.GetListAsync(m=>m.SiteGuid== siteGuid, m=>m.Guid,DbOrderEnum.Asc);
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("class/add")]
        public async Task<ApiResult<string>> AddClass([FromBody]CmsAdvClass parm)
        {
            parm.SiteGuid = SiteTool.CurrentSite?.Guid;
            parm.Guid = Guid.NewGuid().ToString();
            return await _classService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("class/delete")]
        public async Task<ApiResult<string>> DeleteClass([FromBody]ParmString obj)
        {
            return await _classService.DeleteAsync(obj.parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("class/edit")]
        public async Task<ApiResult<string>> EditClass([FromBody]CmsAdvClass parm)
        {
            parm.SiteGuid = SiteTool.CurrentSite?.Guid;
            return await _classService.UpdateAsync(parm);
        }
        #endregion

        #region 广告位管理

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("list/page")]
        public async Task<JsonResult> GetAdvListPages(PageParm parm)
        {            
            var res = await _listService.GetListAsync(m=>m.ClassGuid==parm.key,m=>m.Sort,DbOrderEnum.Desc);
            return Json(new { code = 0, msg = "success", count = 1, res.data });
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("list/add")]
        public async Task<ApiResult<string>> AddAdvList([FromBody]CmsAdvList parm)
        {
            parm.Guid = Guid.NewGuid().ToString();
            return await _listService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("list/delete")]
        public async Task<ApiResult<string>> DeleteAdvList([FromBody]ParmString obj)
        {
            return await _listService.DeleteAsync(obj.parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("list/edit")]
        public async Task<ApiResult<string>> EditAdvList([FromBody]CmsAdvList parm)
        {
            return await _listService.UpdateAsync(parm);
        }
        #endregion
    }
}