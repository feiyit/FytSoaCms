using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    public class SysBtnFunDto
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 归属菜单功能
        /// </summary>
        public string MenuGuid { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单功能类型
        /// </summary>
        public string FunType { get; set; }

        /// <summary>
        /// 是否授权
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 描述，可为空
        /// </summary>
        public string Summary { get; set; }
    }
}
