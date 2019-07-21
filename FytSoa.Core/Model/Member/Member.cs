using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Member
{
    ///<summary>
    /// 用户表 
    ///</summary>
    public partial class Member
    {
        public Member()
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
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Grade { get; set; }

        /// <summary>
        /// Desc:登录账号 登录账号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string LoginName { get; set; }

        /// <summary>
        /// Desc:登录密码 登录密码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string LoginPwd { get; set; }

        /// <summary>
        /// Desc:真实姓名 真实姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string NickName { get; set; }

        /// <summary>
        /// Desc:手机号码 手机号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Mobile { get; set; }

        /// <summary>
        /// Desc:邮箱 邮箱
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Email { get; set; }

        /// <summary>
        /// Desc:签名 签名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Autograph { get; set; }

        /// <summary>
        /// Desc:头像 头像
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HeadPic { get; set; }

        /// <summary>
        /// Desc:金额
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public decimal Money { get; set; } = 0;

        /// <summary>
        /// Desc:积分 积分
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Point { get; set; } = 0;

        /// <summary>
        /// Desc:是否删除
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// Desc:状态 状态
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool Status { get; set; } = true;

        /// <summary>
        /// Desc:登录时间 登录时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? LoginTime { get; set; }

        /// <summary>
        /// Desc:登录次数 登录次数
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int LoginSum { get; set; } = 0;

        /// <summary>
        /// Desc:注册时间 注册时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime RegTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Desc:组名称
        /// Default:
        /// Nullable:True
        /// </summary>   
        [SugarColumn(IsIgnore = true)]
        public string Name { get; set; }


    }
}
