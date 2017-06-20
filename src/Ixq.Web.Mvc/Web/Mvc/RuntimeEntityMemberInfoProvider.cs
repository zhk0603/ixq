using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Ixq.Core.Entity;
using Ixq.Core.Security;
using Microsoft.AspNet.Identity;
using Ixq.Security.Identity;

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
        public IUserManager<Security.Identity.IUser> UserManager { get; set; }

        public IRuntimeEntityMenberInfo GetRuntimeEntityMenberInfo(Type type, IPrincipal user)
        {
            var key = type.FullName +
                      $"_{UserManager?.GetUserRolesByName(user.Identity.Name).Aggregate(0, (c, i) => i.GetHashCode())}";

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
