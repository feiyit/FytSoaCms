using System;
using SqlSugar;

namespace FytSoa.Core.Model.Cms
{
    /// <summary>
    /// 网站栏目管理
    /// </summary>
    [SugarTable("Cms_Column")]
    public class CmsColumn 
    {
        
        /// <summary>
        /// Desc:自动递增
        /// Default:-
        /// Nullable:False
        /// </summary>
        public int Id { get;set;}

        /// <summary>
        /// Desc:栏目编号
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string Number {get;set;}

        /// <summary>
        /// Desc:栏目标题
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string Title {get;set;}

        /// <summary>
        /// Desc:英文栏位名称
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string EnTitle {get;set;}

        /// <summary>
        /// Desc:栏位副标题
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string SubTitle {get;set;}

        /// <summary>
        /// Desc:父栏目
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int ParentId {get;set;}

        /// <summary>
        /// Desc:栏位集合
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string ClassList {get;set;}

        /// <summary>
        /// Desc:栏位等级
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int ClassLayer { get; set; } = 1;

        /// <summary>
        /// Desc:排序
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int Sort { get; set; } = 1;

        /// <summary>
        /// Desc:栏目类型
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int TypeID { get; set; } = 1;

        /// <summary>
        /// Desc:栏位属性
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string Attr {get;set;}

        /// <summary>
        /// Desc:模板ID
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int TempId { get; set; } = 0;

        /// <summary>
        /// Desc:模板名称
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string TempName {get;set;}

        /// <summary>
        /// Desc:模板地址
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string TempUrl {get;set;}

        /// <summary>
        /// Desc:栏位图片
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string ImgUrl {get;set;}

        /// <summary>
        /// Desc:外部链接地址
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string LinkUrl {get;set;}

        /// <summary>
        /// Desc:移动端链接地址
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string WapLinkUrl {get;set;}

        /// <summary>
        /// Desc:是否顶部显示
        /// Default:b'0'
        /// Nullable:False
        /// </summary>
        public bool IsTopShow { get; set; } = true;

        /// <summary>
        /// Desc:是否手机端展示
        /// Default:b'0'
        /// Nullable:False
        /// </summary>
        public bool IsWapShow { get; set; } = false;

        /// <summary>
        /// Desc:关键词
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string KeyWord {get;set;}

        /// <summary>
        /// Desc:描述
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string Summary {get;set;}

        /// <summary>
        /// Desc:内容
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string Content {get;set;}

        /// <summary>
        /// Desc:站点ID
        /// Default:0
        /// Nullable:False
        /// </summary>
        public string SiteGuid { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; } = DateTime.Now;

    }
}
