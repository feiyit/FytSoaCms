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
    [Route("api/wx/menu")]
    [JwtAuthorize(Roles = "Admin")]
    //[ApiController]
    public class WxMenuController : Controller
    {
        private readonly IWxSettingService _settingService;
        public WxMenuController(IWxSettingService settingService)
        {
            _settingService = settingService;
        }

        /// <summary>
        /// 修改自定义菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit"), Log("WxMenu：edit", LogType = LogEnum.UPDATE)]
        public async Task<ApiResult<string>> DeleteRole([FromBody]MenuEditDto parm)
        {
            var model = _settingService.GetModelAsync(m => m.Id == parm.id).Result.data;
            model.MenuJson = parm.menu;
            return await _settingService.UpdateAsync(model);
        }

        /// <summary>
        /// 修改自定义菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("model")]
        public async Task<ApiResult<WxSetting>> GetModel([FromBody]ParmInt obj)
        {
            return await _settingService.GetModelAsync(m=>m.Id== obj.id);
        }

        /// <summary>
        /// 同步到公众号菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost("synchro"), Log("WxMenu：synchro", LogType = LogEnum.ASYWX)]
        public async Task<ApiResult<string>> PushMenu([FromBody]ParmInt obj)
        {
            //MemoryCacheService.Default.RemoveCache("WinXinAccessToken");
            var res = new ApiResult<string>();
            //获得公众号配置
            var model = _settingService.GetModelAsync(m => m.Id == obj.id).Result.data;
            //获得access_taken
            var token = WxTools.GetAccess(model.AppId,model.AppSecret);

            var dbMenu = JsonConvert.DeserializeObject<List<WxButton>>(model.MenuJson);
            
            foreach (var item in dbMenu)
            {
                item.sub_button = item.sub_button.Count > 0 ? item.sub_button : null;
                if (item.type == "0" && !string.IsNullOrEmpty(item.url))
                {
                    item.type = "view";
                }
                else if (item.type=="1" && !string.IsNullOrEmpty(item.media_id))
                {
                    item.type = "media_id";
                    item.url = null;
                }
                else
                {
                    item.type = null;
                    item.url = null;
                }
                if (item.sub_button!=null)
                {
                    foreach (var row in item.sub_button)
                    {
                        if (row.type == "0" && !string.IsNullOrEmpty(row.url))
                        {
                            row.type = "view";
                        }
                        else if (row.type == "1" && !string.IsNullOrEmpty(row.media_id))
                        {
                            row.type = "media_id";
                            row.url = null;
                        }
                        else
                        {
                            row.type = null;
                            row.url = null;
                        }
                    }
                }
            }
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.NullValueHandling = NullValueHandling.Ignore;
            var body = JsonConvert.SerializeObject(new WxPushButton() { button = dbMenu }, jsetting);

            var wxres = WxTools.PushMenu(token.access_token, body);
            res.message = wxres.errmsg;
            res.statusCode = wxres.errcode == 0 ? 200 : wxres.errcode;

            ////构建菜单
            //var wxbutton = new List<WxButton>();

            //var AsubButton = new List<WxSubButton>();
            //AsubButton.Add(new WxSubButton()
            //{
            //    name = "返回文本",
            //    type= "click",
            //    key = "V1001_TODAY_MUSIC"
            //});

            //var BsubButton = new List<WxSubButton>();
            //BsubButton.Add(new WxSubButton()
            //{
            //    name = "Url跳转",
            //    type= "view",
            //    url = "http://h5.feiyit.com/"
            //});

            //wxbutton.Add(new WxButton() {
            //    name = "菜单A",
            //    sub_button = AsubButton
            //});
            //wxbutton.Add(new WxButton()
            //{
            //    name = "菜单B",
            //    sub_button = BsubButton
            //});
            //wxbutton.Add(new WxButton()
            //{
            //    name = "菜单C",
            //    type = "click",
            //    key = "V1001_GOOD"
            //});
            ////过滤空值不序列化
            //JsonSerializerSettings jsetting = new JsonSerializerSettings();
            //jsetting.NullValueHandling = NullValueHandling.Ignore;
            //var body = JsonConvert.SerializeObject(new WxPushButton() {button=wxbutton }, jsetting);
            //var wxres = WxTools.PushMenu(token.access_token,body);
            //res.message = wxres.errmsg;
            //res.statusCode = wxres.errcode == 0 ? 200 : wxres.errcode;
            return await Task.Run(() => res);
        }
    }
}