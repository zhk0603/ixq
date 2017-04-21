using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Cache;
using StackExchange.Redis;

namespace Ixq.Redis
{
    public class RedisCacheProvider : CacheProviderBase
    {
        private static readonly object Lock = new object();
        private static readonly Lazy<ConnectionMultiplexer> ConnectionMultiplexer;
        private static string _connStr;
        private static ConfigurationOptions _options;
        private static readonly ConcurrentDictionary<string, int> DbNumDic;
        public static ConnectionMultiplexer ConnectionMultiplexerInstance => ConnectionMultiplexer.Value;

        public RedisCacheProvider(string connectionString)
        {
            _connStr = connectionString;
        }
        public RedisCacheProvider(ConfigurationOptions options)
        {
            _options = options;
        }
        static RedisCacheProvider()
        {
            ConnectionMultiplexer = new Lazy<ConnectionMultiplexer>(GetConnectionMultiplexer);
            DbNumDic = new ConcurrentDictionary<string, int>();
        }

        private static ConnectionMultiplexer GetConnectionMultiplexer()
        {
            var connStr = _options?.ToString() ?? _connStr;
            if (string.IsNullOrWhiteSpace(connStr))
                throw new ArgumentNullException(nameof(connStr));
            return StackExchange.Redis.ConnectionMultiplexer.Connect(connStr);
        }
        public override ICache GetCache(string regionName)
        {
            ICache cache;
            if (Caches.TryGetValue(regionName, out cache))
            {
                return cache;
            }
            var dbNum = GetDbNum(regionName);
            cache = new RedisCache(ConnectionMultiplexerInstance.GetDatabase(dbNum));
            Caches[regionName] = cache;
            return cache;
        }

        #region private method

        private int GetDbNum(string regionName)
        {
            int dbNum = 0;
            lock (Lock)
            {
                if (DbNumDic.TryGetValue(regionName, out dbNum))
                {
                    return dbNum;
                }
                if (!DbNumDic.Any())
                {
                    dbNum = 0;
                }
                else
                {
                    dbNum = DbNumDic.Max(x => x.Value) + 1;
                }
                DbNumDic[regionName] = dbNum;
            }
            return dbNum;
        }

        #endregion
    }
}
