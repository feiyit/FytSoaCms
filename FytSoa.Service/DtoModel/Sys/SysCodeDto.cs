using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    public class SysCodePostPage
    {
        public string guid { get; set; }

        public int limit { get; set; } = 10;

        public int page { get; set; } = 1;
    }
    /// <summary>
    /// 角色授权显示权限值
    /// </summary>
    public class SysCodeDto
    {
        public string guid { get; set; }

        public string name { get; set; }

        public string codeType { get; set; }

        public bool status { get; set; } = false;
    }
}
