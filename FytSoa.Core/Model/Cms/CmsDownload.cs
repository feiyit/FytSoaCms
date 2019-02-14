using SqlSugar;
using System;

namespace FytSoa.Core.Model.Cms
{
    ///<summary>
    /// 下载管理表
    ///</summary>
    [SugarTable("Cms_Download")]
    public partial class CmsDownload
    {
        public CmsDownload()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:下载所属类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int ColumnId { get; set; } = 0;

        /// <summary>
        /// Desc:下载标题
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:下载副标题
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SubTitle { get; set; }

        /// <summary>
        /// Desc:文件地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FileUrl { get; set; }

        /// <summary>
        /// Desc:文件类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FileType { get; set; }

        /// <summary>
        /// Desc:文件大小
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FileSize { get; set; }

        /// <summary>
        /// Desc:下载数量
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int DownSum { get; set; }

        /// <summary>
        /// Desc:访问数
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Hits { get; set; } = 0;

        /// <summary>
        /// Desc:权重
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Sort { get; set; } = 0;

        /// <summary>
        /// Desc:适用系统
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string IsSystem { get; set; }

        /// <summary>
        /// Desc:软件类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string IsCharge { get; set; }

        /// <summary>
        /// Desc:图片地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ImgUrl { get; set; }

        /// <summary>
        /// Desc:缩略图地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ThumImg { get; set; }

        /// <summary>
        /// Desc:是否置顶
        /// Default:
        /// Nullable:False
        /// </summary>           
        public bool IsTop { get; set; } = false;

        /// <summary>
        /// Desc:是否评论
        /// Default:
        /// Nullable:False
        /// </summary>           
        public bool IsComment { get; set; } = false;

        /// <summary>
        /// Desc:状态
        /// Default:
        /// Nullable:False
        /// </summary>           
        public bool Audit { get; set; } = true;

        /// <summary>
        /// Desc:是否有外链
        /// Default:
        /// Nullable:False
        /// </summary>           
        public bool IsLink { get; set; } = false;

        /// <summary>
        /// Desc:外链地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LinkUrl { get; set; }

        /// <summary>
        /// Desc:标签
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Tag { get; set; }

        /// <summary>
        /// Desc:简介
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:详细描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Content { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime EditDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;

    }
}
