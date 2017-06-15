using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Ixq.Core.Entity;

namespace Ixq.Web.Mvc
{
    /// <summary>
    ///     运行时实体信息提供者。
    /// </summary>
    public class RuntimeEntityMemberInfoProvider : IRuntimeEntityMemberInfoProvider
    {
        private static readonly ConcurrentDictionary<string, IRuntimeEntityMenberInfo> RuntimeEntityMenberInfo;

        static RuntimeEntityMemberInfoProvider()
        {
            RuntimeEntityMenberInfo = new ConcurrentDictionary<string, IRuntimeEntityMenberInfo>();
        }

        public IRuntimeEntityMenberInfo GetRuntimeEntityMenberInfo(Type type, IPrincipal user)
        {
            var key = type.FullName + "_role:";

            IRuntimeEntityMenberInfo runtimeEntity;
            if (RuntimeEntityMenberInfo.TryGetValue(key, out runtimeEntity))
            {
                return runtimeEntity;
            }
            runtimeEntity = new RuntimeEntityMenberInfo(type, user);
            RuntimeEntityMenberInfo[key] = runtimeEntity;
            return runtimeEntity;
        }

        public IRuntimeEntityMenberInfo GetRuntimeEntityMenberInfo<T>(IPrincipal user)
        {
            var type = typeof (T);
            return GetRuntimeEntityMenberInfo(type, user);
        }
    }
}
