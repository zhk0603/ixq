using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Cache
{
    /// <summary>
    /// <see cref="MemoryCache"/>提供者。
    /// </summary>
    public class MemoryCacheProvider : CacheProviderBase
    {
        /// <summary>
        /// 获取 <see cref="ICache"/>
        /// </summary>
        /// <param name="regionName">缓存区域。</param>
        /// <returns></returns>
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
