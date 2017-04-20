﻿using Ixq.Core.Cache;
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

            TestMethod1();
            TestMethod2();

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

        class TestMemoryCache : CacheProviderBase
        {
            public override ICache GetCache(string regionName)
            {
                throw new NotImplementedException();
            }
        }
    }
}
