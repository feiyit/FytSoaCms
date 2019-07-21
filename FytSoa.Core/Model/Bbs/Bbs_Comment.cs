using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Bbs
{
    ///<summary>
    /// 评论类
    ///</summary>
    public partial class Bbs_Comment
    {
        public Bbs_Comment()
        {


        }
        /// <summary>
        /// Desc:唯一编号 唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:所属问题编号 所属问题编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string QuestionGuid { get; set; }

        /// <summary>
        /// Desc:所属答案编号 所属答案编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string AnswerGuid { get; set; }

        /// <summary>
        /// Desc:用户编号 用户编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string UserGuid { get; set; }

        /// <summary>
        /// Desc:审核状态 审核状态
        /// Default:
        /// Nullable:False
        /// </summary>           
        public bool Status { get; set; } = false;

        /// <summary>
        /// Desc:评论内容 评论内容
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Content { get; set; }

        /// <summary>
        /// Desc:评论时间 评论时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddTime { get; set; } = DateTime.Now;

    }
}
