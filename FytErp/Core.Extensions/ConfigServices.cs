using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Extensions
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public class ConfigServices
    {
        public static IConfiguration Configuration { get; set; }
        static ConfigServices()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "DbSetting.json", ReloadOnChange = true })
            .Build();
        }
    }
}
