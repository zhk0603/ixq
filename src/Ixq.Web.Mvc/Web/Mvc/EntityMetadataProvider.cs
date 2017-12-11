using System;
using System.Collections.Concurrent;
using Ixq.Core.Dto;
using Ixq.Core.Entity;
using Ixq.Extensions;

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
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var dtoInterfaceType = typeof(IDto<,>);
            if (!dtoInterfaceType.IsGenericAssignableFrom(type))
                throw new ArgumentException($"类型[{type.FullName}]，不实现接口[{dtoInterfaceType.FullName}]", nameof(type));

            var key = type.FullName;
            if (EntityMetadatas.TryGetValue(type.FullName, out var metadata))
                return metadata;
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
            var type = typeof(T);
            return GetEntityMetadata(type);
        }
    }
}