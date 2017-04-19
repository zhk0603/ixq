using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Cache
{
    public abstract class CacheProviderBase : ICacheProvider
    {
        private static readonly string GlobalCacheKey = "__globalCacheKey:" + typeof (CacheProviderBase).FullName;
        private static readonly ConcurrentDictionary<string, ICache> Caches;

        static CacheProviderBase()
        {
            Caches = new ConcurrentDictionary<string, ICache>();
        }

        public virtual ICache GetGlobalCache()
        {
            ICache cache;
            if (Caches.TryGetValue(GlobalCacheKey, out cache))
            {
                return cache;
            }
            cache = GetCache(GlobalCacheKey);
            Caches[GlobalCacheKey] = cache;
            return cache;
        }

        public abstract ICache GetCache(string regionName);
    }
}
