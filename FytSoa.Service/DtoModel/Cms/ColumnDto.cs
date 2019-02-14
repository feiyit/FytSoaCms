using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 栏目树结构
    /// </summary>
    public class ColumnTree
    {
        public int Id { get; set; }

        public int ColumnId { get; set; }

        public string Name { get; set; }

        public string Href { get; set; }

        public int TempId { get; set; }

        public int Sort { get; set; }

        public bool Spread { get; set; } = true;

        public bool IsAjax { get; set; } = true;

        public List<ColumnTree> children { get; set; }
    }
}
