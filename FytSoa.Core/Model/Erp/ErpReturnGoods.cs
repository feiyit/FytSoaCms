using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 返货表
    ///</summary>
    public partial class ErpReturnGoods
    {
        public ErpReturnGoods()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:订单号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Number { get; set; }

        /// <summary>
        /// Desc:哪个店铺提出的返货
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ShopGuid { get; set; }

        /// <summary>
        /// Desc:哪个人操作的
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string AdminGuid { get; set; }

        /// <summary>
        /// Desc:返货的是哪件衣服
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GoodsSku { get; set; }

        /// <summary>
        /// Desc:返货的数量
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int ReturnCount { get; set; }

        /// <summary>
        /// Desc:属于哪个批次
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BatchGuid { get; set; }

        /// <summary>
        /// Desc:批次名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BatchName { get; set; }

        /// <summary>
        /// Desc:返货的状态 1=提交返货 2=受理 3=完成 4=其他
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public byte Status { get; set; }

        /// <summary>
        /// Desc:返货描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:提交返货的时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; }

    }
}
