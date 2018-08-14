using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 会员等级
    ///</summary>
    public partial class ErpUserGrade
    {
        public ErpUserGrade()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:会员等级名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:会员等级图标
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Icon { get; set; }

        /// <summary>
        /// Desc:0=积分类型   1=经验类型
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public byte IsTypes { get; set; } = 1;

        /// <summary>
        /// Desc:所需经验值
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int ExpVal { get; set; } = 0;

        /// <summary>
        /// Desc:所需最小积分
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Point { get; set; } = 0;

        /// <summary>
        /// Desc:所需金额
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public decimal Amount { get; set; } = 0;

        /// <summary>
        /// Desc:购物折扣
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int DisCount { get; set; } = 0;

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Desc:修改时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? EditDate { get; set; }

    }
}
