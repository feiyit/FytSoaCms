using SqlSugar;
using System;

namespace FytSoa.Core.Model.Cms
{
    /// <summary>
    /// 站点表
    /// </summary>
    [SugarTable("Cms_Site")]
    public class CmsSite 
    {
        
        /// <summary>
        /// Desc:-
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string Guid {get;set;}

        /// <summary>
        /// Desc:系统ID
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int SysId { get; set; } = 0;

        /// <summary>
        /// Desc:网站名称
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string SiteName {get;set;}

        /// <summary>
        /// Desc:网站域名
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string SiteUrl {get;set;}

        /// <summary>
        /// Desc:网站Logo
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string SiteLogo {get;set;}

        /// <summary>
        /// Desc:网站描述
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string Summary {get;set;}

        /// <summary>
        /// Desc:公司电话
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string SiteTel {get;set;}

        /// <summary>
        /// Desc:公司传真
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string SiteFax {get;set;}

        /// <summary>
        /// Desc:公司人事邮箱
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string SiteEmail {get;set;}

        /// <summary>
        /// Desc:公司客服QQ
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string QQ {get;set;}

        /// <summary>
        /// Desc:微信公众号图片
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string WeiXin {get;set;}

        /// <summary>
        /// Desc:微博链接地址或者二维码
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string WeiBo {get;set;}

        /// <summary>
        /// Desc:公司地址
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string SiteAddress {get;set;}

        /// <summary>
        /// Desc:网站备案号其它等信息
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string SiteCode {get;set;}

        /// <summary>
        /// Desc:网站SEO标题
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string SeoTitle {get;set;}

        /// <summary>
        /// Desc:网站SEO关键字
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string SeoKey {get;set;}

        /// <summary>
        /// Desc:网站SEO描述
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string SeoDescribe {get;set;}

        /// <summary>
        /// Desc:网站版权等信息
        /// Default:-
        /// Nullable:False
        /// </summary>
        public string SiteCopyright {get;set;}

        /// <summary>
        /// Desc:网站开启关闭状态
        /// Default:b'1'
        /// Nullable:False
        /// </summary>
        public bool Status { get; set; } = false;

        /// <summary>
        /// Desc:如果状态关闭，请输入关闭网站原因
        /// Default:-
        /// Nullable:True
        /// </summary>
        public string CloseInfo {get;set;}

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; } = false;

    }
}
