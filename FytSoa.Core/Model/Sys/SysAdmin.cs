﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FytSoa.Core.Model.Sys
{
    [SugarTable("Sys_Admin")]
    public class SysAdmin
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 归属角色
        /// </summary>
        public string RoleGuid { get; set; }

        /// <summary>
        /// 返回角色列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<AdminToRoleList> RoleList {
            get {
                var role = new List<AdminToRoleList>();
                if (!string.IsNullOrEmpty(RoleGuid))
                {
                    role= JsonConvert.DeserializeObject<List<AdminToRoleList>>(RoleGuid);
                }
                return role;
            } }

        /// <summary>
        /// 归属部门
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 归属部门
        /// </summary>
        public string DepartmentGuid { get; set; }

        /// <summary>
        /// 部门集合
        /// </summary>
        public string DepartmentGuidList { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPwd { get; set; }

        /// <summary>
        /// 真是姓名
        /// </summary>
        public string TrueName { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadPic { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; } = "男";

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 状态 1=整除 0=不允许登录
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 当前登录时间
        /// </summary>
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? UpLoginDate { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginSum { get; set; } = 1;

        /// <summary>
        /// 是否系统管理员
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool IsSystem { get; set; } = false;
    }

    /// <summary>
    /// 用户关联角色
    /// </summary>
    public class AdminToRoleList
    {
        public string name { get; set; }

        public string guid { get; set; }
    }
}
