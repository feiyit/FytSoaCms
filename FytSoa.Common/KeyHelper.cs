using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Common
{
    /// <summary>
    /// 值管理
    /// </summary>
    public class KeyHelper
    {
        /// <summary>
        /// 管理菜单
        /// </summary>
        public static string ADMINMENU = "ADMINMENU";

        /// <summary>
        /// Redis连接对象
        /// </summary>
        public static string REDISLOCALHOST = "Cache:Configuration";

        /// <summary>
        /// Redis-单例名称
        /// </summary>
        public static string REDISSIGNSNAME = "Cache:SingleRedis";

        /// <summary>
        /// Redis-分布式名称
        /// </summary>
        public static string REDISDEFAULTNAME = "Cache:RedisInstance";

        #region 用户登录配置
        /// <summary>
        /// 用户登录保存菜单使用方式
        /// </summary>
        public static string LOGINAUTHORIZE = "Login:Authorize";

        /// <summary>
        /// 用户登录保存用户方式
        /// </summary>
        public static string LOGINSAVEUSER = "Login:SaveType";

        /// <summary>
        /// 用户登录保存Cookie过期时间  小时
        /// </summary>
        public static string LOGINCOOKIEEXPIRES = "Login:ExpiresHours";

        /// <summary>
        /// 用户登录-保存登录次数
        /// </summary>
        public static string LOGINCOUNT = "Login:Count";

        /// <summary>
        /// 用户登录-延时分钟
        /// </summary>
        public static string LOGINDELAYMINUTE = "Login:DelayMinute";
        #endregion
    }
}
