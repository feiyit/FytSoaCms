using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FytSoa.Extensions
{
    public class ExceptionFilter
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public ExceptionFilter(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            bool isCatched = false;
            try
            {
                await _next(context);
            }
            catch (Exception ex) //发生异常
            {
                //自定义业务异常
                if (ex is MyException)
                {
                    context.Response.StatusCode = ((MyException)ex).GetCode();
                }
                //未知异常
                else
                {
                    context.Response.StatusCode = 500;
                    //LogHelper.SetLog(LogLevel.Error, ex);
                }
                await HandleExceptionAsync(context, context.Response.StatusCode, ex.Message);
                isCatched = true;
            }
            finally
            {
                if (!isCatched && context.Response.StatusCode != 200)//未捕捉过并且状态码不为200
                {
                    string msg = "";
                    switch (context.Response.StatusCode)
                    {
                        case 401:
                            msg = "未授权";
                            break;
                        case 404:
                            msg = "未找到服务";
                            break;
                        case 502:
                            msg = "请求错误";
                            break;
                        default:
                            msg = "未知错误";
                            break;
                    }
                    await HandleExceptionAsync(context, context.Response.StatusCode, msg);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static async Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            var data = new { statusCode, Success = false, message = msg };
            context.Response.ContentType = "application/json;charset=utf-8";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
            //var path = context.Request.Path.ToString().ToLower();
            //if (path.Contains("api"))
            //{
            //    var data = new { statusCode, Success = false, message = msg };
            //    context.Response.ContentType = "application/json;charset=utf-8";
            //    await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
            //}
            //else if (path.Contains("fytadmin") && !path.Contains("login"))
            //{
            //    await context.Response.WriteAsync("<script>window.location.href = '/fytadmin/login';</script>");
            //}
            //else
            //{
            //    //await _next(context);
            //}
        }
    }
}
