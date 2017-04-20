using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Cache
{
    public class MemoryCacheProvider : CacheProviderBase
    {
        public override ICache GetCache(string regionName)
        {
            ICache cache;
            if (Caches.TryGetValue(regionName, out cache))
            {
                return cache;
            }
            cache = new MemoryCache(regionName);
            Caches[regionName] = cache;
            return cache;
        }
    }
}
