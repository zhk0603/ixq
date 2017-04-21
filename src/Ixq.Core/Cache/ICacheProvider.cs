using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Cache
{
    /// <summary>
    /// 缓存提供者接口。
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// 获取全局缓存实例。
        /// </summary>
        /// <returns></returns>
        ICache GetGlobalCache();
        /// <summary>
        /// 获取 <see cref="ICache"/>
        /// </summary>
        /// <param name="regionName">缓存区域。</param>
        /// <returns></returns>
        ICache GetCache(string regionName);
        /// <summary>
        /// 获取全部的<see cref="ICache"/>
        /// </summary>
        /// <returns></returns>
        IDictionary<string, ICache> GetAllRegionCaches();
        /// <summary>
        /// 移除指定的<see cref="ICache"/>
        /// </summary>
        /// <param name="regionName">缓存区域。</param>
        void RemoveCache(string regionName);
        /// <summary>
        /// 移除全部<see cref="ICache"/>
        /// </summary>
        void RemoveAllRegionCahces();
    }
}
