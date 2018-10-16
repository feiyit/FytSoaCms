using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    /// <summary>
    /// 返货报损
    /// </summary>
    public class ErpReturnLoss
    {
        public ErpReturnLoss()
        {


        }
        /// <summary>
        /// Desc: 唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc: 条形码，可输入中文
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string CodeName { get; set; }

        /// <summary>
        /// Desc: 描述
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc: 数量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Count { get; set; } = 0;

        /// <summary>
        /// Desc: 添加时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Desc: 操作人
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string AdminGuid { get; set; }
    }
}
