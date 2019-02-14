using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Common
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// 是否存在此缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 取得缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetCache<T>(string key) where T : class;

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetCache(string key, object value);

        /// <summary>
        /// 设置缓存,绝对过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationMinute">间隔分钟</param>
        /// MemoryCacheService.Default.SetCache("test", "RedisCache works!", 30);
        void SetCache(string key, object value, double expirationMinute);

        /// <summary>
        /// 设置缓存,绝对过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime">DateTimeOffset 结束时间</param>
        /// MemoryCacheService.Default.SetCache("test", "RedisCache works!", DateTimeOffset.Now.AddSeconds(30));
        void SetCache(string key, object value, DateTimeOffset expirationTime);

        /// <summary>
        /// 设置缓存,相对过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="t"></param>
        /// MemoryCacheService.Default.SetCache("test", "MemoryCache works!",TimeSpan.FromSeconds(30));
        void SetSlidingCache(string key, object value, TimeSpan t);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        void RemoveCache(string key);

        /// <summary>
        /// 释放
        /// </summary>
        void Dispose();
    }
}
