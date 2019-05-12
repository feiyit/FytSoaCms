using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers.Cms
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CloudFilesController : Controller
    {
        private readonly ICmsImgTypeService _imgTypeService;
        public CloudFilesController(ICmsImgTypeService imgTypeService)
        {
            _imgTypeService = imgTypeService;
        }

        /// <summary>
        /// 获得云端图片列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("token")]
        public CloudFile GetToken()
        {
            return QiniuCloud.GetToken();
        }

        /// <summary>
        /// 获得云端图片列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("list")]
        public CloudFile FileList([FromBody]QiniuListParmDto obj)
        {
            return QiniuCloud.List(obj.prefix, obj.marker);
        }

        /// <summary>
        /// 删除云端图片列表
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <returns></returns>
        [HttpPost("delete")]
        public CloudFile DeleteList([FromBody]QiniuDelParmDto obj)
        {
            return QiniuCloud.Delete(obj.filename);
        }

        /// <summary>
        /// 删除云端图片列表
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <returns></returns>
        [HttpPost("upload")]
        public CloudFile UpLoadFile([FromBody]QiniuDelByPathParmDto obj)
        {
            return QiniuCloud.UploadFile(obj.prefix, obj.filepath);
        }

        /// <summary>
        /// 添加图片类型
        /// </summary>
        /// <param name="model">CmsImgType</param>
        /// <returns></returns>
        [HttpPost("type/add")]
        public async Task<ApiResult<string>> AddImageType([FromBody]CmsImgType model)
        {
            if (string.IsNullOrEmpty(model.Guid))
            {
                model.Guid = Guid.NewGuid().ToString();
                model.AddDate = DateTime.Now;
                model.Level = string.IsNullOrEmpty(model.ParentGuid) ? 0 : 1;
            }
            return await _imgTypeService.AddAsync(model);
        }

        /// <summary>
        /// 删除图片类型
        /// </summary>
        /// <param name="parm">string parm</param>
        /// <returns></returns>
        [HttpPost("type/del")]
        public async Task<ApiResult<string>> DelImageType([FromBody]ParmString obj)
        {
            return await _imgTypeService.DeleteAsync(obj.parm);
        }

        /// <summary>
        /// 修改图片类型
        /// </summary>
        /// <param name="model">CmsImgType</param>
        /// <returns></returns>
        [HttpPost("type/modify")]
        public async Task<ApiResult<string>> EditImageType([FromBody]CmsImgType model)
        {
            return await _imgTypeService.UpdateAsync(model);
        }

        /// <summary>
        /// 获得图片类型列表
        /// </summary>
        /// <param name="parm">PageParm parm</param>
        /// <returns></returns>
        [HttpPost("type/list")]
        public async Task<ApiResult<List<CmsImgType>>> ListImageType([FromBody]PageParm parm)
        {
            return await _imgTypeService.GetListAsync(m=>m.Type==parm.types,m=>m.AddDate,DbOrderEnum.Asc);
        }

        /// <summary>
        /// 本地上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost("localupload")]
        //[Consumes("application/json", "text/html")]
        public ApiResult<string> LocalUpload()
        {
            var res = new ApiResult<string>();
            try
            {
                //var file = HttpContext.Request.Form.Files["upfile"];
                res.message = HttpContext.Request.Form.Files.Count.ToString();
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }
    }
}