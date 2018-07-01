using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 采购单Dto
    /// </summary>
    public class PurchaseDto
    {
        /// <summary>
        /// 采购单唯一编号
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 采购单编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 入库状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }
        /// <summary>
        /// 采购时间
        /// </summary>
        public DateTime AddDate { get; set; }
        /// <summary>
        /// 采购单总金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 交付日期
        /// </summary>
        public DateTime DeliverDate { get; set; }
        /// <summary>
        /// 交付地点
        /// </summary>
        public string DeliverCity { get; set; }
    }
}
