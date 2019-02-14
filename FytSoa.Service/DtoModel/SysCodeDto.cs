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
}
