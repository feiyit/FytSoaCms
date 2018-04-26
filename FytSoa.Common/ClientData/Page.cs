using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Common
{
    public class Page<T>
    {
        /// <summary>
        /// 当前页索引
        /// </summary>
        public long CurrentPage { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public long TotalPages { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public long TotalItems { get; set; }
        /// <summary>
        /// 每页的记录数
        /// </summary>
        public long ItemsPerPage { get; set; }
        /// <summary>
        /// 数据集
        /// </summary>
        public List<T> Items { get; set; }
    }
}
