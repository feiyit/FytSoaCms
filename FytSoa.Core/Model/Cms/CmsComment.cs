using SqlSugar;
using System;

namespace FytSoa.Core.Model.Cms
{
    ///<summary>
    /// 评论表
    ///</summary>
    [SugarTable("Cms_Comment")]
    public partial class CmsComment
    {
        public CmsComment()
        {


        }
        /// <summary>
        /// Desc:唯一ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:归属栏目ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ColumnId { get; set; }

        /// <summary>
        /// Desc:评论人ID
        /// Default:0
        /// Nullable:True
        /// </summary>           
        public string UserGuid { get; set; }

        /// <summary>
        /// Desc:评论人昵称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserName { get; set; }

        /// <summary>
        /// Desc:评论内容
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:评论时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Desc:评论类型，如=1文章   2=下载  3=商品
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Option { get; set; } = 1;

        /// <summary>
        /// Desc:如果评论有星，显示星数
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Star { get; set; } = 5;

        /// <summary>
        /// Desc:回复人ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RepUserId { get; set; }

        /// <summary>
        /// Desc:回复人昵称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RepUserName { get; set; }

        /// <summary>
        /// Desc:回复时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? RepDate { get; set; }

        /// <summary>
        /// Desc:是否审核通过
        /// Default:b'0'
        /// Nullable:False
        /// </summary>           
        public bool Audit { get; set; } = true;

    }
}
