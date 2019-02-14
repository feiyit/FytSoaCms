using SqlSugar;
using System;

namespace FytSoa.Core.Model.Cms
{
    /// <summary>
    /// 文章表
    /// </summary>
    [SugarTable("Cms_Article")]
    public class CmsArticle 
    {
        /// <summary>
        /// Desc:-
        /// Default:-
        /// Nullable:False
        /// </summary>
        public int Id { get;set;}

        /// <summary>
        /// Desc:栏目ID
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int ColumnId { get; set; } = 0;

        /// <summary>
        /// Desc:0=新闻 1=多图
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int Types { get; set; } = 0;

        /// <summary>
        /// Desc:文章标题
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string Title {get;set;}

        /// <summary>
        /// Desc:文章标题颜色
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string TitleColor {get;set;}

        /// <summary>
        /// Desc:文章副标题
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string SubTitle {get;set;}

        /// <summary>
        /// Desc:作者
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string Author {get;set;}

        /// <summary>
        /// Desc:来源
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string Source {get;set;}

        /// <summary>
        /// Desc:是否外链
        /// Default:b'0'
        /// Nullable:False
        /// </summary>
        public bool IsLink { get; set; } = false;

        /// <summary>
        /// Desc:外部链接地址
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string LinkUrl {get;set;}

        /// <summary>
        /// Desc:文章标签
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string Tag {get;set;}

        /// <summary>
        /// Desc:权重
        /// Default:-
        /// Nullable:True
        /// </summary>
        public int Sort { get; set; } = 1;

        /// <summary>
        /// Desc:文章宣传图
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string ImgUrl {get;set;}

        /// <summary>
        /// Desc:文章缩略图
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string ThumImg {get;set;}

        /// <summary>
        /// Desc:视频链接地址
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string VideoUrl {get;set;}

        /// <summary>
        /// Desc:是否置顶
        /// Default:b'0'
        /// Nullable:False
        /// </summary>
        public bool IsTop { get; set; } = false;

        /// <summary>
        /// Desc:是否热点
        /// Default:b'0'
        /// Nullable:False
        /// </summary>
        public bool IsHot {get;set; } = false;

        /// <summary>
        /// Desc:是否滚动
        /// Default:b'0'
        /// Nullable:False
        /// </summary>
        public bool IsScroll {get;set; } = false;

        /// <summary>
        /// Desc:是否幻灯
        /// Default:b'0'
        /// Nullable:False
        /// </summary>
        public bool IsSlide {get;set; } = false;

        /// <summary>
        /// Desc:是否允许评论
        /// Default:b'0'
        /// Nullable:False
        /// </summary>
        public bool IsComment {get;set; } = false;

        /// <summary>
        /// Desc:是否手机站显示
        /// Default:b'0'
        /// Nullable:False
        /// </summary>
        public bool IsWap {get;set; } = true;

        /// <summary>
        /// Desc:是否在回收站
        /// Default:b'0'
        /// Nullable:False
        /// </summary>
        public bool IsRecyc {get;set; } = false;

        /// <summary>
        /// Desc:审核状态
        /// Default:b'1'
        /// Nullable:False
        /// </summary>
        public bool Audit {get;set; } = true;

        /// <summary>
        /// SEO关键字
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 文章摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Desc:点击量
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int Hits { get; set; } = 0;

        /// <summary>
        /// Desc:当日点击量
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int DayHits { get; set; } = 0;

        /// <summary>
        /// Desc:星期点击量
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int WeedHits { get; set; } = 0;

        /// <summary>
        /// Desc:月点击量
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int MonthHits { get; set; } = 0;

        /// <summary>
        /// Desc:最后点击时间
        /// Default:-
        /// Nullable:True
        /// </summary>
        public DateTime? LastHitDate {get;set;}

        /// <summary>
        /// Desc:编辑时间
        /// Default:-
        /// Nullable:True
        /// </summary>
        public DateTime? EditDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Desc:添加时间
        /// Default:-
        /// Nullable:True
        /// </summary>
        public DateTime AddDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Desc:删除到回收站时间
        /// Default:-
        /// Nullable:True
        /// </summary>
        public DateTime? DelDate {get;set;}

    }
}
