using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using Ixq.Core.Cache;

namespace Ixq.Web.Mvc.Caching
{
    public class WebCache : ICache
    {
        private readonly Cache _cache = HttpRuntime.Cache;
        private readonly string _cacheKeyPrefix;
        private readonly string _cacheName;

        public WebCache(string name)
        {
            _cacheName = name;
            _cacheKeyPrefix = $"Ixq.Web.Mvc.Caching.WebCache.{name}.";
        }

        public virtual string GetRegionName()
        {
            return _cacheName;
        }

        public virtual IEnumerable<KeyValuePair<string, object>> GetAll()
        {
            var cacheEnum = _cache.GetEnumerator();
            while (cacheEnum.MoveNext())
                if (cacheEnum.Key != null && cacheEnum.Key.ToString().StartsWith(_cacheKeyPrefix))
                    yield return new KeyValuePair<string, object>(cacheEnum.Key.ToString().Replace(_cacheKeyPrefix, ""),
                        cacheEnum.Value);
        }

        public virtual Task<IEnumerable<KeyValuePair<string, object>>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        public virtual object Get(string key)
        {
            return _cache.Get(ParseKey(key));
        }

        public virtual Task<object> GetAsync(string key)
        {
            return Task.FromResult(Get(key));
        }

        public virtual T Get<T>(string key)
        {
            var value = _cache[ParseKey(key)];
            if (value != null)
                return (T) value;
            return default(T);
        }

        public virtual Task<T> GetAsync<T>(string key)
        {
            return Task.FromResult(Get<T>(key));
        }

        public virtual void Set<T>(string key, T value)
        {
            _cache.Insert(ParseKey(key), value);
        }

        public virtual Task SetAsync<T>(string key, T value)
        {
            Set(key, value);
            return Task.FromResult(0);
        }

        public virtual void Set<T>(string key, T value, int second)
        {
            var absoluteExpiration = DateTime.Now.AddSeconds(second);
            Set(key, value, absoluteExpiration);
        }

        public virtual Task SetAsync<T>(string key, T value, int second)
        {
            Set(key, value, second);
            return Task.FromResult(0);
        }

        public virtual void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            _cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration);
        }

        public virtual Task SetAsync<T>(string key, T value, DateTime absoluteExpiration)
        {
            Set(key, value, absoluteExpiration);
            return Task.FromResult(0);
        }

        public virtual void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            _cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration);
        }

        public virtual Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration)
        {
            Set(key, value, slidingExpiration);
            return Task.FromResult(0);
        }

        public virtual void Remove(string key)
        {
            _cache.Remove(ParseKey(key));
        }

        public virtual Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.FromResult(0);
        }

        public virtual void Clear()
        {
            var cacheEnum = _cache.GetEnumerator();
            while (cacheEnum.MoveNext())
                if (cacheEnum.Key != null && cacheEnum.Key.ToString().StartsWith(_cacheKeyPrefix))
                    _cache.Remove(cacheEnum.Key.ToString());
        }

        public virtual Task ClearAsync()
        {
            Clear();
            return Task.FromResult(0);
        }

        protected virtual string ParseKey(string key)
        {
            return _cacheKeyPrefix + key;
        }
    }
}