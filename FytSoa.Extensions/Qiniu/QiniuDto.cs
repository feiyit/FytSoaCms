using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Extensions
{
    public class QiniuListParmDto
    {
        public string prefix { get; set; }

        public string marker { get; set; }
    }

    public class QiniuDelParmDto
    {
        public string filename { get; set; }
    }

    public class QiniuDelByPathParmDto
    {
        public string prefix { get; set; }

        public string filepath { get; set; }
    }

    /// <summary>
    /// 七牛云基本配置
    /// </summary>
    public class QiniuConfig
    {
        /// <summary>
        ///  Access Key 
        /// </summary>
        public string AK { get; set; }

        /// <summary>
        /// Secret Key
        /// </summary>
        public string SK { get; set; }

        /// <summary>
        /// 空间名称
        /// </summary>
        public string Bucket { get; set; }

        /// <summary>
        /// 基本目录
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string domain { get; set; }
    }
}
