using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Cache
{
    public interface ICacheProvider
    {
        ICache GetGlobalCache();
        ICache GetCache(string regionName);
        IDictionary<string, ICache> GetAllRegionCaches();
        void RemoveCache(string regionName);
        void RemoveAllRegionCahces();
    }
}
