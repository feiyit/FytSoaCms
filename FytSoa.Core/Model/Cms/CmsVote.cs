using SqlSugar;
using System;

namespace FytSoa.Core.Model.Cms
{
    ///<summary>
    /// 投票栏目表
    ///</summary>
    [SugarTable("Cms_Vote")]
    public partial class CmsVote
    {
        public CmsVote()
        {


        }
        /// <summary>
        /// Desc:主键自增
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:所属栏目ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int ColumnId { get; set; } = 0;

        /// <summary>
        /// Desc:投票标题
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:投票副标题
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SubTitle { get; set; }

        /// <summary>
        /// Desc:选项总数
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int ItemSum { get; set; } = 0;

        /// <summary>
        /// Desc:投票总数
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int VoteSum { get; set; } = 0;

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public bool Options { get; set; } = true;

        /// <summary>
        /// Desc:投票类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int VoteType { get; set; } = 0;

        /// <summary>
        /// Desc:宣传图
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ImgUrl { get; set; }

        /// <summary>
        /// Desc:时间限制
        /// Default:
        /// Nullable:False
        /// </summary>           
        public bool IsTime { get; set; } = false;

        /// <summary>
        /// Desc:开始时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Desc:投票简介
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:发布时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;

    }
}
