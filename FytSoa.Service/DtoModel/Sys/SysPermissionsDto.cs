using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 角色授权，入参
    /// </summary>
    public class SysPermissionsParm
    {
        /// <summary>
        /// 角色
        /// </summary>
        public string role { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public string menu { get; set; }

        /// <summary>
        /// 按钮的值
        /// </summary>
        public string btnfun { get; set; }

        /// <summary>
        /// 状态 1=授权  0=取消
        /// </summary>
        public int status { get; set; } = 1;
    }
}
