using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Core.Model.Cms
{
    /// <summary>
    /// 视频表
    /// </summary>
    [SugarTable("cms_video")]
    public class CmsVideo
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
        /// Desc:视频副标题
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// Desc:-视频封面
        /// </summary>
        public string VideoCover { get; set; }

        /// <summary>
        /// Desc:-视频地址
        /// </summary>
        public string VideoUrl { get; set; }

        /// <summary>
        /// Desc:-视频内容
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
