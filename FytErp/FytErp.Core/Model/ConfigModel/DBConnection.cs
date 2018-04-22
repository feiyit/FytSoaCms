using System;
using System.Collections.Generic;
using System.Text;

namespace FytErp.Core.Model.ConfigModel
{
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public class DbConnection
    {
        /// <summary>
        /// MySql数据库连接字符串
        /// </summary>
        public string MySqlConnectionString { get; set; }

        /// <summary>
        /// SqlServer数据库连接字符串
        /// </summary>
        public string SqlServerConnectionString { get; set; }
    }
}
