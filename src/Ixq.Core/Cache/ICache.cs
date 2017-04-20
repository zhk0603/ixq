using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Cache
{
    /// <summary>
    /// 缓存接口。
    /// </summary>
    public interface ICache
    {
        IEnumerable<KeyValuePair<string, object>> GetAll();
        Task<IEnumerable<KeyValuePair<string, object>>> GetAllAsync();
        object Get(string key);
        Task<object> GetAsync(string key);
        T Get<T>(string key);
        Task<T> GetAsync<T>(string key);
        void Set<T>(string key, T value);
        Task SetAsync<T>(string key, T value);
        void Set<T>(string key, T value, int second);
        Task SetAsync<T>(string key, T value, int second);
        void Set<T>(string key, T value, DateTime absoluteExpiration);
        Task SetAsync<T>(string key, T value, DateTime absoluteExpiration);
        void Set<T>(string key, T value, TimeSpan slidingExpiration);
        Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration);
        void Remove(string key);
        Task RemoveAsync(string key);
        void Clear();
        Task ClearAsync();
    }
}
