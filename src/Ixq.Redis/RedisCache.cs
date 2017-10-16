using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        private readonly IDatabase _database;
        private readonly string _region;
        private readonly string _cacheKeyPrefix;

        /// <summary>
        ///     初始化一个<see cref="RedisCache" />实例。
        /// </summary>
        /// <param name="database"></param>
        /// <param name="region">缓存区域名称</param>
        public RedisCache(IDatabase database, string region)
        {
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }
            _database = database;
            _region = region;
            _cacheKeyPrefix = $"Ixq.Redis.RedisCache.{region}.";
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
                new KeyValuePair<string, object>(key.ToString().Replace(_cacheKeyPrefix, ""), Get(key)));
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
            object value;
            if (!TryDeserialize(byteValue, out value))
            {
                value = byteValue;
            }
            return value;
        }

        /// <summary>
        ///     异步从<see cref="IDatabase" />中获取缓存项。
        /// </summary>
        /// <param name="key">缓存项的唯一标识符。</param>
        /// <returns>缓存项。</returns>
        public virtual async Task<object> GetAsync(string key)
        {
            var byteValue = await _database.StringGetAsync(ParseKey(key));
            object value;
            if (!TryDeserialize(byteValue, out value))
            {
                value = byteValue;
            }
            return value;
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
            T value;
            TryDeserialize(byteValue, out value);
            if (value == null)
            {
                return default(T);
            }
            return value;
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
            T value;
            TryDeserialize(byteValue, out value);
            if (value == null)
            {
                return default(T);
            }
            return value;
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
            _database.StringSet(ParseKey(key), Serialize(value));
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
            return Do(db => db.StringSetAsync(ParseKey(key), Serialize(value)));
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
            {
                throw new Exception("无效的到期时间");
            }
            _database.StringSet(ParseKey(key), Serialize(value), new TimeSpan(0, 0, 0, second));
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
            {
                throw new Exception("无效的到期时间");
            }
            return Do(db => db.StringSetAsync(ParseKey(key), Serialize(value), new TimeSpan(0, 0, 0, second)));
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
            {
                throw new Exception("无效的到期时间");
            }
            _database.StringSet(ParseKey(key), Serialize(value), expiry);
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
            {
                throw new Exception("无效的到期时间");
            }
            return Do(db => db.StringSetAsync(ParseKey(key), Serialize(value), expiry));
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
            {
                throw new Exception("无效的到期时间");
            }
            _database.StringSet(ParseKey(key), Serialize(value), slidingExpiration);
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
            return Do(db => db.StringSetAsync(ParseKey(key), Serialize(value), slidingExpiration));
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
            {
                _database.KeyDelete(key);
            }
        }

        /// <summary>
        ///     异步清空<see cref="IDatabase" />所有的缓存项。
        /// </summary>
        /// <returns></returns>
        public virtual async Task ClearAsync()
        {
            foreach (var key in GetAllKeys())
            {
                await _database.KeyDeleteAsync(key);
            }
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
            {
                if (key.ToString().StartsWith(_cacheKeyPrefix))
                {
                    yield return key;
                }
            }
        }
        protected virtual string ParseKey(string key)
        {
            return _cacheKeyPrefix + key;
        }

        #region 私有方法


        private T Do<T>(Func<IDatabase, T> func)
        {
            return func(_database);
        }

        /// <summary>
        ///     序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static byte[] Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                var data = memoryStream.ToArray();
                return data;
            }
        }

        /// <summary>
        ///     反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="object"></param>
        /// <returns></returns>
        private static bool TryDeserialize<T>(byte[] data, out T @object)
        {
            @object = default(T);

            if (data == null)
            {
                return false;
            }

            try
            {
                var binaryFormatter = new BinaryFormatter();
                using (var memoryStream = new MemoryStream(data))
                {
                    var result = (T) binaryFormatter.Deserialize(memoryStream);
                    @object = result;
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}