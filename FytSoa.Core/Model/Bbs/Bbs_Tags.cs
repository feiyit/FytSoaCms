using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Bbs
{
    ///<summary>
    ///论坛标签表 论坛标签表
    ///</summary>
    public partial class Bbs_Tags
    {
        public Bbs_Tags()
        {


        }
        /// <summary>
        /// Desc:唯一编号 唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:标签名称 标签名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string TagName { get; set; }

        /// <summary>
        /// Desc:英文名称 英文名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string EnTagName { get; set; }

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
        /// Desc:删除状态 删除状态
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// Desc:描述 描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

    }
}
