using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    public class SysCodeTypeDto
    {
        public string guid { get; set; }
        public string name { get; set; }
        public string parent { get; set; }
        public int sort { get; set; }
    }

    public class SysCodeTypeTree
    {
        public string guid { get; set; }
        public string name { get; set; }
        public List<SysCodeTypeTree> children { get; set; }
        public bool open { get; set; } = true;
    }
}
