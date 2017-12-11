using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Ixq.Core.Cache
{
    /// <summary>
    ///     内存缓存。
    /// </summary>
    public class MemoryCache : ICache, IDisposable
    {
        private readonly System.Runtime.Caching.MemoryCache _cache;
        private readonly string _region;

        /// <summary>
        ///     初始化一个<see cref="MemoryCache" />实例。
        /// </summary>
        /// <param name="region"></param>
        public MemoryCache(string region)
        {
            _cache = new System.Runtime.Caching.MemoryCache(region);
            _region = region;
        }

        /// <summary>
        ///     获取缓存区域名称。
        /// </summary>
        /// <returns></returns>
        public virtual string GetRegionName()
        {
            return _region;
        }

        /// <summary>
        ///     获取缓存项。
        /// </summary>
        /// <param name="key">缓存项的唯一标识符。</param>
        /// <returns>缓存项。</returns>
        public virtual object Get(string key)
        {
            return _cache.Get(key);
        }

        /// <summary>
        ///     异步获取缓存项。
        /// </summary>
        /// <param name="key">缓存项的唯一标识符。</param>
        /// <returns>缓存项。</returns>
        public virtual Task<object> GetAsync(string key)
        {
            return Task.FromResult(Get(key));
        }

        /// <summary>
        ///     获取缓存项。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">缓存项的唯一标识符。</param>
        /// <returns>缓存项。</returns>
        public virtual T Get<T>(string key)
        {
            var value = _cache.Get(key);
            if (value == null)
                return default(T);
            return (T) value;
        }

        /// <summary>
        ///     异步获取缓存项。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">缓存项的唯一标识符。</param>
        /// <returns>缓存项。</returns>
        public virtual Task<T> GetAsync<T>(string key)
        {
            return Task.FromResult(Get<T>(key));
        }

        /// <summary>
        ///     将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="value">该缓存项的数据。</param>
        public virtual void Set<T>(string key, T value)
        {
            var policy = new CacheItemPolicy();
            _cache.Set(key, value, policy);
        }

        /// <summary>
        ///     异步将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="value">该缓存项的数据。</param>
        public virtual Task SetAsync<T>(string key, T value)
        {
            Set(key, value);
            return Task.FromResult(0);
        }

        /// <summary>
        ///     将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="second">有效时长，单位：秒。</param>
        /// <param name="value">该缓存项的数据。</param>
        public virtual void Set<T>(string key, T value, int second)
        {
            var absoluteExpiration = DateTime.Now.AddSeconds(second);
            Set(key, value, absoluteExpiration);
        }

        /// <summary>
        ///     异步将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="second">有效时长，单位：秒。</param>
        /// <param name="value">该缓存项的数据。</param>
        public virtual Task SetAsync<T>(string key, T value, int second)
        {
            Set(key, value, second);
            return Task.FromResult(0);
        }

        /// <summary>
        ///     将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="absoluteExpiration">缓存项过期时间。</param>
        /// <param name="value">该缓存项的数据。</param>
        public virtual void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            var policy = new CacheItemPolicy {AbsoluteExpiration = absoluteExpiration};
            _cache.Set(key, value, policy);
        }

        /// <summary>
        ///     异步将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="absoluteExpiration">缓存项过期时间。</param>
        /// <param name="value">该缓存项的数据。</param>
        public virtual Task SetAsync<T>(string key, T value, DateTime absoluteExpiration)
        {
            Set(key, value, absoluteExpiration);
            return Task.FromResult(0);
        }

        /// <summary>
        ///     将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="slidingExpiration">在此时间内访问缓存，缓存将继续有效。</param>
        /// <param name="value">该缓存项的数据。</param>
        public virtual void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            var policy = new CacheItemPolicy {SlidingExpiration = slidingExpiration};
            _cache.Set(key, value, policy);
        }

        /// <summary>
        ///     异步将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="slidingExpiration">在此时间内访问缓存，缓存将继续有效。</param>
        /// <param name="value">该缓存项的数据。</param>
        public virtual Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration)
        {
            Set(key, value, slidingExpiration);
            return Task.FromResult(0);
        }

        /// <summary>
        ///     从缓存中移除某个缓存项。
        /// </summary>
        /// <param name="key">要移除的缓存项的唯一标识符。</param>
        public virtual void Remove(string key)
        {
            _cache.Remove(key);
        }

        /// <summary>
        ///     异步从缓存中移除某个缓存项。
        /// </summary>
        /// <param name="key">要移除的缓存项的唯一标识符。</param>
        public virtual Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.FromResult(0);
        }

        /// <summary>
        ///     清空缓存。
        /// </summary>
        public virtual void Clear()
        {
            var cacheKeys = _cache.Select(m => m.Key).ToList();
            foreach (var cacheKey in cacheKeys)
                _cache.Remove(cacheKey);
        }

        /// <summary>
        ///     异步清空缓存。
        /// </summary>
        /// <returns></returns>
        public virtual Task ClearAsync()
        {
            Clear();
            return Task.FromResult(0);
        }

        /// <summary>
        ///     获取全部的缓存项。
        /// </summary>
        /// <returns>全部的缓存项。</returns>
        public virtual IEnumerable<KeyValuePair<string, object>> GetAll()
        {
            return _cache.AsEnumerable();
        }

        /// <summary>
        ///     异步获取全部的缓存项。
        /// </summary>
        /// <returns>全部的缓存项。</returns>
        public virtual Task<IEnumerable<KeyValuePair<string, object>>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        public void Dispose()
        {
            _cache?.Dispose();
        }
    }
}