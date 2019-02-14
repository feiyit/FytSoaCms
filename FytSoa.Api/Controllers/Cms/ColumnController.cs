using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers.Cms
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize(Roles = "Admin")]
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
        public async Task<JsonResult> GetPages(PageParm parm)
        {
            var list = _columnService.RecursiveModule(_columnService.GetListAsync().Result.data);
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
            return await _columnService.TreeAsync(type);
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ApiResult<string>> Add(CmsColumn parm)
        {
            return await _columnService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<ApiResult<string>> Delete(string parm)
        {
            return await _columnService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<ApiResult<string>> Edit(CmsColumn parm)
        {
            return await _columnService.UpdateAsync(parm);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <returns></returns>
        [HttpPost("sort")]
        public async Task<ApiResult<string>> ColStor(int p,int i,int o)
        {
            return await _columnService.ColSort(p,i,o);
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
        public async Task<ApiResult<string>> AddTemplate(CmsTemplate parm)
        {
            return await _tempService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("template/delete")]
        public async Task<ApiResult<string>> DeleteTemplate(string parm)
        {
            return await _tempService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("template/edit")]
        public async Task<ApiResult<string>> EditTemplate(CmsTemplate parm)
        {
            return await _tempService.UpdateAsync(parm);
        }
        #endregion
    }
}