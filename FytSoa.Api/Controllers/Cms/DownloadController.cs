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
    public class DownloadController : Controller
    {
        private readonly ICmsDownloadService _downloadService;
        public DownloadController(ICmsDownloadService downloadService)
        {
            _downloadService = downloadService;
        }

        #region 文章管理
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("getpages")]
        public JsonResult GetPages(PageParm parm)
        {
            var res = _downloadService.GetList(parm);
            return Json(new { code = 0, msg = "success", count = res.TotalItems, data = res.Items });
        }


        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ApiResult<string>> Add(CmsDownload parm)
        {
            //处理文件类型
            parm.FileType= FileHelper.GetFileExt(parm.FileUrl);
            return await _downloadService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<ApiResult<string>> Delete(string parm)
        {
            return await _downloadService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<ApiResult<string>> Edit(CmsDownload parm)
        {
            //处理文件类型
            parm.FileType = FileHelper.GetFileExt(parm.FileUrl);
            return await _downloadService.UpdateAsync(parm);
        }
        #endregion
    }
}