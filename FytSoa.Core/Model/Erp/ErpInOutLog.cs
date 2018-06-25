using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 出入库日志表
    ///</summary>
    public partial class ErpInOutLog
    {
        public ErpInOutLog()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:类型 1=入库 2=出库
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public byte Types { get; set; } = 1;

        /// <summary>
        /// Desc:出入库打包日志的编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string PackGuid { get; set; }

        /// <summary>
        /// Desc:出库商品到店铺的编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ShopGuid { get; set; }

        /// <summary>
        /// Desc:出入库商品的编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GoodsSku { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brank { get; set; }

        /// <summary>
        /// 款式
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// Desc:出入库商品的数量
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int GoodsSum { get; set; } = 0;

        /// <summary>
        /// Desc:后台管理人员的编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string AdminGuid { get; set; }

        /// <summary>
        /// Desc:出入库的时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;

    }
}
