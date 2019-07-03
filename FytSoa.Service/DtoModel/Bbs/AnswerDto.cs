using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 用户中心的答案列表
    /// </summary>
    public class AnswerDto
    {
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
        /// Desc:问题标题 问题标题
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:英文标题，可以作为Url 英文标题，可以作为Url
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string EnTitle { get; set; }

        /// <summary>
        /// Desc:标签 标签
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Tags { get; set; }

        /// <summary>
        /// Desc:查看次数 查看次数
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int LookSum { get; set; } = 0;

        /// <summary>
        /// Desc:答案次数 答案次数
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int AnswerSum { get; set; } = 0;

        /// <summary>
        /// Desc:投票次数=赞 投票次数=赞
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Support { get; set; } = 0;

        /// <summary>
        /// Desc:用户昵称
        /// Default:
        /// Nullable:True
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Desc:头像
        /// Default:
        /// Nullable:True
        /// </summary>
        public string HeadPic { get; set; }

        /// <summary>
        /// Desc:级别
        /// Default:
        /// Nullable:True
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Desc:回答内容
        /// Default:
        /// Nullable:True
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// Desc:回答内容
        /// Default:
        /// Nullable:True
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
