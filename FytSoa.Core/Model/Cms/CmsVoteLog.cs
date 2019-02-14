using SqlSugar;
using System;

namespace FytSoa.Core.Model.Cms
{
    ///<summary>
    /// 投票记录表
    ///</summary>
    [SugarTable("Cms_VoteLog")]
    public partial class CmsVoteLog
    {
        public CmsVoteLog()
        {

        }
        /// <summary>
        /// Desc:唯一ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:投票ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int VoteId { get; set; } = 0;

        /// <summary>
        /// Desc:投票项ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int ItemId { get; set; } = 0;

        /// <summary>
        /// Desc:投票人ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string UserId { get; set; }

        /// <summary>
        /// Desc:投票人昵称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserName { get; set; }

        /// <summary>
        /// Desc:IP地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Ip { get; set; }

        /// <summary>
        /// Desc:投票时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Desc:投票详情
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

    }
}
