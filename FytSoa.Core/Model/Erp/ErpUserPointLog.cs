using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    /// <summary>
    /// 用户积分变动日志
    /// </summary>
    public class ErpUserPointLog
    {
        public ErpUserPointLog()
        {


        }
        /// <summary>
        /// Desc: 唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc: 用户唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string UserGuid { get; set; }

        /// <summary>
        /// Desc: 操作对象编号，如退货Guid等
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string OperateGuid { get; set; }

        /// <summary>
        /// Desc: 积分类型，0=增加1=减少
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Types { get; set; } = 0;

        /// <summary>
        /// Desc: 变动积分值
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Point { get; set; } = 0;

        /// <summary>
        /// Desc: 备注
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc: 备注
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;
    }
}
