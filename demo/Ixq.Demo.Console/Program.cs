using Ixq.Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Demo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryCacheProvider cacheProvider = new MemoryCacheProvider();
            CacheManager.SetCacheProvider(cacheProvider);


            CacheManager.GetGlobalCache().Set("test", "test");
            CacheManager.GetCache<Program>().Set("test", "test");
            CacheManager.GetCache(nameof(Program)).Set("test", "test1");

            System.Console.WriteLine(CacheManager.GetGlobalCache().Get<string>("test"));
            System.Console.WriteLine(CacheManager.GetCache(nameof(Program)).Get<string>("test"));

            System.Console.ReadKey();
        }

        class TestMemoryCache : CacheProviderBase
        {
            public override ICache GetCache(string regionName)
            {
                throw new NotImplementedException();
            }
        }
    }
}
