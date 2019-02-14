using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers.Cms
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize(Roles = "Admin")]
    public class CmsSiteController : Controller
    {
        private readonly ICmsSiteService _siteService;
        public CmsSiteController(ICmsSiteService siteService)
        {
            _siteService = siteService;
        }

        /// <summary>
        /// 保存站点信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("savesite")]
        public async Task<ApiResult<string>> SaveSite(CmsSite parm)
        {
            return await _siteService.UpdateAsync(parm);
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("backups")]
        public async Task<ApiResult<string>> DbBackups()
        {
            var path = FileHelperCore.MapPath("/wwwroot/db_back/") + DateTime.Now.ToString("yyyyMMddHHmmss") + ".sql";
            var res = new ApiResult<string>() { statusCode=(int)ApiEnum.Error};
            var thread =new System.Threading.Thread(
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
        public ApiResult<string> DeleteDbBackupsFile(string filename)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var str = Utils.StrToListString(filename);
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