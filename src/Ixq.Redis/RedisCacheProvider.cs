using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Cache;

namespace Ixq.Redis
{
    public class RedisCacheProvider : CacheProviderBase
    {
        public override ICache GetCache(string regionName)
        {
            throw new NotImplementedException();
        }
    }
}
