using FytSoa.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Extensions
{
    /// <summary>
    /// 微信帮助
    /// </summary>
    public class WxTools
    {
        private static readonly string userAgen = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36";

        /// <summary>
        /// 获得Access_token
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static WxAccessToken GetAccess(string appid, string secret)
        {
            var model = new WxAccessToken();
            var _cacheAccess=MemoryCacheService.Default.GetCache<WxAccessToken>("WinXinAccessToken");
            if (_cacheAccess!=null)
            {
                model = _cacheAccess;
            }
            else
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret);
                model = GetResponse<WxAccessToken>(url);
                MemoryCacheService.Default.SetCache("WinXinAccessToken",model,119);
            }
            return model;
        }

        /// <summary>
        /// 同步到公众号的菜单
        /// </summary>
        /// <returns></returns>
        public static WxReturnJson PushMenu(string access_token,string body)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}",access_token);
            return PostResponse<WxReturnJson>(url,body);
        }

        /// <summary>
        /// 获得素材列表
        /// </summary>
        /// <returns></returns>
        public static WxMeterArr GetMediaList(string access_token) {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}", access_token);
            return PostResponse<WxMeterArr>(url, "{\"type\":\"news\",\"offset\":0,\"count\":50}");
        }


        /// <summary>
        /// 根据Url地址Get请求返回数据
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <returns>字符串</returns>
        public static T GetResponse<T>(string url)
        {
            T result = default(T);
            HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
            HttpResponseMessage response = null;
            try
            {
                httpClient.DefaultRequestHeaders.Add("user-agent", userAgen);
                httpClient.CancelPendingRequests();
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                Task<HttpResponseMessage> taskResponse = httpClient.GetAsync(url);
                taskResponse.Wait();
                response = taskResponse.Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<System.IO.Stream> taskStream = response.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    //此处会抛出异常：不支持超时设置，对返回结果没有影响
                    System.IO.Stream dataStream = taskStream.Result;
                    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                    string res = reader.ReadToEnd();

                    return JsonConvert.DeserializeObject<T>(res); ;
                }
                return result;
            }
            catch
            {
                return result;
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
                if (httpClient != null)
                {
                    httpClient.Dispose();
                }
            }
        }

        /// <summary>
        /// Post请求返回实体 
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求数据</param>
        /// <returns>实体</returns>
        public static T PostResponse<T>(string url, string postData)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
            HttpResponseMessage response = null;
            try
            {
                httpClient.MaxResponseContentBufferSize = 256000;
                httpClient.DefaultRequestHeaders.Add("user-agent", userAgen);
                httpClient.CancelPendingRequests();
                httpClient.DefaultRequestHeaders.Clear();
                HttpContent httpContent = new StringContent(postData);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                T result = default(T);
                Task<HttpResponseMessage> taskResponse = httpClient.PostAsync(url, httpContent);
                taskResponse.Wait();
                response = taskResponse.Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<System.IO.Stream> taskStream = response.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    System.IO.Stream dataStream = taskStream.Result;
                    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                    string s = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<T>(s);
                }
                return result;
            }
            catch(Exception ex)
            {
                return default(T);
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
                if (httpClient != null)
                {
                    httpClient.Dispose();

                }

            }

        }

        /// <summary>
        /// Post请求返回字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求数据</param>
        /// <returns>实体</returns>
        public static string PostResponse(string url, string postData)
        {
            var result = "";
            HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
            HttpResponseMessage response = null;
            try
            {
                httpClient.MaxResponseContentBufferSize = 256000;
                httpClient.DefaultRequestHeaders.Add("user-agent", userAgen);
                httpClient.CancelPendingRequests();
                httpClient.DefaultRequestHeaders.Clear();
                HttpContent httpContent = new StringContent(postData);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                Task<HttpResponseMessage> taskResponse = httpClient.PostAsync(url, httpContent);
                taskResponse.Wait();
                response = taskResponse.Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<System.IO.Stream> taskStream = response.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    System.IO.Stream dataStream = taskStream.Result;
                    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                    result = reader.ReadToEnd();
                }
                return result;
            }
            catch
            {
                return result;
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
                if (httpClient != null)
                {
                    httpClient.Dispose();

                }

            }

        }
    }


    /// <summary>
    /// 微信帮助
    /// </summary>
    public class WxAccessToken
    {
        /// <summary>
        /// access_token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int expires_in { get; set; }
    }

    /// <summary>
    /// 微信接口返回值
    /// </summary>
    public class WxReturnJson
    {
        /// <summary>
        /// 返回代码  0=正确
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 返货错误
        /// </summary>
        public string errmsg { get; set; }
    }

    #region 素材模型组
    /// <summary>
    /// 素材组
    /// </summary>
    public class WxMeterArr
    {
        /// <summary>
        /// 该类型的素材的总数
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 本次调用获取的素材的数量
        /// </summary>
        public int item_count { get; set; }

        public List<WxMeterItem> item { get; set; }
    }

    /// <summary>
    /// 素材集合
    /// </summary>
    public class WxMeterItem
    {
        /// <summary>
        /// 素材id
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public int update_time { get; set; }

        public WxMeterContent content { get; set; }
    }

    public class WxMeterContent
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public int create_time { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public int update_time { get; set; }

        public List<WxMeterNewsItem> news_item { get; set; }
    }

    public class WxMeterNewsItem
    {
        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        //public string name { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 图文消息的摘要，仅有单图文消息才有摘要，多图文此处为空
        /// </summary>
        public string digest { get; set; }

        /// <summary>
        /// 图文消息的具体内容，支持HTML标签，必须少于2万字符，小于1M，且此处会去除JS
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 图文消息的原文地址，即点击“阅读原文”后的URL
        /// </summary>
        public string content_source_url { get; set; }

        /// <summary>
        /// 图文消息的封面图片素材id（必须是永久mediaID）
        /// </summary>
        public string thumb_media_id { get; set; }

        /// <summary>
        /// 图文页的URL，或者，当获取的列表是图片素材列表时，该字段是图片的URL
        /// </summary>
        public string url { get; set; }


        public string thumb_url { get; set; }

        /// <summary>
        /// 是否显示封面，0为false，即不显示，1为true，即显示
        /// </summary>
        public int show_cover_pic { get; set; }
        public int need_open_comment { get; set; }
        public int only_fans_can_comment { get; set; }
    }
    #endregion

    #region 微信按钮
    /// <summary>
    /// 按钮组
    /// </summary>
    public class WxPushButton
    {
        public List<WxButton> button { get; set; }
    }

    /// <summary>
    /// 微信菜单一级按钮
    /// </summary>
    public class WxButton {

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 菜单的响应动作类型，view表示网页类型，click表示点击类型，miniprogram表示小程序类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// click等点击类型必须	菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// view、miniprogram类型必须	网页 链接，用户点击菜单可打开链接，不超过1024字节。 type为miniprogram时，不支持小程序的老版本客户端将打开本url。  view_limited=media_id
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// media_id类型和view_limited类型必须	调用新增永久素材接口返回的合法media_id
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// miniprogram类型必须	小程序的appid（仅认证公众号可配置）
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// miniprogram类型必须	小程序的页面路径
        /// </summary>
        public string pagepath { get; set; }

        /// <summary>
        /// 一级对应二级菜单集合
        /// </summary>
        public List<WxSubButton> sub_button { get; set; }
    }

    /// <summary>
    /// 微信二级菜单按钮
    /// </summary>
    public class WxSubButton
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 菜单的响应动作类型，view表示网页类型，click表示点击类型，miniprogram表示小程序类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// click等点击类型必须	菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// view、miniprogram类型必须	网页 链接，用户点击菜单可打开链接，不超过1024字节。 type为miniprogram时，不支持小程序的老版本客户端将打开本url。
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// media_id类型和view_limited类型必须	调用新增永久素材接口返回的合法media_id
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// miniprogram类型必须	小程序的appid（仅认证公众号可配置）
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// miniprogram类型必须	小程序的页面路径
        /// </summary>
        public string pagepath { get; set; }
    }
    #endregion
}
