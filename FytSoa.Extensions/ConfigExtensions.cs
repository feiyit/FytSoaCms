using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace FytSoa.Extensions
{
    /// <summary>
    /// 读取配置文件信息
    /// </summary>
    public class ConfigExtensions
    {
        public static IConfiguration Configuration { get; set; }
        static ConfigExtensions()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
        }
    }
}
