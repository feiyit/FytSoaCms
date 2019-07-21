using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Bbs
{
    ///<summary>
    ///论坛分类表 论坛分类表
    ///</summary>
    public partial class Bbs_Classify
    {
        public Bbs_Classify()
        {


        }
        /// <summary>
        /// Desc:唯一编号 唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:分类名称 分类名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ClassName { get; set; }

        /// <summary>
        /// Desc:英文名称 英文名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string EnClassName { get; set; }

        /// <summary>
        /// Desc:首字母
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FirstLetter { get; set; }

        /// <summary>
        /// Desc:状态 状态
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public bool Status { get; set; } = true;

        /// <summary>
        /// Desc:父级 父级
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ParentGuid { get; set; }

        /// <summary>
        /// Desc:删除状态 删除状态
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// Desc:描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

    }
}
