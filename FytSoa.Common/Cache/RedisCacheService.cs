using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Newtonsoft.Json;

namespace FytSoa.Common
{
    public class RedisCacheService:ICacheService
    {
        private readonly IDistributedCache _cache;
        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }


        /// <summary>
        /// 是否存在此缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            return !string.IsNullOrEmpty(_cache.GetString(key));
        }

        /// <summary>
        /// 取得缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetCache<T>(string key) where T : class
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var cacheVal = _cache.GetString(key);
            return JsonConvert.DeserializeObject<T>(cacheVal);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetCache(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions
            {
                //设置过期默认为一个月
                AbsoluteExpiration = DateTime.Now.AddMonths(1)
            };
            // 添加缓存
            _cache.Set(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options);
            //刷新缓存
            _cache.Refresh(key);
        }

        /// <summary>
        /// 设置缓存,绝对过期  分钟
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationMinute">滑动过期时间</param>
        /// MemoryCacheService.Default.SetCache("test", "RedisCache works!", 30);
        public void SetCache(string key, object value, double expirationMinute)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions
            {
                //设置过期-间隔分钟
                AbsoluteExpiration = DateTime.Now.AddMinutes(expirationMinute)
            };
            // 添加缓存
            _cache.Set(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options);
            //刷新缓存
            _cache.Refresh(key);
        }

        /// <summary>
        /// 设置缓存,绝对过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime">DateTimeOffset 结束时间</param>
        /// MemoryCacheService.Default.SetCache("test", "RedisCache works!", DateTimeOffset.Now.AddSeconds(30));
        public void SetCache(string key, object value, DateTimeOffset expirationTime)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions
            {
                //设置过期-时间
                AbsoluteExpiration = expirationTime
            };
            // 添加缓存
            _cache.Set(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options);
            //刷新缓存
            _cache.Refresh(key);
        }

        /// <summary>
        /// 设置缓存,相对过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="t"></param>
        /// MemoryCacheService.Default.SetCache("test", "MemoryCache works!",TimeSpan.FromSeconds(30));
        public void SetSlidingCache(string key, object value, TimeSpan t)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions
            {
                //设置过期-间隔分钟
                SlidingExpiration = t
            };
            // 添加缓存
            _cache.Set(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options);
            //刷新缓存
            _cache.Refresh(key);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public void RemoveCache(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            _cache.Remove(key);
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
