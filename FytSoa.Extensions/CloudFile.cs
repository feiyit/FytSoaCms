using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Extensions
{
    public class ListInfo
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime Time { get; set; }
    }
    public class CloudFile
    {
        public int Code { get; set; } = 200;
        public string Message { get; set; }
        public string Page { get; set; } = "";
        public string Token { get; set; }
        public List<ListInfo> list { get; set; }
    }
}
