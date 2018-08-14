using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    public class UserRegReport
    {
        /// <summary>
        /// 月份
        /// </summary>
        public string Months { get; set; }

        /// <summary>
        /// 注册数
        /// </summary>
        public int RegCount { get; set; } = 0;
    }
}
