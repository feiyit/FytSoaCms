using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 微信消息
    /// </summary>
    public class Material
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string img { get; set; }

        /// <summary>
        /// 连接
        /// </summary>
        public string link { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
    }
}
