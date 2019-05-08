using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Wx
{
    ///<summary>
    /// 微信素材表
    ///</summary>
    [SugarTable("Wx_material")]
    public partial class WxMaterial
    {
        public WxMaterial()
        {


        }
        /// <summary>
        /// Desc:自动增长
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:所属公众号ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int WxId { get; set; }

        /// <summary>
        /// Desc:类型，1=图文  2=连接
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public byte Type { get; set; } = 1;

        /// <summary>
        /// Desc:保存位置  1=本地 2=服务器
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public byte Position { get; set; } = 1;

        /// <summary>
        /// Desc:标题
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:图片
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Img { get; set; }

        /// <summary>
        /// Desc:描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:作者
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Author { get; set; }

        /// <summary>
        /// Desc:连接
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Link { get; set; }

        /// <summary>
        /// Desc:内容
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Content { get; set; }

        /// <summary>
        /// Desc:内容Json
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TestJson { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;

    }
}
