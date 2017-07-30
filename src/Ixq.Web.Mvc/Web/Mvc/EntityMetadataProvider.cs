using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Ixq.Core.Dto;
using Ixq.Core.Entity;
using Ixq.Core.Security;
using Ixq.Extensions;
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

        /// <summary>
        ///     获取实体元数据。
        /// </summary>
        /// <param name="type">实体类型。</param>
        /// <returns></returns>
        public virtual IEntityMetadata GetEntityMetadata(Type type)
        {
            var dtoInterfaceType = typeof(IDto<,>);
            if (!dtoInterfaceType.IsGenericAssignableFrom(type))
            {
                throw new ArgumentException($"类型[{type.FullName}]，不实现接口[{dtoInterfaceType.FullName}]", nameof(type));
            }

            var key = type.FullName;
            IEntityMetadata metadata;
            if (EntityMetadatas.TryGetValue(type.FullName, out metadata))
            {
                return metadata;
            }
            metadata = new EntityMetadata(type);
            EntityMetadatas[key] = metadata;
            return metadata;
        }

        /// <summary>
        ///     获取实体元数据。
        /// </summary>
        /// <typeparam name="T">实体类型。</typeparam>
        /// <returns></returns>
        public virtual IEntityMetadata GetEntityMetadata<T>()
        {
            var type = typeof (T);
            return GetEntityMetadata(type);
        }
    }
}
