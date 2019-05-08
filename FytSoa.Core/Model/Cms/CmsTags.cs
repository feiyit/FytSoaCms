using SqlSugar;
using System;

namespace FytSoa.Core.Model.Cms
{
    /// <summary>
    /// 标签表
    /// </summary>
    [SugarTable("Cms_Tags")]
    public class CmsTags 
    {
        
        /// <summary>
        /// Desc:-
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string Guid {get;set;}

        /// <summary>
        /// Desc:首字母
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string FirstLetter {get;set;}

        /// <summary>
        /// Desc:标签名称
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string Name {get;set;}

        /// <summary>
        /// Desc:是否启用
        /// Default:b'1'
        /// Nullable:False
        /// </summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// Desc:标签链接地址
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string Links {get;set;}

        /// <summary>
        /// Desc:标签点击量
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int TagsHits { get; set; } = 0;

    }
}
