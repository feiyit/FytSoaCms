using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    /// <summary>
    /// 返货订单表
    /// </summary>
    public class ErpReturnOrder
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// 返货订单号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 所属返货店铺
        /// </summary>
        public string ShopGuid { get; set; }

        /// <summary>
        /// 返货数量
        /// </summary>
        public int GoodsSum { get; set; } = 0;

        /// <summary>
        /// 返货的状态 1=提交返货 2=受理 3=完成 4=其他
        /// </summary>
        public int Status { get; set; } = 3;

        /// <summary>
        /// 操作人-员工编号
        /// </summary>
        public string StaffGuid { get; set; }

        /// <summary>
        /// 是否删除 0=否 1=是
        /// </summary>
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate { get; set; } = DateTime.Now;
    }
}
