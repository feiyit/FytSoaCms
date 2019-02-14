using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    /// <summary>
    /// 文件上传API
    /// </summary>
    [Produces("application/json")]
    [Route("api/upload")]
    public class FileUploadController : Controller
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("index")]
        public JsonResult Index(IFormFile file)
        {
            //原文件名
            var filename = file.FileName;
            //扩展名
            var fileExt = FileHelper.GetFileExt(filename);
            var path =Utils.AssigendPath(fileExt, "wwwroot");           
            //检查物理路径是否存在 不存在则创建
            FileHelperCore.CreateFiles(path);
            using (var stream = new FileStream(path+ filename, FileMode.Create))
            {
                file.CopyTo(stream);
                stream.Flush();
            }
            return Json(new {code=200,data=path+filename });
        }
    }
}