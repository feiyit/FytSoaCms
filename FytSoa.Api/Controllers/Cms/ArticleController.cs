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
    public class ArticleController : ControllerBase
    {
        private readonly ICmsArticleService _articleService;
        public ArticleController(ICmsArticleService articleService)
        {
            _articleService = articleService;
        }
        #region 文章管理
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("getpages")]
        public IActionResult GetPages(PageParm parm)
        {
            parm.site = SiteTool.CurrentSite?.Guid;
            var res = _articleService.GetList(parm);
            return Ok(new { code = 0, msg = "success", count = res.TotalItems, data = res.Items });
        }

        /// <summary>
        /// 回收站操作
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("recycle")]
        public async Task<IActionResult> GoRecycle([FromBody]ArticleOptionParm obj)
        {
            return Ok(await _articleService.GoRecycle(obj.parm, obj.type));
        }

        /// <summary>
        /// 复制/转移
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("copyortransfer")]
        public async Task<IActionResult> GoCopyOrTransfer([FromBody]ArticleOptionParm obj)
        {
            return Ok(await _articleService.GoCopyOrTransfer(obj.parm, obj.type, obj.column));
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]CmsArticle parm)
        {
            parm.SiteGuid = SiteTool.CurrentSite?.Guid;
            return Ok(await _articleService.AddAsync(parm));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody]ParmString obj)
        {
            return Ok(await _articleService.DeleteAsync(obj.parm));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody]CmsArticle parm)
        {
            parm.SiteGuid = SiteTool.CurrentSite?.Guid;
            return Ok(await _articleService.UpdateAsync(parm));
        }
        #endregion
    }
}