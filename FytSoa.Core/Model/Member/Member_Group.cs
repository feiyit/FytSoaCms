using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Member
{
    ///<summary>
    ///  用户组管理
    ///</summary>
    public partial class Member_Group
    {
        public Member_Group()
        {


        }
        /// <summary>
        /// Desc:唯一编号 唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:等级 等级
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Level { get; set; } = 0;

        /// <summary>
        /// Desc:组名称 组名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:等级图片 等级图片
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Images { get; set; }

        /// <summary>
        /// Desc:升级积分 升级积分
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int UpPoint { get; set; } = 0;

        /// <summary>
        /// Desc:升级金额 升级金额
        /// Default:0.00
        /// Nullable:False
        /// </summary>           
        public decimal UpMoney { get; set; } = 0;

        /// <summary>
        /// Desc:购物折扣 购物折扣
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int DisCount { get; set; } = 0;

        /// <summary>
        /// Desc:状态 状态
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool Status { get; set; } = true;

        /// <summary>
        /// Desc:是否删除
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool IsDel { get; set; } = false;

    }
}
