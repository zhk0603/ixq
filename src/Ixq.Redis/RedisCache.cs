using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Cache;
using StackExchange.Redis;

namespace Ixq.Redis
{
    public class RedisCache : ICache
    {
        private IDatabase _Database;
        public RedisCache(IDatabase database)
        {
            if (database == null)
                throw new ArgumentNullException(nameof(database));
            _Database = database;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Task<object> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value, int second)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync<T>(string key, T value, int second)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync<T>(string key, T value, DateTime absoluteExpiration)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }
    }
}
