using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers.Cms
{
    [Produces("application/json")]
    [Route("api/localfiles")]
    public class LocalFilesController : Controller
    {
        private readonly ICmsImageService _imageService;
        public LocalFilesController(ICmsImageService imageService)
        {
            _imageService = imageService;
        }

        /// <summary>
        /// 本地上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost("upload")]
        public ApiResult<string> LocalUpload(IFormFile file,string path)
        {
            var res = new ApiResult<string>() { statusCode=(int)ApiEnum.Error};
            try
            {
                var s = HttpContext.Request.Form["path"];
                if (path == "/")
                {
                    res.message = "请选择分类";
                    return res;
                }
                //原文件名
                var filename = file.FileName;
                //扩展名
                var fileExt = FileHelper.GetFileExt(filename);
                //自定义保存文件地址
                var npath = "wwwroot/upload/"+path+"/";
                //检查物理路径是否存在 不存在则创建
                FileHelperCore.CreateFiles(npath);
                using (var stream = new FileStream(npath + filename, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Flush();
                }
                res.statusCode = (int)ApiEnum.Status;
                res.data = "/upload/"+path+"/"+ filename;
                //图片保存到数据库里面
                var model = _imageService.GetModelAsync(m=>m.ImgBig==res.data).Result.data;
                if (string.IsNullOrEmpty(model.Guid))
                {
                    _imageService.AddAsync(new CmsImage() { Guid = Guid.NewGuid().ToString(),ImgSize=file.Length,ImgType= fileExt, ImgBig = res.data });
                }
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }

        /// <summary>
        /// 获得图片列表
        /// </summary>
        /// <param name="parm">分页参数</param>
        /// <returns></returns>
        [HttpPost("list")]
        public CloudFile LocalList(PageParm parm) {
            return _imageService.GetList(parm);
        }

        /// <summary>
        /// 删除本地图片列表
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <returns></returns>
        [HttpPost("delete")]
        public Task<ApiResult<string>> DeleteList(string filename)
        {
            return _imageService.DeleteAsync(m => m.ImgBig == filename);
        }
    }
}