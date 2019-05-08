using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Sys
{
    ///<summary>
    /// 图片表
    ///</summary>
    public partial class SysImage
    {
        public SysImage()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:所属栏目Guid
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TheGuid { get; set; }

        /// <summary>
        /// Desc:图片类型，一个栏目可有多个图片类型
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Types { get; set; }

        /// <summary>
        /// Desc:图片名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:图片原图
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ImgBig { get; set; }

        /// <summary>
        /// Desc:图片缩略图
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ImgSmall { get; set; }

        /// <summary>
        /// Desc:是否为封面
        /// Default:b'0'
        /// Nullable:False
        /// </summary>           
        public bool IsCover { get; set; }

        /// <summary>
        /// Desc:排序字段
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Sort { get; set; }

    }
}
