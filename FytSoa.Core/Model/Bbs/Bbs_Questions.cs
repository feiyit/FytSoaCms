using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;

namespace FytSoa.Core.Model.Bbs
{
    ///<summary>
    /// 问题类
    ///</summary>
    public partial class Bbs_Questions
    {
        public Bbs_Questions()
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
        /// Desc:问题类型 问题类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Types { get; set; }

        /// <summary>
        /// Desc:标签 标签
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Tags { get; set; }

        /// <summary>
        /// Desc:消耗积分 消耗积分
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Point { get; set; } = 0;

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
        /// Desc:状态 1=未解决2=未回答3=已解决
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int Status { get; set; } = 1;

        /// <summary>
        /// Desc:热门状态 热门状态
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool IsRed { get; set; } = false;

        /// <summary>
        /// 审核状态
        /// </summary>
        public bool Audit { get; set; } = true;

        /// <summary>
        /// Desc:问题内容 问题内容
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Contents { get; set; }

        /// <summary>
        /// Desc:描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:发布时间 发布时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Desc:用户昵称
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string NickName { get; set; }

        /// <summary>
        /// Desc:头像
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string HeadPic { get; set; }

        /// <summary>
        /// Desc:级别
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string GroupName { get; set; }


    }
}
