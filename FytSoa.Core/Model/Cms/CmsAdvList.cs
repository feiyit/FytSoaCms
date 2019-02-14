using System;
using SqlSugar;
namespace FytSoa.Core.Model.Cms
{
    /// <summary>
    /// 广告位管理
    /// </summary>
    [SugarTable("Cms_AdvList")]
    public class CmsAdvList 
    {
        
        /// <summary>
        /// Desc:-
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string Guid {get;set;}

        /// <summary>
        /// Desc:栏目ID
        /// Default:0
        /// Nullable:False
        /// </summary>
        public string ClassGuid { get;set;}

        /// <summary>
        /// Desc:广告位名称
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string Title {get;set;}

        /// <summary>
        /// Desc:广告位类型
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int Types { get; set; } = 1;

        /// <summary>
        /// Desc:是否启用
        /// Default:b'1'
        /// Nullable:False
        /// </summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// Desc:图片地址
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string ImgUrl {get;set;}

        /// <summary>
        /// Desc:链接地址
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string LinkUrl {get;set;}

        /// <summary>
        /// Desc:链接描述
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string LinkSummary {get;set;}

        /// <summary>
        /// Desc:打开窗口类型
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string Target { get; set; } = "_blank";

        /// <summary>
        /// Desc:广告位说明
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string AdvCode {get;set;}

        /// <summary>
        /// Desc:是否启用时间显示
        /// Default:b'0'
        /// Nullable:False
        /// </summary>
        public bool IsTimeLimit { get; set; } = false;

        /// <summary>
        /// Desc:开始时间
        /// Default:-
        /// Nullable:True
        /// </summary>
        public DateTime? BeginTime {get;set;}

        /// <summary>
        /// Desc:结束时间
        /// Default:-
        /// Nullable:True
        /// </summary>
        public DateTime? EndTime {get;set;}

        /// <summary>
        /// Desc:排序
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int Sort { get; set; } = 0;

        /// <summary>
        /// Desc:点击量
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int Hits { get; set; } = 0;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; } = DateTime.Now;

    }
}
