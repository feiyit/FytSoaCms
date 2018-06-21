using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    /// <summary>
    /// 消息推送
    /// </summary>
    public class ErpPush
    {
        /// <summary>
        /// Desc: 唯一编号
        /// </summary>   
        [Required()]
        public string Guid { get; set; }

        /// <summary>
        /// 推送方式 1=全部  2=某个店铺  3=更新通知
        /// </summary>
        [Required()]
        public byte Mode { get; set; } = 1;

        /// <summary>
        /// 1=普通消息  2=透传消息
        /// </summary>
        [Required()]
        public byte Types { get; set; } = 1;

        /// <summary>
        /// 消息标题
        /// </summary>
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [StringLength(500)]
        public string Summary { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Required()]
        public DateTime AddDate { get; set; } = DateTime.Now;
    }
}
