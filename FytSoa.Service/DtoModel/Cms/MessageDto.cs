using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    public class MessageDeleteDto
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        public string parm { get; set; }

        /// <summary>
        /// 操作类型，单个删除，还是全部删除
        /// </summary>
        public int type { get; set; }
    }

    public class MessageReadDto
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        public int parm { get; set; }

        /// <summary>
        /// 操作类型，单个删除，还是全部删除
        /// </summary>
        public int type { get; set; }
    }
}
