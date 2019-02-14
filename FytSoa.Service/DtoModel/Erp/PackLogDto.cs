using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    public class PackLogDto
    {
        /// <summary>
        /// Desc:唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

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
        /// 加盟商名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// Desc:打包时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// 出入库搜索
    /// </summary>
    public class SearchParm
    {
        public string shopGuid { get; set; }
        public string packGuid { get; set; }
        public string brank { get; set; }
        public string size { get; set; }
    }
}
