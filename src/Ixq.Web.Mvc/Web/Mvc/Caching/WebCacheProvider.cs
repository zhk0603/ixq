using Ixq.Core.Cache;

namespace Ixq.Web.Mvc.Caching
{
    public class WebCacheProvider : CacheProviderBase
    {
        public override ICache GetCache(string regionName)
        {
            if (Caches.TryGetValue(regionName, out var cache))
                return cache;
            cache = new WebCache(regionName);
            Caches[regionName] = cache;
            return cache;
        }
    }
}