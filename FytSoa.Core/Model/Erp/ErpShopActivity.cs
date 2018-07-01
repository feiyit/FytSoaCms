using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 商品活动表
    ///</summary>
    public partial class ErpShopActivity
    {
        public ErpShopActivity()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:店铺编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ShopGuid { get; set; }

        /// <summary>
        /// Desc:品牌编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string BrandGuid { get; set; }

        /// <summary>
        /// Desc:类型(0=全部店铺/1=按商铺/2=按品牌/3=按地区)
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public byte Types { get; set; } = 1;

        /// <summary>
        /// Desc:方式(1=打折/2=满减)
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public byte Method { get; set; } = 1;

        /// <summary>
        /// Desc:折扣数
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? CountNum { get; set; }

        /// <summary>
        /// Desc:满减Json
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FullBack { get; set; }

        /// <summary>
        /// Desc:开始时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// Desc:结束时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 是否启用  1=启用  0=不启用
        /// </summary>
        public bool Enable { get; set; } = true;

        /// <summary>
        /// 是否删除  0=不删除  1=删除
        /// </summary>
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// Desc:活动添加时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;

    }
}
