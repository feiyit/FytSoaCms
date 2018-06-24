using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 商品表
    ///</summary>
    public partial class ErpGoods
    {
        public ErpGoods()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brank { get; set; }

        /// <summary>
        /// 款式
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// 状态 1=上架  2=下架  3=其他
        /// </summary>
        public byte Status { get; set; } = 1;

        /// <summary>
        /// 规格属性
        /// </summary>
        public string Attribute { get; set; }

        /// <summary>
        /// 是否删除  0=否 1=是
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate { get; set; } = DateTime.Now;

    }
}
