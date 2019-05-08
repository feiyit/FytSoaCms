using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Wx;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FytSoa.Api.Controllers.Wx
{
    [Produces("application/json")]
    [Route("api/wx/material")]
    [JwtAuthorize(Roles = "Admin")]
    public class WxMaterialController : Controller
    {
        private readonly IWxMaterialService _meterialService;
        private readonly IWxSettingService _settingService;
        public WxMaterialController(IWxMaterialService meterialService, IWxSettingService settingService)
        {
            _meterialService = meterialService;
            _settingService = settingService;
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("list"), Log("WxMaterial：list", LogType = LogEnum.RETRIEVE)]
        public async Task<ApiResult<Page<WxMaterial>>> GetList(PageParm parm)
        {
            return await _meterialService.GetPageList(parm);
        }

        /// <summary>
        /// 添加一条微信素材
        /// </summary>
        /// <returns></returns>
        [HttpPost("add"), Log("WxMaterial：add", LogType = LogEnum.ADD)]
        public async Task<ApiResult<string>> Add([FromBody]WxMaterial model)
        {
            return await _meterialService.Add(model,null);
        }

        /// <summary>
        ///删除一条微信素材
        /// </summary>
        /// <returns></returns>
        [HttpPost("del"), Log("WxMaterial：del", LogType = LogEnum.DELETE)]
        public async Task<ApiResult<string>> Delete([FromBody]ParmInt obj)
        {
            return await _meterialService.DeleteAsync(m=>m.Id== obj.id);
        }

        /// <summary>
        /// 获得一条微信素材
        /// </summary>
        /// <returns></returns>
        [HttpPost("get"), Log("WxMaterial：get", LogType = LogEnum.RETRIEVE)]
        public async Task<ApiResult<WxMaterial>> GetModel([FromBody]ParmInt obj)
        {
            return await _meterialService.GetModelAsync(m => m.Id == obj.id);
        }

        /// <summary>
        /// 根据公众号获得素材
        /// </summary>
        /// <returns></returns>
        [HttpPost("server"), Log("WxMaterial：server", LogType = LogEnum.ASYWX)]
        public JsonResult GetServerMaterial([FromBody]ParmInt obj)
        {
            var gzhModel = _settingService.GetModelAsync(m=>m.Id== obj.id).Result.data;
            var token = WxTools.GetAccess(gzhModel.AppId, gzhModel.AppSecret);
            var list = WxTools.GetMediaList(token.access_token);
            return Json(list);
        }

        /// <summary>
        /// 同步到微信公众号，没有保存
        /// </summary>
        /// <returns></returns>
        [HttpPost("asynwx"), Log("WxMaterial：asynwx", LogType = LogEnum.ASYWX)]
        public async Task<ApiResult<string>> PushMaterial([FromBody]ParmInt obj)
        {
            var res = new ApiResult<string>();
            //根据公众号查询配置
            var gzhModel = _settingService.GetModelAsync(m => m.Id == obj.id).Result.data;
            var token = WxTools.GetAccess(gzhModel.AppId, gzhModel.AppSecret);

            //提交素材的Url
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/add_news?access_token={0}", token.access_token);
            //定义变量只接受同步成功的素材
            var asynOkList = new List<WxMaterial>();
            //定义标识，是否有素材没有上传成功
            bool isUploadOk = true,asynOk=true;
            //根据公众号查询所有
            var list = _meterialService.GetListAsync(m => m.WxId == obj.id && m.Position == 1, m => m.AddDate, DbOrderEnum.Desc).Result.data;
            if (list.Count > 0)
            {
                //到微信服务端获得thumb_media_id
                foreach (var item in list)
                {
                    var articleList = new List<WxMeterArticle>();
                    item.Position = 2;
                    if (!string.IsNullOrEmpty(item.TestJson))
                    {
                        var resList = JsonConvert.DeserializeObject<List<WxMaterial>>(item.TestJson);
                        foreach (var row in resList)
                        {
                            var fileExt = FileHelperCore.GetFileExtension(row.Img);
                            var resultJson = WxTools.UploadFile(token.access_token, FileHelperCore.MapPath("/wwwroot" + row.Img), fileExt);
                            if (resultJson.code == 200)
                            {
                                articleList.Add(new WxMeterArticle()
                                {
                                    title = row.Title,
                                    thumb_media_id = resultJson.media_id,
                                    author = row.Author,
                                    digest = row.Summary,
                                    content = row.Content,
                                    content_source_url = row.Link
                                });
                            }
                            else
                            {
                                isUploadOk = false;
                            }
                        }
                    }
                    //开始发送到微信
                    var postStr = JsonConvert.SerializeObject(new { articles = articleList });
                    string resMewsJson = WxTools.PostResponse(url, postStr);
                    if (resMewsJson.Contains("errcode")) {
                        asynOk = false;
                    }
                    else
                    {
                        asynOkList.Add(item);
                    }
                }
            }
            if (!isUploadOk)
            {
                res.statusCode = 500;
                res.message = "上传素材失败~";
                return res;
            }
            if (!asynOk)
            {
                res.statusCode = 500;
                res.message = "同步素材失败~";
                return res;
            }
            //只修改同步成功的素材
            if (asynOkList.Count>0)
            {
                await _meterialService.UpdateAsync(list);
            }

            return res;
        }

        /// <summary>
        /// 素材同步到公众号
        /// </summary>
        /// <returns></returns>
        [HttpPost("synchro"), Log("WxMaterial：synchro", LogType = LogEnum.ASYWX)]
        public async Task<ApiResult<string>> PushMaterial([FromBody]WxMaterial model) {
            var res = new ApiResult<string>();

            var gzhModel = _settingService.GetModelAsync(m => m.Id == model.WxId).Result.data;
            var token = WxTools.GetAccess(gzhModel.AppId, gzhModel.AppSecret);

            await _meterialService.Add(model, null);
            var articleList = new List<WxMeterArticle>();
            //定义标识，是否有素材没有上传成功
            var isUploadOk = true;
            //根据公众号查询所有
            var list = _meterialService.GetListAsync(m=>m.WxId==model.WxId && m.Position==1,m=>m.AddDate,DbOrderEnum.Desc).Result.data;
            if (list.Count>0)
            {
                //到微信服务端获得thumb_media_id
                foreach (var item in list)
                {
                    item.Position = 2;
                    if (!string.IsNullOrEmpty(item.TestJson))
                    {
                        var resList = JsonConvert.DeserializeObject<List<WxMaterial>>(item.TestJson);
                        foreach (var row in resList)
                        {
                            var fileExt = FileHelperCore.GetFileExtension(row.Img);
                            var path = row.Img;
                            if (!path.ToLower().StartsWith("http") && !path.ToLower().StartsWith("https"))
                            {
                                path = FileHelperCore.MapPath("/wwwroot" + row.Img);
                            }
                            var resultJson = WxTools.UploadFile(token.access_token, path, fileExt);
                            if (resultJson.code==200)
                            {
                                articleList.Add(new WxMeterArticle()
                                {
                                    title = row.Title,
                                    thumb_media_id = resultJson.media_id,
                                    author = row.Author,
                                    digest = row.Summary,
                                    content = row.Content,
                                    content_source_url = row.Link
                                });
                            }
                            else
                            {
                                isUploadOk = false;
                            }
                        }
                    }                   
                }
            }
            if (!isUploadOk)
            {
                res.statusCode = 500;
                res.message = "同步素材失败~";
                return res;
            }

            var postStr = JsonConvert.SerializeObject(new { articles = articleList });
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/add_news?access_token={0}",token.access_token);
            string resMewsJson = WxTools.PostResponse(url, postStr);
            if (resMewsJson.Contains("errcode"))
            {
                res.statusCode = 500;
                res.message = "上传图文失败~";
                return res;
            }
            //修改状态
            await _meterialService.UpdateAsync(list);
            return res;
        }
    }
}