using System;

namespace Ixq.Core.Cache
{
    /// <summary>
    ///     缓存管理器。
    /// </summary>
    public sealed class CacheManager
    {
        private static ICacheProvider _cacheProvider;

        /// <summary>
        /// 是否启用缓存。
        /// </summary>
        public static bool IsEnable => _cacheProvider != null;

        /// <summary>
        ///     设置 <see cref="ICacheProvider" />
        /// </summary>
        /// <param name="cacheProvider">缓存提供者。</param>
        public static void SetCacheProvider(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        /// <summary>
        ///     设置 <see cref="ICacheProvider" />
        /// </summary>
        /// <param name="func"></param>
        public static void SetCacheProvider(Func<ICacheProvider> func)
        {
            _cacheProvider = func();
        }

        /// <summary>
        ///     获取<see cref="ICacheProvider" />
        /// </summary>
        /// <returns></returns>
        public static ICacheProvider GetCacheProvider()
        {
            return _cacheProvider;
        }

        /// <summary>
        ///     获取 <see cref="ICache" />
        /// </summary>
        /// <param name="region">缓存区域。</param>
        /// <returns></returns>
        public static ICache GetCache(string region)
        {
            if (_cacheProvider == null)
                throw new ArgumentNullException(nameof(_cacheProvider));
            if (string.IsNullOrEmpty(region))
                throw new ArgumentNullException(nameof(region));

            return _cacheProvider.GetCache(region);
        }

        /// <summary>
        ///     获取 <see cref="ICache" />
        /// </summary>
        /// <typeparam name="T">缓存区域。</typeparam>
        /// <returns></returns>
        public static ICache GetCache<T>()
        {
            if (_cacheProvider == null)
                throw new ArgumentNullException(nameof(_cacheProvider));
            return GetCache(typeof (T).FullName);
        }

        /// <summary>
        ///     获取全局缓存实例。
        /// </summary>
        /// <returns></returns>
        public static ICache GetGlobalCache()
        {
            if (_cacheProvider == null)
                throw new ArgumentNullException(nameof(_cacheProvider));
            return _cacheProvider.GetGlobalCache();
        }
    }
}