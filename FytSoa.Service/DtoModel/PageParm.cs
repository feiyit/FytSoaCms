﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 查询count
    /// </summary>
    public class ResultCount
    {
        public int Count { get; set; } = 0;
    }

    /// <summary>
    /// 是否存在
    /// </summary>
    public class ResultAny
    {
        public bool Any { get; set; } = false;
    }

    /// <summary>
    /// 自定义排序
    /// </summary>
    public class ParmSort
    {
        public int p { get; set; }
        public int i { get; set; }
        public int o { get; set; }
    }

    /// <summary>
    /// 自定义排序
    /// </summary>
    public class ParmStringSort
    {
        public string p { get; set; }
        public string i { get; set; }
        public int o { get; set; }
    }

    /// <summary>
    /// 提供字符串FromBody的解析
    /// </summary>
    public class ParmString
    {
        public string parm { get; set; }
    }

    /// <summary>
    /// 提供字符串FromBody的解析
    /// </summary>
    public class ParmInt
    {
        public int id { get; set; }
    }

    /// <summary>
    /// 分页参数
    /// </summary>
    public class PageParm
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; } = 1;

        /// <summary>
        /// 每页总条数
        /// </summary>
        public int limit { get; set; } = 15;

        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; } = 0;

        /// <summary>
        /// 编号
        /// </summary>
        public string guid { get; set; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 类型条件
        /// </summary>
        public int types { get; set; } = 0;

        /// <summary>
        /// 审核状态
        /// </summary>
        public int audit { get; set; } = -1;

        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; } = -1;

        /// <summary>
        /// 属性
        /// </summary>
        public int attr { get; set; } = 0;

        /// <summary>
        /// 搜索日期，可能是2个日期，通过-分隔
        /// </summary>
        public string time { get; set; }

        /// <summary>
        /// 排序方式，可根据数字来判断，
        /// </summary>
        public int orderType { get; set; } = 0;

        /// <summary>
        /// 排序的字段
        /// </summary>
        public string field { get; set; }

        /// <summary>
        /// 排序的类型 asc  desc
        /// </summary>
        public string order { get; set; } = "desc";

        /// <summary>
        /// 动态条件
        /// </summary>
        public string where { get; set; }

        /// <summary>
        /// 站点
        /// </summary>
        public string site { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string number { get; set; }
    }

    public class AppSearchParm
    {
        public string btime { get; set; }
        public string etime { get; set; }
        public string brand { get; set; }
        public int saleType { get; set; } = 0;
        public int activityTypes { get; set; } = 0;
    }
}
