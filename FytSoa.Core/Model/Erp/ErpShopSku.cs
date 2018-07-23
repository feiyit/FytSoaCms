using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 加盟商条形码表，有库存和销售
    ///</summary>
    public partial class ErpShopSku
    {
           public ErpShopSku(){


           }
           /// <summary>
           /// Desc:条形码的唯一编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SkuGuid {get;set;}

           /// <summary>
           /// Desc:条形码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SkuCode {get;set;}

           /// <summary>
           /// Desc:加盟商编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ShopGuid {get;set;}

        /// <summary>
        /// Desc:库存
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Stock { get; set; } = 0;

        /// <summary>
        /// Desc:销售数量
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Sale { get; set; } = 0;

        /// <summary>
        /// 修改库存时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// 增加库存时间
        /// </summary>
        public DateTime AddDate { get; set; } = DateTime.Now;

    }
}
