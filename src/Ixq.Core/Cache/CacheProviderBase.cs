using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Ixq.Core.Cache
{
    /// <summary>
    ///     缓存提供者基类。
    /// </summary>
    public abstract class CacheProviderBase : ICacheProvider
    {
        private static readonly string GlobalCacheKey = "__globalCacheKey"; // + typeof (CacheProviderBase).FullName;

        /// <summary>
        ///     <see cref="ICache" /> 实例字典集合。
        /// </summary>
        protected static readonly ConcurrentDictionary<string, ICache> Caches;

        static CacheProviderBase()
        {
            Caches = new ConcurrentDictionary<string, ICache>();
        }

        /// <summary>
        ///     获取全局缓存实例。
        /// </summary>
        /// <returns></returns>
        ICache ICacheProvider.GetGlobalCache()
        {
            return GetCache(GlobalCacheKey);
        }

        /// <summary>
        ///     获取 <see cref="ICache" />
        /// </summary>
        /// <param name="regionName">缓存区域。</param>
        /// <returns></returns>
        ICache ICacheProvider.GetCache(string regionName)
        {
            return GetCache(regionName);
        }

        /// <summary>
        ///     获取全部的<see cref="ICache" />
        /// </summary>
        /// <returns></returns>
        public virtual IDictionary<string, ICache> GetAllRegionCaches()
        {
            return Caches;
        }

        /// <summary>
        ///     移除指定的<see cref="ICache" />
        /// </summary>
        /// <param name="regionName">缓存区域。</param>
        void ICacheProvider.RemoveCache(string regionName)
        {
            ICache cache;
            if (!Caches.TryRemove(regionName, out cache))
            {
                return;
            }
            cache = null;
        }

        /// <summary>
        ///     移除全部<see cref="ICache" />
        /// </summary>
        void ICacheProvider.RemoveAllRegionCahces()
        {
            Caches.Clear();
        }

        /// <summary>
        ///     获取 <see cref="ICache" />
        /// </summary>
        /// <param name="regionName">缓存区域。</param>
        /// <returns></returns>
        public abstract ICache GetCache(string regionName);
    }
}