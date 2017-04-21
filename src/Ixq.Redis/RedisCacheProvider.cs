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
    /// <summary>
    /// <see cref="RedisCache"/>提供者。
    /// </summary>
    public class RedisCacheProvider : CacheProviderBase
    {
        private static readonly object Lock = new object();
        private static readonly Lazy<ConnectionMultiplexer> ConnectionMultiplexer;
        private static string _connStr;
        private static ConfigurationOptions _options;
        private static readonly ConcurrentDictionary<string, int> DbNumDic;
        public static ConnectionMultiplexer ConnectionMultiplexerInstance => ConnectionMultiplexer.Value;

        /// <summary>
        /// 初始化一个<see cref="RedisCacheProvider"/>实例。
        /// </summary>
        /// <param name="connectionString">Redis 连接串。</param>
        public RedisCacheProvider(string connectionString)
        {
            _connStr = connectionString;
        }
        /// <summary>
        /// 初始化一个<see cref="RedisCacheProvider"/>实例。
        /// </summary>
        /// <param name="options">配置选项。</param>
        public RedisCacheProvider(ConfigurationOptions options)
        {
            _options = options;
        }
        static RedisCacheProvider()
        {
            ConnectionMultiplexer = new Lazy<ConnectionMultiplexer>(GetConnectionMultiplexer);
            DbNumDic = new ConcurrentDictionary<string, int>();
        }
        /// <summary>
        /// 获取 <see cref="ConnectionMultiplexer"/> 实例。
        /// </summary>
        /// <returns></returns>
        private static ConnectionMultiplexer GetConnectionMultiplexer()
        {
            var connStr = _options?.ToString() ?? _connStr;
            if (string.IsNullOrWhiteSpace(connStr))
                throw new ArgumentNullException(nameof(connStr));
            return StackExchange.Redis.ConnectionMultiplexer.Connect(connStr);
        }
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
