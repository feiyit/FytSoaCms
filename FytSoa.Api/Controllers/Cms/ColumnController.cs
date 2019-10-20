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
    public class ColumnController : ControllerBase
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
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("getpages")]
        public IActionResult GetPages([FromQuery]PageParm parm)
        {
            parm.site = SiteTool.CurrentSite?.Guid;
            var list = _columnService.RecursiveModule(_columnService.GetListAsync(m => m.SiteGuid == parm.site, m => m.Sort, DbOrderEnum.Asc).Result.data);
            foreach (var item in list)
            {
                item.Title = Utils.LevelName(item.Title, item.ClassLayer);
            }
            return Ok(new { code = 0, msg = "success", count = 1, data = list });
        }

        /// <summary>
        /// 查询Tree
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("tree")]
        public async Task<IActionResult> GetTree([FromBody]PageParm param)
        {
            return Ok(await _columnService.TreeAsync(param.types, SiteTool.CurrentSite?.Guid));
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]CmsColumn parm)
        {
            parm.SiteGuid = SiteTool.CurrentSite?.Guid;
            return Ok(await _columnService.AddAsync(parm));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody]ParmString obj)
        {
            return Ok(await _columnService.DeleteAsync(obj.parm));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody]CmsColumn parm)
        {
            parm.SiteGuid = SiteTool.CurrentSite?.Guid;
            return Ok(await _columnService.UpdateAsync(parm));
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <returns></returns>
        [HttpPost("sort")]
        public async Task<IActionResult> ColStor([FromBody]ParmSort obj)
        {
            return Ok(await _columnService.ColSort(obj.p, obj.i, obj.o));
        }
        #endregion

        #region 模板管理

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("template/getpages")]
        public async Task<IActionResult> GetTemplatePages([FromQuery]PageParm parm)
        {
            var res = await _tempService.GetPagesAsync(parm);
            return Ok(new { code = 0, msg = "success", count = res.data.TotalItems, data = res.data.Items });
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("template/add")]
        public async Task<IActionResult> AddTemplate([FromBody]CmsTemplate parm)
        {
            return Ok(await _tempService.AddAsync(parm));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("template/delete")]
        public async Task<IActionResult> DeleteTemplate([FromBody]ParmString obj)
        {
            return Ok(await _tempService.DeleteAsync(obj.parm));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("template/edit")]
        public async Task<IActionResult> EditTemplate([FromBody]CmsTemplate parm)
        {
            return Ok(await _tempService.UpdateAsync(parm));
        }
        #endregion
    }
}