using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    public class StaffModifyPwdParm
    {
        /// <summary>
        /// 员工编号
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 原密码
        /// </summary>
        public string HistoryPwd { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPwd { get; set; }
    }

    public class ShopBasicDto
    {
        /// <summary>
        /// 员工编号
        /// </summary>
        public string StaffGuid { get; set; }
        /// <summary>
        /// 店铺编号
        /// </summary>
        public string ShopGuid { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string AdminName { get; set; }
        /// <summary>
        /// 员工联系方式
        /// </summary>
        public string Mobile { get; set; }
    }

    /// <summary>
    /// 商家登录，或者商家员工登录请求参数
    /// </summary>
    public class StaffLoginDto
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string loginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string loginPwd { get; set; }

        /// <summary>
        /// 推送用的到token
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 设备类型，是苹果还是安卓  0=苹果 1=安卓
        /// </summary>
        public int isDevice { get; set; }

        /// <summary>
        /// 设备类型名称，是苹果还是安卓
        /// </summary>
        public string deviceName { get; set; }
    }
}
