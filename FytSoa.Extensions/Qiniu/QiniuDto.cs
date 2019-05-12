using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Extensions
{
    public class QiniuListParmDto
    {
        public string prefix { get; set; }

        public string marker { get; set; }
    }

    public class QiniuDelParmDto
    {
        public string filename { get; set; }
    }

    public class QiniuDelByPathParmDto
    {
        public string prefix { get; set; }

        public string filepath { get; set; }
    }
}
