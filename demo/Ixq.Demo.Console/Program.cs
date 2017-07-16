using Ixq.Core.Cache;
using Ixq.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ixq.Core.Logging;
using StackExchange.Redis;
using Ixq.Logging.Log4Net;

namespace Ixq.Demo.Console
{
    [Serializable]
    class Program
    {
        static void Main(string[] args)
        {


            var tc = new CustomTest();
            ITest tc1 = tc;



            ITestBase<CustomTest> tb = new TestBase<CustomTest>();
            var tb1 = (ITestBase<Test>)tb;



            var s1 = "Admin";
            var s2 = "Employee";
            var c1 = s1.GetHashCode();
            var c2 = s2.GetHashCode();

            var config = new ConfigurationOptions();
            config.Password = "zhaokun123";

            //ICacheProvider cacheProvider = new RedisCacheProvider("localhost:6379,password=zhaokun");
            ICacheProvider cacheProvider = new MemoryCacheProvider();
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
            TestMethod2();
            TestMethod3();
            TestMethod4();
            //TestMethod5();

            System.Console.ReadKey();
        }

        public interface ITest
        {

        }

        public abstract class Test : ITest
        {

        }

        public class CustomTest : Test
        {

        }

        public interface ITestBase<in T>
            where T : class
        {

        }

        public class TestBase<T> : ITestBase<T>
            where T : class
        {
        }


        static void Main1(string[] args)
        {
            TestLog();

            BulkInsert();
            System.Console.ReadKey();
        }

        static void TestLog()
        {
            ILoggerFactory factory = new Log4NetLoggerFactory();
            LogManager.SetLoggerFactory(factory);

            var log = LogManager.GetLogger<Program>();
            log.Debug("debug log.");
            log.Info("info log.");
            log.Error("Error log.");
            log.Fatal("Fatal log.");
            log.Warn("Warn log.");

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
            foreach (var cache in CacheManager.GetCacheProvider().GetAllRegionCaches())
            {
                foreach (var item in await cache.Value.GetAllAsync())
                {
                    System.Console.WriteLine($"region:{cache.Key}\tkey:{item.Key}\tvalue:{item.Value}");

                }
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

        static void TestMethod5()
        {
            for (var i = 0; i < 20; i++)
            {
                var cache = CacheManager.GetCache("region_" + i);
                cache.Set("cache" + i + "_Test", "value");
            }
            for (var i = 0; i < 20; i++)
            {
                var cache = CacheManager.GetCache("region_" + i);
                System.Console.WriteLine(cache.Get("cache" + i + "_Test"));
            }
        }

        class TestMemoryCache : CacheProviderBase
        {
            public override ICache GetCache(string regionName)
            {
                throw new NotImplementedException();
            }
        }

        //使用Bulk插入的情况 [ 较快 ]
        #region [ 使用Bulk插入的情况 ]
        static void BulkToDB(DataTable dt)
        {
            Stopwatch sw = new Stopwatch();
            SqlConnection sqlconn = new SqlConnection("server=.;database=IxqDb;user=sa;password=123@Abc;");
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlconn);
            bulkCopy.DestinationTableName = "Test";
            bulkCopy.BatchSize = dt.Rows.Count;
            try
            {
                sqlconn.Open();
                if (dt != null && dt.Rows.Count != 0)
                {
                    bulkCopy.WriteToServer(dt);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlconn.Close();
                if (bulkCopy != null)
                {
                    bulkCopy.Close();
                }
            }
        }
        static DataTable GetTableSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("Id",typeof(int)),
                new DataColumn("Name",typeof(string)),
                new DataColumn("Name1",typeof(string)),
                new DataColumn("Name2",typeof(string)),
                new DataColumn("Name3",typeof(string)),
                new DataColumn("Name4",typeof(string)),
                new DataColumn("SortCode",typeof(string)),
                new DataColumn("CreateDate",typeof(DateTime)),
                new DataColumn("UpdataDate",typeof(DateTime)),
                new DataColumn("DeleteDate",typeof(DateTime)),
                new DataColumn("IsDeleted",typeof(int)),
            });
            return dt;
        }

        static void BulkInsert()
        {
            System.Console.WriteLine("使用简单的Bulk插入的情况");
            Stopwatch sw = new Stopwatch();
            var i = 0;
            for (int multiply = 0; multiply < 10*5000; multiply++) // 10000000
            {
                DataTable dt = GetTableSchema();
                for (int count = multiply*100; count < (multiply + 1)*100; count++)
                {
                    DataRow r = dt.NewRow();
                    //r[0] = Guid.NewGuid();
                    r[1] = $"TestName{i}";
                    r[2] = $"TestName1{i}";
                    r[3] = $"TestName2{i}";
                    r[4] = $"TestName3{i}";
                    r[5] = $"TestName4{i}";
                    r[7] = DateTime.Now;
                    r[10] = 0;
                    dt.Rows.Add(r);
                    i++;
                }
                sw.Start();
                BulkToDB(dt);
                sw.Stop();
                System.Console.WriteLine(string.Format("Elapsed Time is {0} Milliseconds", sw.ElapsedMilliseconds));
            }
        }

        #endregion
    }
}
