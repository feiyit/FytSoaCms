using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 店铺会员表
    ///</summary>
    public partial class ErpShopUser
    {
        public ErpShopUser()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:归属于某个店铺
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ShopGuid { get; set; }

        /// <summary>
        /// Desc:会员姓名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string UserName { get; set; }

        /// <summary>
        /// Desc:手机号码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Mobile { get; set; }

        /// <summary>
        /// Desc:登录密码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string LoginPwd { get; set; }

        /// <summary>
        /// Desc:积分数
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Points { get; set; }

        /// <summary>
        /// Desc:性别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Sex { get; set; }

        /// <summary>
        /// Desc:状态 0=正常 1=账号冻结
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public byte Status { get; set; }

        /// <summary>
        /// Desc:注册时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime RegDate { get; set; }

        /// <summary>
        /// Desc:登录次数
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int LoginCount { get; set; }

        /// <summary>
        /// Desc:最后登录日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// Desc:上次登录日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? UpLoginDate { get; set; }

    }
}
