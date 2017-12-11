using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ixq.Core.Cache;
using StackExchange.Redis;

namespace Ixq.Redis
{
    /// <summary>
    ///     Redis缓存。
    /// </summary>
    public class RedisCache : ICache
    {
        private readonly string _cacheKeyPrefix;
        private readonly IDatabase _database;
        private readonly string _region;
        private readonly ISerializableService _serializableService;

        /// <summary>
        ///     初始化一个<see cref="RedisCache" />实例。
        /// </summary>
        /// <param name="database"></param>
        /// <param name="region">缓存区域名称</param>
        /// <param name="serializableService">序列化服务。</param>
        public RedisCache(IDatabase database, string region, ISerializableService serializableService)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _region = region;
            _cacheKeyPrefix = $"Ixq.Redis.RedisCache.{region}.";
            _serializableService = serializableService ?? throw new ArgumentNullException(nameof(serializableService));
        }

        /// <summary>
        ///     获取缓存区域名称。
        /// </summary>
        /// <returns></returns>
        public virtual string GetRegionName()
        {
            return _region;
        }

        /// <summary>
        ///     从<see cref="IDatabase" />中获取全部的缓存项。
        /// </summary>
        /// <returns>全部的缓存项。</returns>
        public virtual IEnumerable<KeyValuePair<string, object>> GetAll()
        {
            return GetAllKeys().Select(key =>
            {
                var originalName = key.ToString().Replace(_cacheKeyPrefix, "");
                return new KeyValuePair<string, object>(originalName, Get(originalName));
            });
        }

        /// <summary>
        ///     异步从<see cref="IDatabase" />中获取全部的缓存项。
        /// </summary>
        /// <returns>全部的缓存项。</returns>
        public virtual Task<IEnumerable<KeyValuePair<string, object>>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        /// <summary>
        ///     从<see cref="IDatabase" />中获取缓存项。
        /// </summary>
        /// <param name="key">缓存项的唯一标识符。</param>
        /// <returns>缓存项。</returns>
        public virtual object Get(string key)
        {
            var byteValue = _database.StringGet(ParseKey(key));
            if (TryDeserialize(byteValue, out var value))
                return value.Value;
            return null;
        }

        /// <summary>
        ///     异步从<see cref="IDatabase" />中获取缓存项。
        /// </summary>
        /// <param name="key">缓存项的唯一标识符。</param>
        /// <returns>缓存项。</returns>
        public virtual async Task<object> GetAsync(string key)
        {
            var byteValue = await _database.StringGetAsync(ParseKey(key));
            if (TryDeserialize(byteValue, out var value))
                return value.Value;
            return null;
        }

        /// <summary>
        ///     从<see cref="IDatabase" />中获取缓存项。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">缓存项的唯一标识符。</param>
        /// <returns>缓存项。</returns>
        public virtual T Get<T>(string key)
        {
            var byteValue = _database.StringGet(ParseKey(key));
            TryDeserialize(byteValue, out var value);
            if (value == null)
                return default(T);
            return (T) value.Value;
        }

        /// <summary>
        ///     异步从<see cref="IDatabase" />中获取缓存项。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">缓存项的唯一标识符。</param>
        /// <returns>缓存项。</returns>
        public virtual async Task<T> GetAsync<T>(string key)
        {
            var byteValue = await _database.StringGetAsync(ParseKey(key));
            TryDeserialize(byteValue, out var value);
            if (value == null)
                return default(T);
            return (T) value.Value;
        }

        /// <summary>
        ///     将某个缓存项插入<see cref="IDatabase" />中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="value">该缓存项的数据。</param>
        /// <returns></returns>
        public virtual void Set<T>(string key, T value)
        {
            _database.StringSet(ParseKey(key), ParseValue(key, value));
        }

        /// <summary>
        ///     异步将某个缓存项插入<see cref="IDatabase" />中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="value">该缓存项的数据。</param>
        /// <returns></returns>
        public virtual Task SetAsync<T>(string key, T value)
        {
            return Do(db => db.StringSetAsync(ParseKey(key), ParseValue(key, value)));
        }

        /// <summary>
        ///     将某个缓存项插入<see cref="IDatabase" />中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="value">该缓存项的数据。</param>
        /// <param name="second">有效时长，单位：秒。</param>
        /// <returns></returns>
        public virtual void Set<T>(string key, T value, int second)
        {
            if (second <= 0)
                throw new Exception("无效的到期时间");
            _database.StringSet(ParseKey(key), ParseValue(key, value), new TimeSpan(0, 0, 0, second));
        }

        /// <summary>
        ///     异步将某个缓存项插入<see cref="IDatabase" />中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="value">该缓存项的数据。</param>
        /// <param name="second">有效时长，单位：秒。</param>
        /// <returns></returns>
        public virtual Task SetAsync<T>(string key, T value, int second)
        {
            if (second <= 0)
                throw new Exception("无效的到期时间");
            return Do(db => db.StringSetAsync(ParseKey(key), ParseValue(key, value), new TimeSpan(0, 0, 0, second)));
        }

        /// <summary>
        ///     将某个缓存项插入<see cref="IDatabase" />中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="value">该缓存项的数据。</param>
        /// <param name="absoluteExpiration">缓存项过期时间。</param>
        /// <returns></returns>
        public virtual void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            var expiry = absoluteExpiration - DateTime.Now;
            if (expiry.TotalSeconds <= 0)
                throw new Exception("无效的到期时间");
            _database.StringSet(ParseKey(key), ParseValue(key, value), expiry);
        }

        /// <summary>
        ///     异步将某个缓存项插入<see cref="IDatabase" />中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="value">该缓存项的数据。</param>
        /// <param name="absoluteExpiration">缓存项过期时间。</param>
        /// <returns></returns>
        public virtual Task SetAsync<T>(string key, T value, DateTime absoluteExpiration)
        {
            var expiry = absoluteExpiration - DateTime.Now;
            if (expiry.TotalSeconds <= 0)
                throw new Exception("无效的到期时间");
            return Do(db => db.StringSetAsync(ParseKey(key), ParseValue(key, value), expiry));
        }

        /// <summary>
        ///     将某个缓存项插入<see cref="IDatabase" />中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="value">该缓存项的数据。</param>
        /// <param name="slidingExpiration">在此时间内访问缓存，缓存将继续有效（Redis Cache 暂未实现。）</param>
        /// <returns></returns>
        public virtual void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            if (slidingExpiration.TotalSeconds <= 0)
                throw new Exception("无效的到期时间");
            _database.StringSet(ParseKey(key), ParseValue(key, value, slidingExpiration), slidingExpiration);
        }

        /// <summary>
        ///     异步将某个缓存项插入<see cref="IDatabase" />中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="value">该缓存项的数据。</param>
        /// <param name="slidingExpiration">在此时间内访问缓存，缓存将继续有效（Redis Cache 暂未实现。）</param>
        /// <returns></returns>
        public virtual Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration)
        {
            return Do(db =>
            {
                if (slidingExpiration.TotalSeconds <= 0)
                    throw new Exception("无效的到期时间");
                return db.StringSetAsync(ParseKey(key), ParseValue(key, value, slidingExpiration), slidingExpiration);
            });
        }

        /// <summary>
        ///     从<see cref="IDatabase" />移除某个缓存项。
        /// </summary>
        /// <param name="key">要移除的缓存项的唯一标识符。</param>
        /// <returns></returns>
        public virtual void Remove(string key)
        {
            _database.KeyDelete(ParseKey(key));
        }

        /// <summary>
        ///     异步从<see cref="IDatabase" />移除某个缓存项。
        /// </summary>
        /// <param name="key">要移除的缓存项的唯一标识符。</param>
        /// <returns></returns>
        public virtual Task RemoveAsync(string key)
        {
            return Do(db => db.KeyDeleteAsync(ParseKey(key)));
        }

        /// <summary>
        ///     清空<see cref="IDatabase" />所有的缓存项。
        /// </summary>
        public virtual void Clear()
        {
            foreach (var key in GetAllKeys())
                _database.KeyDelete(key);
        }

        /// <summary>
        ///     异步清空<see cref="IDatabase" />所有的缓存项。
        /// </summary>
        /// <returns></returns>
        public virtual async Task ClearAsync()
        {
            foreach (var key in GetAllKeys())
                await _database.KeyDeleteAsync(key);
        }

        /// <summary>
        ///     获取 <see cref="IDatabase" /> 所有的Key。
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<RedisKey> GetAllKeys()
        {
            var option = ConfigurationOptions.Parse(RedisCacheProvider.ConnectionMultiplexerInstance.Configuration);
            var server = RedisCacheProvider.ConnectionMultiplexerInstance.GetServer(option.EndPoints.First());
            foreach (var key in server.Keys(_database.Database))
                if (key.ToString().StartsWith(_cacheKeyPrefix))
                    yield return key;
        }

        protected virtual string ParseKey(string key)
        {
            return _cacheKeyPrefix + key;
        }

        protected virtual byte[] ParseValue(string key, object value)
        {
            return ParseValue(key, value, null, null);
        }

        protected virtual byte[] ParseValue(string key, object value, DateTime? absoluteExpiration)
        {
            return ParseValue(key, value, absoluteExpiration, null);
        }

        protected virtual byte[] ParseValue(string key, object value, TimeSpan? slidingExpiration)
        {
            return ParseValue(key, value, null, slidingExpiration);
        }

        /// <summary>
        ///     使用 <see cref="RedisCacheValue" /> 包装 缓存值，并返回序列化后的结果。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        /// <returns></returns>
        protected virtual byte[] ParseValue(string key, object value, DateTime? absoluteExpiration,
            TimeSpan? slidingExpiration)
        {
            var cacheValue = new RedisCacheValue
            {
                Key = key,
                AbsoluteExpiration = absoluteExpiration,
                SlidingExpiration = slidingExpiration,
                Value = value
            };
            return Serialize(cacheValue);
        }

        /// <summary>
        ///     检查缓存值的过期时间，并做相应处理。
        ///     实际上我们只需要处理 滑动过期，我们通过比较滑动过期时间与插入时间，过期了则移除缓存项，在滑动时间范围内则重新插入缓存。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual bool CheckCacheValue(RedisCacheValue value)
        {
            if (value.SlidingExpiration.HasValue)
            {
                // 通常情况下在 滑动时间到期时 RedisCache 会自动移除缓存项。
                if (value.SlidingExpiration.Value < DateTime.Now - value.InsertTime)
                {
                    Remove(value.Key);
                    return false;
                }
                Set(value.Key, value.Value, value.SlidingExpiration.Value);
            }
            return true;
        }

        /// <summary>
        ///     序列化。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual byte[] Serialize(object obj)
        {
            return _serializableService.Serialize(obj);
        }

        /// <summary>
        ///     反序列化。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="object"></param>
        /// <returns></returns>
        protected virtual bool TryDeserialize(byte[] data, out RedisCacheValue @object)
        {
            @object = default(RedisCacheValue);
            if (data == null)
                return false;
            try
            {
                var result = _serializableService.Deserialize(data);
                @object = (RedisCacheValue) result;
                if (!CheckCacheValue(@object))
                {
                    @object = null;
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region 私有方法

        private T Do<T>(Func<IDatabase, T> func)
        {
            return func(_database);
        }

        #endregion
    }
}