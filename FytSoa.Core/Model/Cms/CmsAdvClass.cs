using SqlSugar;
using System;

namespace FytSoa.Core.Model.Cms
{
    /// <summary>
    /// 广告栏目管理
    /// </summary>
    [SugarTable("Cms_AdvClass")]
    public class CmsAdvClass
    {
        
        /// <summary>
        /// Desc:-
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string Guid {get;set;}

        /// <summary>
        /// Desc:父ID
        /// Default:0
        /// Nullable:False
        /// </summary>
        public string ParentGuid { get;set;}

        /// <summary>
        /// Desc:栏位名称
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string Title {get;set;}

        /// <summary>
        /// Desc:栏位类型
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string Flag {get;set;}

        /// <summary>
        /// Desc:宽度
        /// Default:-
        /// Nullable:True
        /// </summary>
        public int Width { get; set; } = 0;

        /// <summary>
        /// Desc:高度
        /// Default:-
        /// Nullable:True
        /// </summary>
        public int Height { get; set; } = 0;

        /// <summary>
        /// Desc:说明
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string Summary {get;set;}

        /// <summary>
        /// Desc:状态
        /// Default:b'1'
        /// Nullable:False
        /// </summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// Desc:站点ID
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int SiteID { get; set; } = 1;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; } = DateTime.Now;

    }
}
