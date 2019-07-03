using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace FytSoa.Core.Model.Bbs
{
    ///<summary>
    /// 答案类
    ///</summary>
    public partial class Bbs_Answer
    {
        public Bbs_Answer()
        {


        }
        /// <summary>
        /// Desc:唯一编号 唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:问题编号 问题编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string QuestionGuid { get; set; }

        /// <summary>
        /// Desc:用户编号 用户编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string UserGuid { get; set; }

        /// <summary>
        /// Desc:是否采纳 是否采纳
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool IsAdopt { get; set; } = false;

        /// <summary>
        /// Desc:答案内容 答案内容
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Content { get; set; }

        /// <summary>
        /// Desc:回答时间 回答时间
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
