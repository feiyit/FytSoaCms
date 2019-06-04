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
    public class ColumnController : Controller
    {
        private readonly ICmsColumnService _columnService;
        private readonly ICmsTemplateService _tempService;
        public ColumnController(ICmsColumnService columnService, ICmsTemplateService tempService)
        {
            _columnService = columnService;
            _tempService = tempService;
        }
        #region 栏目管理
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("getpages")]
        public JsonResult GetPages(PageParm parm)
        {
            parm.site = SiteTool.CurrentSite?.Guid;
            var list = _columnService.RecursiveModule(_columnService.GetListAsync(m => m.SiteGuid == parm.site, m => m.Sort, DbOrderEnum.Asc).Result.data);
            foreach (var item in list)
            {
                item.Title = Utils.LevelName(item.Title, item.ClassLayer);
            }
            return Json(new { code = 0, msg = "success", count = 1, data = list });
        }

        /// <summary>
        /// 查询Tree
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("tree")]
        public async Task<ApiResult<List<ColumnTree>>> GetTree(int type=1)
        {
            return await _columnService.TreeAsync(type, SiteTool.CurrentSite?.Guid);
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ApiResult<string>> Add([FromBody]CmsColumn parm)
        {
            parm.SiteGuid = SiteTool.CurrentSite?.Guid;
            return await _columnService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<ApiResult<string>> Delete([FromBody]ParmString obj)
        {
            return await _columnService.DeleteAsync(obj.parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<ApiResult<string>> Edit([FromBody]CmsColumn parm)
        {
            parm.SiteGuid = SiteTool.CurrentSite?.Guid;
            return await _columnService.UpdateAsync(parm);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <returns></returns>
        [HttpPost("sort")]
        public async Task<ApiResult<string>> ColStor([FromBody]ParmSort obj)
        {
            return await _columnService.ColSort(obj.p,obj.i,obj.o);
        }
        #endregion

        #region 模板管理

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("template/getpages")]
        public async Task<JsonResult> GetTemplatePages(PageParm parm)
        {
            var res = await _tempService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data.TotalItems, data = res.data.Items });
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("template/add")]
        public async Task<ApiResult<string>> AddTemplate([FromBody]CmsTemplate parm)
        {
            return await _tempService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("template/delete")]
        public async Task<ApiResult<string>> DeleteTemplate([FromBody]ParmString obj)
        {
            return await _tempService.DeleteAsync(obj.parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("template/edit")]
        public async Task<ApiResult<string>> EditTemplate([FromBody]CmsTemplate parm)
        {
            return await _tempService.UpdateAsync(parm);
        }
        #endregion
    }
}