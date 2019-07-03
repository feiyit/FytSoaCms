using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Bbs
{
    ///<summary>
    /// 论坛通知表
    ///</summary>
    public partial class Bbs_Notice
    {
        public Bbs_Notice()
        {


        }
        /// <summary>
        /// Desc:唯一编号 唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:用户编号 用户编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string UserGuid { get; set; }

        /// <summary>
        /// Desc:消息类型 消息类型
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Types { get; set; } = 0;

        /// <summary>
        /// Desc:消息内容 消息内容
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Content { get; set; }

        /// <summary>
        /// Desc:阅读状态 阅读状态
        /// Default:b'0'
        /// Nullable:False
        /// </summary>           
        public bool IsRead { get; set; } = false;

        /// <summary>
        /// Desc:发送时间 发送时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddTime { get; set; } = DateTime.Now;

    }
}
