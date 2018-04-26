using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        /// <summary>
        /// 获得配置文件的对象值
        /// </summary>
        /// <param name="jsonPath"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetJson(string jsonPath, string key)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile(jsonPath).Build(); //json文件地址
            string s = config.GetSection(key).Value; //json某个对象
            return s;
        }

        /// <summary>
        /// 根据配置文件和Key获得对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">文件名称</param>
        /// <param name="key">节点Key</param>
        /// <returns></returns>
        public static T GetAppSettings<T>(string fileName, string key) where T : class, new()
        {
            var baseDir = AppContext.BaseDirectory + "json/";
            var currentClassDir = baseDir;

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(currentClassDir)
                .Add(new JsonConfigurationSource { Path = fileName, Optional = false, ReloadOnChange = true })
                .Build();
            var appconfig = new ServiceCollection().AddOptions()
                .Configure<T>(config.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
            return appconfig;
        }
    }
}
