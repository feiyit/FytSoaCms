using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Erp
{
    ///<summary>
    /// 店铺表
    ///</summary>
    public partial class ErpShops
    {
        public ErpShops()
        {


        }
        /// <summary>
        /// Desc:唯一编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Guid { get; set; }

        /// <summary>
        /// Desc:店铺登录账号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string LoginName { get; set; }

        /// <summary>
        /// Desc:店铺登录密码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string LoginPwd { get; set; }

        /// <summary>
        /// Desc:负责人姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string AdminName { get; set; }

        /// <summary>
        /// Desc:性别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Sex { get; set; }

        /// <summary>
        /// Desc:店铺封面图
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ShopCover { get; set; }

        /// <summary>
        /// Desc:审核状态 0=正常 1=账号冻结  2=停业
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public byte Status { get; set; }

        /// <summary>
        /// Desc:店铺名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ShopName { get; set; }

        /// <summary>
        /// Desc:负责人联系电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Mobile { get; set; }

        /// <summary>
        /// Desc:座机号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Tel { get; set; }

        /// <summary>
        /// Desc:店铺所在区域
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ShopCity { get; set; }

        /// <summary>
        /// Desc:店铺详细地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ShopAddress { get; set; }

        /// <summary>
        /// Desc:店铺Logo
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ShopLogo { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Summary { get; set; }

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
        /// Desc:最后登录时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// Desc:上次登录时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? UpLoginDate { get; set; }

    }
}
