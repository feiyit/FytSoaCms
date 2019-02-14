using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Core.Model.Cms
{
    /// <summary>
    /// 图片列表分类
    /// </summary>
    [SugarTable("Cms_ImgType")]
    public class CmsImgType
    {
        /// <summary>
        /// Desc:唯一ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:所属父级
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ParentGuid { get; set; }

        /// <summary>
        /// Desc:图片类型分类 0=本地 1=云端
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Type { get; set; } = 0;

        /// <summary>
        /// Desc:级别 
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Level { get; set; }

        /// <summary>
        /// Desc:中文名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:英文名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string EnName { get; set; }

        /// <summary>
        /// Desc:添加时间，作为排序使用
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;
    }
}
