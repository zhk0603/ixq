﻿using System;
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

        /// <summary>
        ///     获取实体元数据。
        /// </summary>
        /// <param name="type">实体类型。</param>
        /// <returns></returns>
        public IEntityMetadata GetEntityMetadata(Type type)
        {
            // 根据用户的所属角色生成唯一key，系统所有拥有相同角色的用户，将获取相同的元数据。
            var key = type.FullName;
            IEntityMetadata runtimeEntity;
            if (EntityMetadatas.TryGetValue(type.FullName, out runtimeEntity))
            {
                return runtimeEntity;
            }
            runtimeEntity = new EntityMetadata(type);
            EntityMetadatas[key] = runtimeEntity;
            return runtimeEntity;
        }

        /// <summary>
        ///     获取实体元数据。
        /// </summary>
        /// <typeparam name="T">实体类型。</typeparam>
        /// <returns></returns>
        public IEntityMetadata GetEntityMetadata<T>()
        {
            var type = typeof (T);
            return GetEntityMetadata(type);
        }
    }
}