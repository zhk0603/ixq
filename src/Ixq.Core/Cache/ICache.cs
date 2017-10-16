using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ixq.Core.Cache
{
    /// <summary>
    ///     缓存接口。
    /// </summary>
    public interface ICache
    {
        /// <summary>
        ///     获取缓存名称。
        /// </summary>
        /// <returns></returns>
        string GetRegionName();

        /// <summary>
        ///     获取全部的缓存项。
        /// </summary>
        /// <returns>全部的缓存项。</returns>
        IEnumerable<KeyValuePair<string, object>> GetAll();

        /// <summary>
        ///     异步获取全部的缓存项。
        /// </summary>
        /// <returns>全部的缓存项。</returns>
        Task<IEnumerable<KeyValuePair<string, object>>> GetAllAsync();

        /// <summary>
        ///     获取缓存项。
        /// </summary>
        /// <param name="key">缓存项的唯一标识符。</param>
        /// <returns>缓存项。</returns>
        object Get(string key);

        /// <summary>
        ///     异步获取缓存项。
        /// </summary>
        /// <param name="key">缓存项的唯一标识符。</param>
        /// <returns>缓存项。</returns>
        Task<object> GetAsync(string key);

        /// <summary>
        ///     获取缓存项。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">缓存项的唯一标识符。</param>
        /// <returns>缓存项。</returns>
        T Get<T>(string key);

        /// <summary>
        ///     异步获取缓存项。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">缓存项的唯一标识符。</param>
        /// <returns>缓存项。</returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        ///     将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="value">该缓存项的数据。</param>
        void Set<T>(string key, T value);

        /// <summary>
        ///     异步将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="value">该缓存项的数据。</param>
        Task SetAsync<T>(string key, T value);

        /// <summary>
        ///     将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="second">有效时长，单位：秒。</param>
        /// <param name="value">该缓存项的数据。</param>
        void Set<T>(string key, T value, int second);

        /// <summary>
        ///     异步将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="second">有效时长，单位：秒。</param>
        /// <param name="value">该缓存项的数据。</param>
        Task SetAsync<T>(string key, T value, int second);

        /// <summary>
        ///     将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="absoluteExpiration">缓存项过期时间。</param>
        /// <param name="value">该缓存项的数据。</param>
        void Set<T>(string key, T value, DateTime absoluteExpiration);

        /// <summary>
        ///     异步将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="absoluteExpiration">缓存项过期时间。</param>
        /// <param name="value">该缓存项的数据。</param>
        Task SetAsync<T>(string key, T value, DateTime absoluteExpiration);

        /// <summary>
        ///     将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="slidingExpiration">在此时间内访问缓存，缓存将继续有效。</param>
        /// <param name="value">该缓存项的数据。</param>
        void Set<T>(string key, T value, TimeSpan slidingExpiration);

        /// <summary>
        ///     异步将缓存项插入缓存中。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="key">要插入的缓存项的唯一标识符。</param>
        /// <param name="slidingExpiration">在此时间内访问缓存，缓存将继续有效。</param>
        /// <param name="value">该缓存项的数据。</param>
        Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration);

        /// <summary>
        ///     从缓存中移除某个缓存项。
        /// </summary>
        /// <param name="key">要移除的缓存项的唯一标识符。</param>
        void Remove(string key);

        /// <summary>
        ///     异步从缓存中移除某个缓存项。
        /// </summary>
        /// <param name="key">要移除的缓存项的唯一标识符。</param>
        Task RemoveAsync(string key);

        /// <summary>
        ///     清空缓存。
        /// </summary>
        void Clear();

        /// <summary>
        ///     异步清空缓存。
        /// </summary>
        /// <returns></returns>
        Task ClearAsync();
    }
}