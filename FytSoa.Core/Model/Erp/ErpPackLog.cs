using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 出入库打包日志表
    ///</summary>
    public partial class ErpPackLog
    {
        public ErpPackLog()
        {


        }
        /// <summary>
        /// Desc:唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:类型：1=出库打包日志  2=入库打包日志
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public byte Types { get; set; } = 1;

        /// <summary>
        /// Desc:打包订单号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Number { get; set; }

        /// <summary>
        /// Desc:打包名称（可以是时间）
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string PackName { get; set; }

        /// <summary>
        /// Desc:当前打包商品总数量
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int GoodsSum { get; set; } = 0;

        /// <summary>
        /// Desc:打包的商品归属商铺编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ShopGuid { get; set; }

        /// <summary>
        /// 是否删除  0=否 1=是
        /// </summary>
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// Desc:打包时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;

    }
}
