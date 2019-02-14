using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Common
{
    /// <summary>
    /// API 返回JSON字符串
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> where T : class
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; } = true;
        /// <summary>
        /// 信息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int statusCode { get; set; } = 200;
        /// <summary>
        /// 数据集
        /// </summary>
        public T data { get; set; }
    }
}
