using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 修改菜单参数
    /// </summary>
    public class MenuEditDto
    {
        /// <summary>
        /// 公众号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 菜单字符串
        /// </summary>
        public string menu { get; set; }
    }
}
