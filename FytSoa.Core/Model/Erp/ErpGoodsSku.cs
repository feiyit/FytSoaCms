using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 商品条形码表
    ///</summary>
    public partial class ErpGoodsSku
    {
        public ErpGoodsSku()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:商品编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GoodsGuid { get; set; }

        /// <summary>
        /// Desc:条形码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Code { get; set; }

        /// <summary>
        /// Desc:销售价格
        /// Default:0.00
        /// Nullable:False
        /// </summary>           
        public string SalePrice { get; set; }

        /// <summary>
        /// Desc:折扣价格
        /// Default:0.00
        /// Nullable:False
        /// </summary>           
        public string DisPrice { get; set; }

        /// <summary>
        /// Desc:库存
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int StockSum { get; set; } = 0;

        /// <summary>
        /// Desc:销售数量
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int SaleSum { get; set; } = 0;

        /// <summary>
        /// Desc:状态 1=正常 2=异常
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public byte Status { get; set; } = 1;

        /// <summary>
        /// Desc:品牌编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BrankGuid { get; set; }

        /// <summary>
        /// Desc:季节编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SeasonGuid { get; set; }

        /// <summary>
        /// Desc:款式编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string StyleGuid { get; set; }

        /// <summary>
        /// Desc:批次编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BatchGuid { get; set; }

        /// <summary>
        /// Desc:尺码编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SizeGuid { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public string YearGuid { get; set; }

        /// <summary>
        /// 是否删除  0=否 1=是
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;

    }
}
