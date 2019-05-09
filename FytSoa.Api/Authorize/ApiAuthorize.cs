using FytSoa.Common;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FytSoa.Api
{
    public class ApiAuthorize : ActionFilterAttribute
    {
        #region 字段和属性
        /// <summary>
        /// 模块别名，可配置更改
        /// </summary>
        public string Modules { get; set; }

        /// <summary>
        /// 权限动作
        /// </summary>
        public string Methods { get; set; }

        /// <summary>
        /// 权限访问控制器参数
        /// </summary>
        private string Sign { get; set; }

        /// <summary>
        /// 是否保存日志
        /// </summary>
        public bool IsLog { get; set; } = true;

        /// <summary>
        /// 操作类型CRUD
        /// </summary>
        public LogEnum LogType { get; set; }

        private string ActionArguments { get; set; }
        private Stopwatch Stopwatch { get; set; }


        #endregion

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (IsLog)
            {
                ActionArguments = JsonConvert.SerializeObject(context.ActionArguments);
                Stopwatch = new Stopwatch();
                Stopwatch.Start();
            }
            var userGuid = "";
            //检测是否包含'Authorization'请求头，如果不包含则直接放行
            if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var tokenHeader = context.HttpContext.Request.Headers["Authorization"];
                tokenHeader = tokenHeader.ToString().Substring("Bearer ".Length).Trim();

                TokenModel tm = JwtHelper.SerializeJWT(tokenHeader);
                userGuid = tm.Uid;
            }
            //获得权限
            var menu = new List<SysMenuDto>();
            var menuSaveType = ConfigExtensions.Configuration[KeyHelper.LOGINAUTHORIZE];
             if (menuSaveType == "Redis")
            {                
                menu = RedisHelper.Get<List<SysMenuDto>>(KeyHelper.ADMINMENU + "_" + userGuid);
            }
            else
            {
                menu = MemoryCacheService.Default.GetCache<List<SysMenuDto>>(KeyHelper.ADMINMENU + "_"+ userGuid);
            }
            if (menu==null)
            {
                ContextReturn(context, "登录已过期，请退出重新登录！");
                return;
            }
            //如果是超管，不做权限控制处理
            if (Modules == "admin")
            {
                base.OnActionExecuting(context);
            }

            if (string.IsNullOrEmpty(Modules))
            {
                ContextReturn(context, "您没有操作权限，请联系系统管理员！");
                return;
            }
            
            //判断是否包含权限模块
            var menuModel = menu.Find(m => m.nameCode == Modules);
            if (!menu.Any(m => m.nameCode == Modules) || menuModel.btnFun == null)
            {
                ContextReturn(context, "您没有操作权限，请联系系统管理员！");
                return;
            }
            //判断模块下面的权限是否包含功能
            if (!menuModel.btnFun.Any(m => m.codeType == Methods))
            {
                ContextReturn(context, "您没有操作权限，请联系系统管理员！");
                return;
            }
            base.OnActionExecuting(context);
        }
        /// <summary>
        /// 返回API的信息
        /// </summary>
        /// <param name="context"></param>
        private void ContextReturn(ActionExecutingContext context,string mes) {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Unauthorized, message = "您没有操作权限，请联系系统管理员！" };
            context.HttpContext.Response.ContentType = "application/json;charset=utf-8";
            context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(res));
            context.Result = new EmptyResult();
        }


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            if (IsLog)
            {
                Stopwatch.Stop();

                string url = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
                string method = context.HttpContext.Request.Method;

                string qs = ActionArguments;

                var user = "";
                //检测是否包含'Authorization'请求头，如果不包含则直接放行
                if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    var tokenHeader = context.HttpContext.Request.Headers["Authorization"];
                    tokenHeader = tokenHeader.ToString().Substring("Bearer ".Length).Trim();

                    TokenModel tm = JwtHelper.SerializeJWT(tokenHeader);
                    user = tm.UserName;
                }

                var str = $"\n 方法：{Modules}：{Methods} \n " +
                    $"地址：{url} \n " +
                    $"方式：{method} \n " +
                    $"参数：{qs}\n " +
                    //$"结果：{res}\n " +
                    $"耗时：{Stopwatch.Elapsed.TotalMilliseconds} 毫秒";
                Logger.Default.Process(user, LogType.GetEnumText(), str);
            }
            
        }

    }
}
