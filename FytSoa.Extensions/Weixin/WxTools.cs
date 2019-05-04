using FytSoa.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            var _cacheAccess = MemoryCacheService.Default.GetCache<WxAccessToken>("WinXinAccessToken");
            if (_cacheAccess != null)
            {
                model = _cacheAccess;
            }
            else
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret);
                model = GetResponse<WxAccessToken>(url);
                MemoryCacheService.Default.SetCache("WinXinAccessToken", model, 119);
            }
            return model;
        }

        /// <summary>
        /// 同步到公众号的菜单
        /// </summary>
        /// <returns></returns>
        public static WxReturnJson PushMenu(string access_token, string body)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", access_token);
            return PostResponse<WxReturnJson>(url, body);
        }

        /// <summary>
        /// 获得素材列表
        /// </summary>
        /// <returns></returns>
        public static WxMeterArr GetMediaList(string access_token)
        {
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

                    return JsonConvert.DeserializeObject<T>(res);
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
            catch (Exception ex)
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


        /// <summary>
        /// 服务号：上传多媒体文件
        /// </summary>
        /// <param name="accesstoken">调用接口凭据</param>
        /// <param name="filename">文件路径</param>
        /// <param name="contenttype">文件Content-Type类型(例如：image/jpeg、audio/mpeg)</param>
        /// <returns></returns>
        public static WxMeterUploadRes UploadFile(string uacaccess_tokenrl, string path, string fileExt)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={0}&type={1}", uacaccess_tokenrl, "image");

            //文件后缀名，使用格式
            var contenttype = "image/jpeg";
            switch (fileExt)
            {
                case ".png":
                    contenttype = "image/png";
                    break;
            }
            FileStream fs = null;
            byte[] bArr = null;
            //判断是否网络图片
            if (path.ToLower().StartsWith("http") || path.ToLower().StartsWith("https"))
            {
                WebClient mywebclient = new WebClient();
                bArr = mywebclient.DownloadData(path);
            }
            else
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                bArr = new byte[fs.Length];
                fs.Read(bArr, 0, bArr.Length);
            }

            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            int pos = path.LastIndexOf("\\");
            string fileName = path.Substring(pos + 1);

            //组织表单数据
            StringBuilder sbHeader = new StringBuilder();
            sbHeader.Append("--" + boundary + "\r\n");
            sbHeader.Append("Content-Disposition: form-data; name=\"media\"; filename=\"" + Guid.NewGuid().ToString() + ".jpg\"; filelength=\"" + bArr.Length + "\"");
            sbHeader.Append("\r\n");
            sbHeader.Append("Content-Type: " + contenttype);
            sbHeader.Append("\r\n\r\n");

            //请求头部信息 
            //StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());


            Stream postStream = request.GetRequestStream();
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            postStream.Write(bArr, 0, bArr.Length);
            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            postStream.Close();

            if (fs!=null)
            {
                fs.Close();
                fs.Dispose();
            }
            
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            if (content.Contains("errcode"))
            {
                return new WxMeterUploadRes() { code = 500 };
            }
            return JsonConvert.DeserializeObject<WxMeterUploadRes>(content);
        }

        /// <summary>
        /// 网络图片转换流
        /// </summary>
        /// <param name="picUrl">网络图片地址</param>
        /// <param name="timeOut">Request最大请求时间，如果为-1则无限制</param>
        /// <returns></returns>
        public static byte[] GetBytesByImagePath(string picUrl, int timeOut)
        {
            byte[] bArr = null;
            WebClient mywebclient = new WebClient();
            bArr=mywebclient.DownloadData(picUrl);
            return bArr;
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

    #region 素材提交到微信平台的模型
    public class WxMeterArticle
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 图文消息的封面图片素材id（必须是永久mediaID）
        /// </summary>
        public string thumb_media_id { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 图文消息的摘要，仅有单图文消息才有摘要，多图文此处为空。如果本字段为没有填写，则默认抓取正文前64个字。
        /// </summary>
        public string digest { get; set; }
        /// <summary>
        /// 是否显示封面，0为false，即不显示，1为true，即显示
        /// </summary>
        public int show_cover_pic { get; set; } = 1;
        /// <summary>
        /// 图文消息的具体内容，支持HTML标签，必须少于2万字符，小于1M，且此处会去除JS,涉及图片url必须来源 "上传图文消息内的图片获取URL"接口获取。外部图片url将被过滤。
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 图文消息的原文地址，即点击“阅读原文”后的URL
        /// </summary>
        public string content_source_url { get; set; }
        /// <summary>
        /// Uint32 是否打开评论，0不打开，1打开
        /// </summary>
        public int need_open_comment { get; set; } = 1;
        /// <summary>
        /// Uint32 是否粉丝才可评论，0所有人可评论，1粉丝才可评论
        /// </summary>
        public int only_fans_can_comment { get; set; } = 0;
    }
    /// <summary>
    /// 上传成功后的返回结果
    /// </summary>
    public class WxMeterUploadRes
    {
        /// <summary>
        /// 永久素材的id
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 素材的地址
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int code { get; set; } = 200;
    }
    #endregion

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
    public class WxButton
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
