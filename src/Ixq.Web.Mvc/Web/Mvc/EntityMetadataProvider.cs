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

        /// <summary>
        ///     获取实体元数据。
        /// </summary>
        /// <param name="type">实体类型。</param>
        /// <param name="user">当前已进行身份验证的用户。</param>
        /// <returns></returns>

        public IEntityMetadata GetEntityMetadata(Type type, IPrincipal user)
        {
            // 根据用户的所属角色生成唯一key，系统所有拥有相同角色的用户，将获取相同的元数据。
            var key = type.FullName +
                      $"_{UserManager?.GetUserRolesByName(user.Identity.Name).Aggregate(0L, (c, i) => i.GetHashCode())}";

            IEntityMetadata runtimeEntity;
            if (EntityMetadatas.TryGetValue(key, out runtimeEntity))
            {
                return runtimeEntity;
            }
            runtimeEntity = new EntityMetadata(type, user);
            EntityMetadatas[key] = runtimeEntity;
            return runtimeEntity;
        }

        /// <summary>
        ///     获取实体元数据。
        /// </summary>
        /// <typeparam name="T">实体类型。</typeparam>
        /// <param name="user">当前已进行身份验证的用户。</param>
        /// <returns></returns>
        public IEntityMetadata GetEntityMetadata<T>(IPrincipal user)
        {
            var type = typeof (T);
            return GetEntityMetadata(type, user);
        }
    }
}
