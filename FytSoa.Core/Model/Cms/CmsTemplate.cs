using SqlSugar;
using System;

namespace FytSoa.Core.Model.Cms
{
    /// <summary>
    /// 模板表
    /// </summary>
    [SugarTable("Cms_Template")]
    public class CmsTemplate 
    {
        
        /// <summary>
        /// Desc:自动增长
        /// Default:-
        /// Nullable:False
        /// </summary>
        public int Id {get;set;}

        /// <summary>
        /// Desc:模板名称
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string Title {get;set;}

        /// <summary>
        /// Desc:模板地址
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string Url {get;set;}

        /// <summary>
        /// Desc:状态是否启用
        /// Default:-
        /// Nullable:False
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Desc:排序
        /// Default:-
        /// Nullable:False
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:-
        /// Nullable:True
        /// </summary>
        public DateTime? AddDate { get; set; } = DateTime.Now;

    }
}
