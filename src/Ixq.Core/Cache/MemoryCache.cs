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
        private bool _disposed = false;
        private readonly string _region;
        private readonly System.Runtime.Caching.MemoryCache _cache;

        public MemoryCache(string region)
        {
            _cache = new System.Runtime.Caching.MemoryCache(region);
            _region = region;
        }
        public virtual object Get(string key)
        {
            return _cache.Get(key);
        }

        public virtual Task<object> GetAsync(string key)
        {
            return Task.FromResult(Get(key));
        }

        public virtual T Get<T>(string key)
        {
            object value = _cache.Get(key);
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
            CacheItemPolicy policy = new CacheItemPolicy();
            _cache.Set(key, value, policy);
        }

        public virtual Task SetAsync<T>(string key, T value)
        {
            return Task.Run(() => Set<T>(key, value));
        }

        public virtual void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = absoluteExpiration };
            _cache.Set(key, value, policy);
        }

        public virtual Task SetAsync<T>(string key, T value, DateTime absoluteExpiration)
        {
            return Task.Run(() => Set(key, value, absoluteExpiration));
        }

        public virtual void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            CacheItemPolicy policy = new CacheItemPolicy() { SlidingExpiration = slidingExpiration };
            _cache.Set(key, value, policy);
        }

        public virtual Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration)
        {
            return Task.Run(() => Set(key, value, slidingExpiration));
        }

        public virtual void Remove(string key)
        {
            _cache.Remove(key);
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

        public void Dispose()
        {
            if (_disposed)
                return;
            _cache.Dispose();
            _disposed = true;
        }

        public virtual IEnumerable<KeyValuePair<string, object>> GetAll()
        {
            return _cache.AsEnumerable();
        }

        public virtual Task<IEnumerable<KeyValuePair<string, object>>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }
    }
}
