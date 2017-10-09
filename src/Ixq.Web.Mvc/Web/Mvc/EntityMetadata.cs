using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Web.Mvc;
using Ixq.Core.DataAnnotations;
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
        /// <summary>
        ///     授权用户委托。
        /// </summary>
        /// <returns></returns>
        public delegate ClaimsPrincipal ClaimsUserDelegate();

        private static readonly object LockObj = new object();
        private readonly Type _entityType;
        private PropertyInfo[] _entityPropertys;
        private IEntityPropertyMetadata[] _propertyMetadatas;

        /// <summary>
        ///     初始化一个<see cref="EntityMetadata" />。
        /// </summary>
        /// <param name="dtoType">数据传输对象类型。</param>
        public EntityMetadata(Type dtoType)
        {
            if (dtoType == null)
            {
                throw new ArgumentNullException(nameof(dtoType));
            }

            DtoType = dtoType;
        }

        /// <summary>
        ///     获取或设置授权用户委托方法。
        /// </summary>
        public static ClaimsUserDelegate CurrentClaimsUser { get; set; }

        /// <summary>
        ///     获取实体在列表中的属性元数据。
        /// </summary>
        public IEntityPropertyMetadata[] ViewPropertyMetadatas
        {
            get
            {
                return
                    PropertyMetadatas.Where(x => !x.IsHiddenOnView && x.IsAuthorization(CurrentClaimsUser())).ToArray();
            }
        }

        /// <summary>
        ///     获取实体在创建时的属性元数据。
        /// </summary>
        public IEntityPropertyMetadata[] CreatePropertyMetadatas
        {
            get
            {
                return
                    PropertyMetadatas.Where(x => !x.IsHiddenOnCreate && x.IsAuthorization(CurrentClaimsUser()))
                        .ToArray();
            }
        }

        /// <summary>
        ///     获取实体在编辑时的属性元数据。
        /// </summary>
        public IEntityPropertyMetadata[] EditPropertyMetadatas
        {
            get
            {
                return
                    PropertyMetadatas.Where(x => !x.IsHiddenOnEdit && x.IsAuthorization(CurrentClaimsUser())).ToArray();
            }
        }

        /// <summary>
        ///     获取在查看实体详情时的属性元数据。
        /// </summary>
        public IEntityPropertyMetadata[] DetailPropertyMetadatas
        {
            get
            {
                return
                    PropertyMetadatas.Where(x => !x.IsHiddenOnDetail && x.IsAuthorization(CurrentClaimsUser()))
                        .ToArray();
            }
        }

        /// <summary>
        ///     获取能搜索实体的属性元数据。
        /// </summary>
        public IEntityPropertyMetadata[] SearcherPropertyMetadatas
        {
            get
            {
                return PropertyMetadatas.Where(x => x.IsSearcher && x.IsAuthorization(CurrentClaimsUser())).ToArray();
            }
        }

        /// <summary>
        ///     获取数据传输对象类型。
        /// </summary>
        public Type DtoType { get; }

        /// <summary>
        ///     获取实体所有公共的属性元数据。
        /// </summary>
        public IEntityPropertyMetadata[] PropertyMetadatas
        {
            get
            {
                if (_propertyMetadatas == null)
                {
                    lock (LockObj)
                    {
                        if (_propertyMetadatas == null)
                        {
                            _propertyMetadatas = GetPropertyMetadatas();
                        }
                    }
                }
                return _propertyMetadatas;
            }
        }

        /// <summary>
        ///     获取实体所有的公共属性。
        /// </summary>
        public PropertyInfo[] EntityPropertyInfos
            => _entityPropertys ?? (_entityPropertys = DtoType.GetProperties());

        /// <summary>
        ///     获取属性元数据。
        /// </summary>
        /// <returns></returns>
        protected virtual IEntityPropertyMetadata[] GetPropertyMetadatas()
        {
            var propertyMetadatas = new List<IEntityPropertyMetadata>();
            foreach (var property in EntityPropertyInfos)
            {
                if (!property.HasAttribute<DisplayAttribute>())
                {
                    continue;
                }
                var runtimeProperty = GetEntityPropertyMetadata(property);
                propertyMetadatas.Add(runtimeProperty);
            }

            return propertyMetadatas.OrderBy(x => x.Order).ToArray();
        }

        /// <summary>
        ///     应用属性元数据感知属性。
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="metadata"></param>
        protected virtual void ApplyPropertyMetadataAwareAttributes(IEnumerable<Attribute> attributes,
            IEntityPropertyMetadata metadata)
        {
            foreach (var attribute in attributes.OfType<IPropertyMetadataAware>())
            {
                attribute.OnPropertyMetadataCreating(metadata);
            }
        }

        /// <summary>
        ///     获取实体属性的元数据，根据数据注释属性以及属性元数据感知属性初始化一个<see cref="IEntityMetadataProvider" />实例。
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        protected virtual IEntityPropertyMetadata GetEntityPropertyMetadata(PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes();
            var propertyMtadata = CreateEntityPropertyMetadata(property, attributes);
            ApplyPropertyMetadataAwareAttributes(attributes, propertyMtadata);
            return propertyMtadata;
        }

        /// <summary>
        ///     创建实体属性元数据。
        /// </summary>
        /// <param name="property"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        protected virtual IEntityPropertyMetadata CreateEntityPropertyMetadata(PropertyInfo property,
            IEnumerable<Attribute> attributes)
        {
            var result = new EntityPropertyMetadata(property)
            {
                IsSearcher = attributes.Any(x => x is SearcherAttribute),
                IsRequired = attributes.Any(x => x is RequiredAttribute),
                IsKey = attributes.Any(x => x is KeyAttribute),
                DataType = EntityExtensions.GetDataType(property)
            };

            var display = attributes.OfType<DisplayAttribute>().FirstOrDefault();
            string name = null;
            if (display != null)
            {
                result.Description = display.GetDescription();
                result.Order = display.GetOrder() ?? ModelMetadata.DefaultOrder;
                result.GroupName = display.GetGroupName();
                name = display.GetName();
            }

            if (name != null)
            {
                result.Name = name;
            }
            else
            {
                var displayNameAttribute = attributes.OfType<DisplayNameAttribute>().FirstOrDefault();
                if (displayNameAttribute != null)
                {
                    result.Name = displayNameAttribute.DisplayName;
                }
            }
            if (string.IsNullOrEmpty(result.Name))
            {
                result.Name = property.Name;
            }

            return result;
        }
    }
}