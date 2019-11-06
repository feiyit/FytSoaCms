﻿using System;
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
    public class FileUploadController : ControllerBase
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("index")]
        public IActionResult Index(IFormFile file)
        {
            //原文件名
            var filename = file.FileName;
            //扩展名
            var fileExt = FileHelper.GetFileExt(filename);
            //判断是否包含盘符： 文件名不允许包含冒号，如果存在，则使用新的文件名字
            if (filename.Contains(":"))
            {
                filename = Guid.NewGuid() + "."+fileExt;
            }
            //根据类型创建文件夹
            var path =Utils.AssigendPath(fileExt, "wwwroot");           
            //检查物理路径是否存在 不存在则创建
            FileHelperCore.CreateFiles(path);
            using (var stream = new FileStream(path+ filename, FileMode.Create))
            {
                file.CopyTo(stream);
                stream.Flush();
            }
            return Ok(new {code=200,data=path+ filename });
        }
    }
}