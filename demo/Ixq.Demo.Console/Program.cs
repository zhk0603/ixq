using Ixq.Core.Cache;
using Ixq.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Ixq.Demo.Console
{
    [Serializable]
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationOptions();
            config.Password = "zhaokun123";

            ICacheProvider cacheProvider = new RedisCacheProvider("localhost:6379,password=zhaokun123");
            CacheManager.SetCacheProvider(cacheProvider);

            CacheManager.GetGlobalCache().Set("test", "test");
            CacheManager.GetCache<Program>().Set("test", "test");
            CacheManager.GetCache<Program>().Set("test1", "test");
            CacheManager.GetCache<Program>().Set("test2", "test");
            CacheManager.GetCache<Program>().Set("test3", "test");
            CacheManager.GetCache(nameof(Program)).Set("test", "test1");
            System.Console.WriteLine(CacheManager.GetGlobalCache().Get<string>("test"));
            System.Console.WriteLine(CacheManager.GetCache(nameof(Program)).Get<string>("test"));

            TestMethod1();
            //TestMethod2();
            TestMethod3();
            TestMethod4();

            System.Console.ReadKey();
        }

        static async void TestMethod1()
        {
            var globalCache = CacheManager.GetGlobalCache();
            var p = new Program();
            System.Console.WriteLine(p.GetHashCode());
            await globalCache.SetAsync("testProgram", p);
            await globalCache.SetAsync("testAsync", "testAsync");
            System.Console.WriteLine(await globalCache.GetAsync<string>("testAsync"));
            System.Console.WriteLine(await globalCache.GetAsync<string>("testAsyncNull"));
            System.Console.WriteLine(globalCache.Get<Program>("testProgram").GetHashCode());
        }

        static async void TestMethod2()
        {
            var globalCache = CacheManager.GetGlobalCache();
            foreach (var item in await globalCache.GetAllAsync())
            {
                System.Console.WriteLine($"key:{item.Key}\tvalue:{item.Value}");
            }
        }

        static void TestMethod3()
        {
            System.Console.WriteLine("TestMethod3 start");
            var globalCache = CacheManager.GetGlobalCache();
            globalCache.Set("absoluteExpirationTest", "zhaokun", DateTime.Now.AddSeconds(10));
            System.Console.WriteLine(globalCache.Get("absoluteExpirationTest"));
            Thread.Sleep(9*1000);
            System.Console.WriteLine(globalCache.Get("absoluteExpirationTest"));
            System.Console.WriteLine("TestMethod3 end");
        }

        static void TestMethod4()
        {
            var cacheProvider = CacheManager.GetCacheProvider();
            Thread.Sleep(2000);
            foreach (var item in cacheProvider.GetAllRegionCaches())
            {
                System.Console.WriteLine($"regionName:{item.Key}");
            }
            cacheProvider.GetCache(nameof(Program)).Set("disposeTest", "disposeTest");
            System.Console.WriteLine(cacheProvider.GetCache(nameof(Program)).Get("disposeTest"));
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
