using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Core.Model.Cms
{
    /// <summary>
    /// 产品表
    /// </summary>
    [SugarTable("cms_product")]
    public class CmsProduct
    {
        /// <summary>
        /// Desc:-
        /// Default:-
        /// Nullable:False
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Desc:-父编号
        /// </summary>
        public int ParentId { get; set; } = 0;

        /// <summary>
        /// Desc:-视频标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string Models { get; set; }

        /// <summary>
        /// Desc:视频副标题
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// Desc:Seo关键词
        /// </summary>
        public string SeoKey { get; set; }

        /// <summary>
        /// Desc:Seo描述
        /// </summary>
        public string SeoDesc { get; set; }

        /// <summary>
        /// Desc:-产品封面
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// Desc:-图片集合列表
        /// </summary>
        public string ImgList { get; set; }

        /// <summary>
        /// Desc:-内容Json
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// Desc:-是否推荐
        /// </summary>
        public bool IsTop { get; set; } = false;

        /// <summary>
        /// Desc:-添加时间
        /// </summary>
        public DateTime AddTime { get; set; } = DateTime.Now;
    }
}
