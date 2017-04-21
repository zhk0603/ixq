using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Cache;
using StackExchange.Redis;

namespace Ixq.Redis
{
    public class RedisCache : ICache
    {
        private readonly IDatabase _database;
        public RedisCache(IDatabase database)
        {
            if (database == null)
                throw new ArgumentNullException(nameof(database));
            _database = database;
        }

        public virtual IEnumerable<KeyValuePair<string, object>> GetAll()
        {
            return GetAllKeys().Select(key => new KeyValuePair<string, object>(key, Get(key)));//.ToList();
        }

        public virtual Task<IEnumerable<KeyValuePair<string, object>>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        public virtual object Get(string key)
        {
            var byteValue = _database.StringGet(key);
            object value;
            if (!TryDeserialize(byteValue, out value)) value = byteValue;
            return value;
        }

        public virtual async Task<object> GetAsync(string key)
        {
            var byteValue = await _database.StringGetAsync(key);
            object value;
            if (!TryDeserialize(byteValue, out value)) value = byteValue;
            return value;
        }

        public virtual T Get<T>(string key)
        {
            var byteValue = _database.StringGet(key);
            T value;
            TryDeserialize(byteValue, out value);
            if (value == null) return default(T);
            return value;
        }

        public virtual async Task<T> GetAsync<T>(string key)
        {
            var byteValue = await _database.StringGetAsync(key);
            T value;
            TryDeserialize(byteValue, out value);
            if (value == null) return default(T);
            return value;
        }

        public virtual void Set<T>(string key, T value)
        {
            _database.StringSet(key, Serialize(value));
        }

        public virtual Task SetAsync<T>(string key, T value)
        {
            return Do(db => db.StringSetAsync(key, Serialize(value)));
        }

        public virtual void Set<T>(string key, T value, int second)
        {
            if (second <= 0)
            {
                throw new Exception("无效的到期时间");
            }
            _database.StringSet(key, Serialize(value), new TimeSpan(0, 0, 0, second));
        }

        public virtual Task SetAsync<T>(string key, T value, int second)
        {
            if (second <= 0)
            {
                throw new Exception("无效的到期时间");
            }
            return Do(db => db.StringSetAsync(key, Serialize(value), new TimeSpan(0, 0, 0, second)));
        }

        public virtual void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            TimeSpan expiry = absoluteExpiration - DateTime.Now;
            if (expiry.TotalSeconds <= 0)
            {
                throw new Exception("无效的到期时间");
            }
            _database.StringSet(key, Serialize(value), expiry);
        }

        public virtual Task SetAsync<T>(string key, T value, DateTime absoluteExpiration)
        {
            TimeSpan expiry = absoluteExpiration - DateTime.Now;
            if (expiry.TotalSeconds <= 0)
            {
                throw new Exception("无效的到期时间");
            }
            return Do(db => db.StringSetAsync(key, Serialize(value), expiry));
        }

        public virtual void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            if (slidingExpiration.TotalSeconds <= 0)
            {
                throw new Exception("无效的到期时间");
            }
            _database.StringSet(key, Serialize(value), slidingExpiration);
        }

        public virtual Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration)
        {
            return Do(db => db.StringSetAsync(key, Serialize(value), slidingExpiration));
        }

        public virtual void Remove(string key)
        {
            _database.KeyDelete(key);
        }

        public virtual Task RemoveAsync(string key)
        {
            return Do(db => db.KeyDeleteAsync(key));
        }

        public virtual void Clear()
        {
            foreach (var key in GetAllKeys())
            {
                _database.KeyDelete(key);
            }
        }

        public virtual async Task ClearAsync()
        {
            foreach (var key in GetAllKeys())
            {
                await _database.KeyDeleteAsync(key);
            }
        }

        #region 私有方法

        private IEnumerable<RedisKey> GetAllKeys()
        {
            var option = ConfigurationOptions.Parse(RedisCacheProvider.ConnectionMultiplexerInstance.Configuration);
            var server = RedisCacheProvider.ConnectionMultiplexerInstance.GetServer(option.EndPoints.First());
            var keys = server.Keys(_database.Database).ToList();
            return keys;
        }

        private T Do<T>(Func<IDatabase, T> func)
        {
            return func(_database);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                var data = memoryStream.ToArray();
                return data;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="object"></param>
        /// <returns></returns>
        private static bool TryDeserialize<T>(byte[] data, out T @object)
        {
            @object = default(T);

            if (data == null)
                return false;

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
