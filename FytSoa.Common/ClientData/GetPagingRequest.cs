using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Common
{
    public class GetPagingRequest
    {
        public GetPagingRequest(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; private set; }
        /// <summary>
        /// 每页的记录数
        /// </summary>
        public int PageSize { get; private set; }


        public bool IsDelete { get; set; } = false;
    }
}
