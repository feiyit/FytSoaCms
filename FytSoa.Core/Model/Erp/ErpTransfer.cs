using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 调拨
    ///</summary>
    public partial class ErpTransfer
    {
        public ErpTransfer()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Number { get; set; }

        /// <summary>
        /// Desc:入库加盟商
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string InShopGuid { get; set; }

        /// <summary>
        /// Desc:出库加盟商
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string OutShopGuid { get; set; }

        /// <summary>
        /// Desc:商品数量
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int GoodsSum { get; set; } = 0;

        /// <summary>
        /// Desc:操作人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string AdminGuid { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;

    }
}
