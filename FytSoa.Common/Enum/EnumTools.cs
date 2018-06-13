using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Common
{
    public enum LogEnum
    {
        /// <summary>
        /// 登录日志
        /// </summary>
        [Text("登录日志")]
        Login = 1,

        /// <summary>
        /// 操作日志
        /// </summary>
        [Text("操作日志")]
        Operation = 2
    }
}
