using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using System.Collections;

namespace Ixq.Core.Cache
{
    public class MemoryCache : ICache
    {
        private readonly string _region;
        public readonly ObjectCache _cache;

        public MemoryCache(string region)
        {
            _cache = new System.Runtime.Caching.MemoryCache(region);
            _region = region;
        }
        public virtual object Get(string key)
        {
            string cacheKey = GetCacheKey(key);
            return _cache.Get(cacheKey);
        }

        public virtual Task<object> GetAsync(string key)
        {
            return Task.FromResult(Get(key));
        }

        public virtual T Get<T>(string key)
        {
            string cacheKey = GetCacheKey(key);
            object value = _cache.Get(cacheKey);
            if (value == null)
            {
                return default(T);
            }
            return (T)value;
        }

        public virtual Task<T> GetAsync<T>(string key)
        {
            return Task.FromResult(Get<T>(key));
        }

        public virtual void Set<T>(string key, T value)
        {
            string cacheKey = GetCacheKey(key);
            CacheItemPolicy policy = new CacheItemPolicy();
            _cache.Set(cacheKey, value, policy);
        }

        public virtual Task SetAsync<T>(string key, T value)
        {
            return Task.Run(() => Set<T>(key, value));
        }

        public virtual void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            string cacheKey = GetCacheKey(key);
            CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = absoluteExpiration };
            _cache.Set(cacheKey, value, policy);
        }

        public virtual Task SetAsync<T>(string key, T value, DateTime absoluteExpiration)
        {
            return Task.Run(() => Set(key, value, absoluteExpiration));
        }

        public virtual void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            string cacheKey = GetCacheKey(key);
            CacheItemPolicy policy = new CacheItemPolicy() { SlidingExpiration = slidingExpiration };
            _cache.Set(cacheKey, value, policy);
        }

        public virtual Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration)
        {
            return Task.Run(() => Set(key, value, slidingExpiration));
        }

        public virtual void Remove(string key)
        {
            string cacheKey = GetCacheKey(key);
            _cache.Remove(cacheKey);
        }

        public virtual Task RemoveAsync(string key)
        {
            return Task.Run(() => Remove(key));
        }

        public virtual void Clear()
        {
            string token = _region + ":";
            List<string> cacheKeys = _cache.Where(m => m.Key.StartsWith(token)).Select(m => m.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                _cache.Remove(cacheKey);
            }
        }

        public virtual Task ClearAsync()
        {
            return Task.Run(() => Clear());
        }


        private string GetCacheKey(string key)
        {
            return string.Concat(_region, ":", key, "@", key.GetHashCode());
        }
    }
}
