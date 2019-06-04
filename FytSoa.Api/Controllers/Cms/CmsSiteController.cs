using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core;
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
    public class CmsSiteController : Controller
    {
        private readonly ICmsSiteService _siteService;
        public CmsSiteController(ICmsSiteService siteService)
        {
            _siteService = siteService;
        }

        /// <summary>
        /// 获得站点列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("list")]
        public async Task<IActionResult> SaveList()
        {
            return Ok(await _siteService.GetListAsync(m => true, m => m.AddTime, DbOrderEnum.Asc));
        }

        /// <summary>
        /// 保存站点信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("savesite")]
        public async Task<ApiResult<string>> SaveSite([FromBody]CmsSite parm)
        {
            if (!string.IsNullOrEmpty(parm.Guid))
            {
                return await _siteService.UpdateAsync(parm);
            }
            else
            {
                parm.Guid = Guid.NewGuid().ToString();
                return await _siteService.AddAsync(parm);
            }
        }

        /// <summary>
        /// 删除站点
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("del")]
        public async Task<IActionResult> DelSite([FromBody]ParmString parm)
        {
            return Ok(await _siteService.DeleteAsync(m => m.Guid == parm.parm));
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("backups")]
        public ApiResult<string> DbBackups()
        {
            var path = FileHelperCore.MapPath("/wwwroot/db_back/") + DateTime.Now.ToString("yyyyMMddHHmmss") + ".sql";
            var res = new ApiResult<string>() { };
            var thread = new System.Threading.Thread(
                                new System.Threading.ParameterizedThreadStart(DbBackup.BackupDb))
            {
                Priority = System.Threading.ThreadPriority.Highest
            };
            thread.Start(path);

            if (thread.ThreadState != System.Threading.ThreadState.Running)
            {
                thread.Abort();
                DbBackup.BackupDb(path);
            }
            else
            {
                res.message = "备份任务正在后台处理，请稍后到数据库恢复菜单中查看";
                System.Threading.Thread.Sleep(1000);
            }
            return res;
        }

        /// <summary>
        /// 获得数据库备份文件
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("backups/files")]
        public JsonResult GetDbBackupsFile()
        {
            var list = FileHelperCore.ResolveFileInfo("/wwwroot/db_back/").OrderByDescending(m=>m.CreateDate).ToList();
            return Json(new { code = 0, msg = "success", count = list.Count, data = list });
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("delete/files")]
        public ApiResult<string> DeleteDbBackupsFile([FromBody]ParmString obj)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var str = Utils.StrToListString(obj.parm);
                foreach (var item in str)
                {
                    FileHelperCore.DeleteFiles("/wwwroot/db_back/"+item);
                }
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }
    }
}