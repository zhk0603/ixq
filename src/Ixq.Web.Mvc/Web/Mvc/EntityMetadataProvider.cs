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
    public class EntityMetadataProvider : IEntityMetadataProvider
    {
        private static readonly ConcurrentDictionary<string, IEntityMetadata> EntityMetadatas;

        static EntityMetadataProvider()
        {
            EntityMetadatas = new ConcurrentDictionary<string, IEntityMetadata>();
        }
        public IUserManager<Security.Identity.IUser> UserManager { get; set; }

        public IEntityMetadata GetEntityMetadata(Type type, IPrincipal user)
        {
            var key = type.FullName +
                      $"_{UserManager?.GetUserRolesByName(user.Identity.Name).Aggregate(0, (c, i) => i.GetHashCode())}";

            IEntityMetadata runtimeEntity;
            if (EntityMetadatas.TryGetValue(key, out runtimeEntity))
            {
                return runtimeEntity;
            }
            runtimeEntity = new EntityMetadata(type, user);
            EntityMetadatas[key] = runtimeEntity;
            return runtimeEntity;
        }

        public IEntityMetadata GetEntityMetadata<T>(IPrincipal user)
        {
            var type = typeof (T);
            return GetEntityMetadata(type, user);
        }
    }
}
