using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    public class SysMenuDto
    {
    }
    public class SysMenuTree
    {
        public string guid { get; set; }
        public string name { get; set; }
        public List<SysMenuTree> children { get; set; }
        public bool open { get; set; } = true;
    }
}
