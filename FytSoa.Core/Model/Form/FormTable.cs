using System;
using System.Collections.Generic;
using SqlSugar;

namespace FytSoa.Core.Model.Form
{
    [SugarTable("Form_Table")]
    public class FormTable
    {
        public FormTable()
        {
        }
        /// <summary>
        /// 唯一编号
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 万用表单的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 万用表单的名称
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// 表结构Json
        /// </summary>
        [SugarColumn(IsJson = true)]
        public List<TableBody> TableJson { get; set; } = new List<TableBody>();


        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// 表单创建时间
        /// </summary>
        public DateTime AddDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 表单修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }


    /// <summary>
    /// 表结构体
    /// </summary>
    public class TableBody
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string Types { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; } = 0;

        /// <summary>
        /// 小数点保留2位
        /// </summary>
        public int Decimals { get; set; } = 0;

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNull { get; set; }=true;

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsKey { get; set; } = false;

        /// <summary>
        /// 字段备注
        /// </summary>
        public string Comment { get; set; }
    }
}
