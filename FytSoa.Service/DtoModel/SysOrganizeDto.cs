using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 组织机构DTO
    /// </summary>
    public class SysOrganizeDto
    {
    }

    public class SysOrganizeTree
    {
        public string guid { get; set; }
        public string name { get; set; }
        public List<SysOrganizeTree> children { get; set; }
        public bool open { get; set; } = true;
    }
}
