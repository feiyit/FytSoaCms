using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 调拨商品
    ///</summary>
    public partial class ErpTransferGoods
    {
           public ErpTransferGoods(){


           }
           /// <summary>
           /// Desc:唯一编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Guid {get;set;}

           /// <summary>
           /// Desc:调拨单编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string TransferGuid {get;set;}

           /// <summary>
           /// Desc:商品编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string GoodsGuid {get;set;}

           /// <summary>
           /// Desc:商品数量
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int GoodsSum {get;set;}

    }
}
