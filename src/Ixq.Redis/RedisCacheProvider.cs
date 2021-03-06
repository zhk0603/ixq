﻿using System;
using Ixq.Core.Cache;
using StackExchange.Redis;

namespace Ixq.Redis
{
    /// <summary>
    ///     <see cref="RedisCache" />提供者。
    /// </summary>
    public class RedisCacheProvider : CacheProviderBase
    {
        private static readonly Lazy<ConnectionMultiplexer> ConnectionMultiplexer;
        private static string _connStr;
        private static ConfigurationOptions _options;


        static RedisCacheProvider()
        {
            ConnectionMultiplexer = new Lazy<ConnectionMultiplexer>(GetConnectionMultiplexer);
        }

        /// <summary>
        ///     初始化一个<see cref="RedisCacheProvider" />实例。
        /// </summary>
        /// <param name="connectionString">Redis 连接串。</param>
        /// <param name="serializableService">序列化服务。</param>
        public RedisCacheProvider(string connectionString, ISerializableService serializableService)
        {
            _connStr = connectionString;
            SerializableService = serializableService;
        }

        public RedisCacheProvider() : this("localhost:6379", new BinarySerializableService())
        {
        }

        /// <summary>
        ///     初始化一个<see cref="RedisCacheProvider" />实例。
        /// </summary>
        /// <param name="options">配置选项。</param>
        /// <param name="serializableService">序列化服务。</param>
        public RedisCacheProvider(ConfigurationOptions options, ISerializableService serializableService)
        {
            _options = options;
            SerializableService = serializableService;
        }

        public static ConnectionMultiplexer ConnectionMultiplexerInstance => ConnectionMultiplexer.Value;

        /// <summary>
        ///     获取序列化服务。
        /// </summary>
        public virtual ISerializableService SerializableService { get; }

        /// <summary>
        ///     获取 <see cref="ConnectionMultiplexer" /> 实例。
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
        ///     获取 <see cref="ICache" />
        /// </summary>
        /// <param name="regionName">缓存区域。</param>
        /// <returns></returns>
        public override ICache GetCache(string regionName)
        {
            ICache cache;
            if (Caches.TryGetValue(regionName, out cache))
                return cache;
            cache = new RedisCache(ConnectionMultiplexerInstance.GetDatabase(), regionName, SerializableService);
            Caches[regionName] = cache;
            return cache;
        }
    }
}