using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Cache
{
    public sealed class CacheManager
    {
        //private readonly Lazy<CacheManager> _lazyCacheManager = new Lazy<CacheManager>(() => new CacheManager());
        //public CacheManager Instance => _lazyCacheManager.Value;
        private static ICacheProvider _cacheProvider;

        public static void SetCacheProvider(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public static ICache GetCache(string region)
        {
            if(_cacheProvider == null)
                throw new ArgumentNullException(nameof(_cacheProvider));
            if (string.IsNullOrEmpty(region))
                throw new ArgumentNullException(nameof(region));

            return _cacheProvider.GetCache(region);
        }

        public static ICache GetCache<T>()
        {
            if (_cacheProvider == null)
                throw new ArgumentNullException(nameof(_cacheProvider));
            return GetCache(typeof (T).FullName);
        }

        public static ICache GetGlobalCache()
        {
            if (_cacheProvider == null)
                throw new ArgumentNullException(nameof(_cacheProvider));
            return _cacheProvider.GetGlobalCache();
        }
    }
}
