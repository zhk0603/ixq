namespace Ixq.Core.Cache
{
    /// <summary>
    ///     <see cref="MemoryCache" />提供者。
    /// </summary>
    public class MemoryCacheProvider : CacheProviderBase
    {
        /// <summary>
        ///     获取 <see cref="ICache" />
        /// </summary>
        /// <param name="regionName">缓存区域。</param>
        /// <returns></returns>
        public override ICache GetCache(string regionName)
        {
            if (Caches.TryGetValue(regionName, out var cache))
            {
                return cache;
            }
            cache = new MemoryCache(regionName);
            Caches[regionName] = cache;
            return cache;
        }
    }
}