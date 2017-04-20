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


        public IEnumerable<KeyValuePair<string, object>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<KeyValuePair<string, object>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            var byteValue = _database.StringGet(key);
            var value = Deserialize<object>(byteValue);
            return value;
        }

        public async Task<object> GetAsync(string key)
        {
            var byteValue = await _database.StringGetAsync(key);
            var value = Deserialize<object>(byteValue);
            return value;
        }

        public T Get<T>(string key)
        {
            var byteValue = _database.StringGet(key);
            var value = Deserialize<T>(byteValue);
            if (value == null) return default(T);
            return value;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var byteValue = await _database.StringGetAsync(key);
            var value = Deserialize<T>(byteValue);
            if (value == null) return default(T);
            return value;
        }

        public void Set<T>(string key, T value)
        {
            _database.StringSet(key, Serialize(value));
        }

        public Task SetAsync<T>(string key, T value)
        {
            return Do(db => db.StringSetAsync(key, Serialize(value)));
        }

        public void Set<T>(string key, T value, int second)
        {
            if (second <= 0)
            {
                throw new Exception("无效的到期时间");
            }
            _database.StringSet(key, Serialize(value), new TimeSpan(0, 0, 0, second));
        }

        public Task SetAsync<T>(string key, T value, int second)
        {
            if (second <= 0)
            {
                throw new Exception("无效的到期时间");
            }
            return Do(db => db.StringSetAsync(key, Serialize(value), new TimeSpan(0, 0, 0, second)));
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            TimeSpan expiry = absoluteExpiration - DateTime.Now;
            if (expiry.TotalSeconds <= 0)
            {
                throw new Exception("无效的到期时间");
            }
            _database.StringSet(key, Serialize(value), expiry);
        }

        public Task SetAsync<T>(string key, T value, DateTime absoluteExpiration)
        {
            TimeSpan expiry = absoluteExpiration - DateTime.Now;
            if (expiry.TotalSeconds <= 0)
            {
                throw new Exception("无效的到期时间");
            }
            return Do(db => db.StringSetAsync(key, Serialize(value), expiry));
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            if (slidingExpiration.TotalSeconds <= 0)
            {
                throw new Exception("无效的到期时间");
            }
            _database.StringSet(key, Serialize(value), slidingExpiration);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration)
        {
            return Do(db => db.StringSetAsync(key, Serialize(value), slidingExpiration));
        }

        public void Remove(string key)
        {
            _database.KeyDelete(key);
        }

        public Task RemoveAsync(string key)
        {
            return Do(db => db.KeyDeleteAsync(key));
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        #region 私有方法
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
        /// <returns></returns>
        private static T Deserialize<T>(byte[] data)
        {
            if (data == null)
                return default(T);

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream(data))
            {
                var result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }

        #endregion
    }
}
