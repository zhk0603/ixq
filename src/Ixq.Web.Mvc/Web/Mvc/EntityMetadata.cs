using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using Ixq.Core.Entity;
using Ixq.Data.DataAnnotations;
using Ixq.Extensions;

namespace Ixq.Web.Mvc
{
    /// <summary>
    ///     实体元数据。
    /// </summary>
    [Serializable]
    public class EntityMetadata : IEntityMetadata
    {
        private static readonly object _lockObj = new object();
        private readonly Type _entityType;
        private readonly IPrincipal _user;
        private PropertyInfo[] _entityPropertys;
        private IEntityPropertyMetadata[] _propertyMetadatas;

        /// <summary>
        ///     初始化一个<see cref="EntityMetadata"/>。
        /// </summary>
        /// <param name="entityType">实体类型。</param>
        /// <param name="user">用户。</param>
        public EntityMetadata(Type entityType, IPrincipal user)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));

            _entityType = entityType;
            _user = user;
        }

        public IEntityPropertyMetadata[] ViewPropertyMetadatas
        {
            get { return PropertyMetadatas.Where(x => !x.IsHiddenOnView).ToArray(); }
        }

        public IEntityPropertyMetadata[] CreatePropertyMetadatas
        {
            get { return PropertyMetadatas.Where(x => !x.IsHiddenOnCreate).ToArray(); }
        }

        public IEntityPropertyMetadata[] EditPropertyMetadatas
        {
            get { return PropertyMetadatas.Where(x => !x.IsHiddenOnEdit).ToArray(); }
        }

        public IEntityPropertyMetadata[] DetailPropertyMetadatas
        {
            get { return PropertyMetadatas.Where(x => !x.IsHiddenOnDetail).ToArray(); }
        }

        public IEntityPropertyMetadata[] SearcherPropertyMetadatas
        {
            get { return PropertyMetadatas.Where(x => x.IsSearcher).ToArray(); }
        }

        public IEntityPropertyMetadata[] PropertyMetadatas
        {
            get
            {
                if (_propertyMetadatas == null)
                {
                    lock (_lockObj)
                    {
                        if (_propertyMetadatas == null)
                            _propertyMetadatas = GetPropertyMetadatas();
                    }
                }
                return _propertyMetadatas;
            }
        }

        public PropertyInfo[] EntityPropertyInfos => _entityPropertys ?? (_entityPropertys = _entityType.GetProperties());

        /// <summary>
        ///     获取属性元数据。
        /// </summary>
        /// <returns></returns>
        protected virtual IEntityPropertyMetadata[] GetPropertyMetadatas()
        {
            var propertyMetadatas = new List<IEntityPropertyMetadata>();
            foreach (var property in EntityPropertyInfos)
            {
                if (!property.HasAttribute<DisplayAttribute>()) continue;

                var propertyAuthorizationAttribute = property.GetAttribute<PropertyAuthorizationAttribute>();
                if (propertyAuthorizationAttribute != null &&
                    !propertyAuthorizationAttribute.IsAuthorization(_user))
                {
                    continue;
                }
                var runtimeProperty = new EntityPropertyMetadata(property);
                propertyMetadatas.Add(runtimeProperty);
            }

            return propertyMetadatas.OrderBy(x => x.Order).ToArray();
        }

    }
}