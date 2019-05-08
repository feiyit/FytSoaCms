using SqlSugar;
using System;

namespace FytSoa.Core.Model.Cms
{
    ///<summary>
    /// 投票项表
    ///</summary>
    [SugarTable("Cms_VoteItem")]
    public partial class CmsVoteItem
    {
        public CmsVoteItem()
        {


        }
        /// <summary>
        /// Desc:主键自增
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:投票项ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int VoteId { get; set; } = 0;

        /// <summary>
        /// Desc:投票项
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:投票数
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int VoteSum { get; set; } = 0;

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Scale { get; set; }

        /// <summary>
        /// Desc:排序字段
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Sort { get; set; } = 0;

    }
}
