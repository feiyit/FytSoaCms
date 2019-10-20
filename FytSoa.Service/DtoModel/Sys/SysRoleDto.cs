using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    public class SysRoleDto
    {
        public string guid { get; set; }
        public string name { get; set; }
        public string codes { get; set; }
        public string DepartmentGroup { get; set; }
        public string ParentGuid { get; set; }
        public int sort { get; set; }
        public int level { get; set; }
        public bool status { get; set; }
    }

    /// <summary>
    /// 查询授权列表 根据登录人信息
    /// </summary>
    public class RoleByAdminParam
    {
        public string key { get; set; }

        public string adminGuid { get; set; }
    }
}
