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
    [Serializable]
    public class RuntimeEntityMenberInfo : IRuntimeEntityMenberInfo
    {
        private readonly Type _entityType;
        private readonly IPrincipal _user;
        private PropertyInfo[] _entityPropertys;
        private IRuntimePropertyMenberInfo[] _createPropertyInfo;
        private IRuntimePropertyMenberInfo[] _detailPropertyInfo;
        private IRuntimePropertyMenberInfo[] _editPropertyInfo;
        private IRuntimePropertyMenberInfo[] _searcherPropertyInfo;
        private IRuntimePropertyMenberInfo[] _viewPropertyInfo;

        public RuntimeEntityMenberInfo(Type entityType, IPrincipal user)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));

            _entityType = entityType;
            _user = user;
        }

        internal PropertyInfo[] EntityPropertys
        {
            get
            {
                if (_entityPropertys != null)
                    return _entityPropertys;
                _entityPropertys = _entityType.GetProperties();
                return _entityPropertys;
            }
        }

        public IRuntimePropertyMenberInfo[] ViewPropertyInfo
        {
            get { return _viewPropertyInfo ?? (_viewPropertyInfo = GetProperty(hide => hide.IsHiddenOnView)); }
        }

        public IRuntimePropertyMenberInfo[] CreatePropertyInfo
        {
            get { return _createPropertyInfo ?? (_createPropertyInfo = GetProperty(hide => hide.IsHiddenOnCreate)); }
        }

        public IRuntimePropertyMenberInfo[] EditPropertyInfo
        {
            get { return _editPropertyInfo ?? (_editPropertyInfo = GetProperty(hide => hide.IsHiddenOnEdit)); }
        }

        public IRuntimePropertyMenberInfo[] DetailPropertyInfo
        {
            get { return _detailPropertyInfo ?? (_detailPropertyInfo = GetProperty(hide => hide.IsHiddenOnDetail)); }
        }

        public IRuntimePropertyMenberInfo[] SearcherPropertyInfo
        {
            get { return _searcherPropertyInfo ?? (_searcherPropertyInfo = GetSearcherPropertyInfo()); }
        }

        public virtual IRuntimePropertyMenberInfo[] GetProperty(Func<HideAttribute, bool> func)
        {
            var viewPropertys = new List<IRuntimePropertyMenberInfo>();
            foreach (var property in EntityPropertys)
            {
                if (!property.HasAttribute<DisplayAttribute>()) continue;

                var hideAttribute = property.GetAttribute<HideAttribute>();
                if (hideAttribute != null)
                {
                    if (func(hideAttribute))
                        continue;
                }
                var propertyAuthorizationAttribute = property.GetAttribute<PropertyAuthorizationAttribute>();
                if (propertyAuthorizationAttribute != null &&
                    !propertyAuthorizationAttribute.IsAuthorization(_user))
                {
                    continue;
                }
                var runtimeProperty = new RuntimePropertyMenberInfo(property);
                viewPropertys.Add(runtimeProperty);
            }

            return viewPropertys.OrderBy(x => x.Order).ToArray();
        }

        public virtual IRuntimePropertyMenberInfo[] GetSearcherPropertyInfo()
        {
            var viewPropertys = new List<IRuntimePropertyMenberInfo>();

            foreach (var property in EntityPropertys)
            {
                if (!property.HasAttribute<SearcherAttribute>()) continue;

                var propertyAuthorizationAttribute = property.GetAttribute<PropertyAuthorizationAttribute>();
                if (propertyAuthorizationAttribute != null &&
                    !propertyAuthorizationAttribute.IsAuthorization(_user))
                {
                    continue;
                }
                var runtimeProperty = new RuntimePropertyMenberInfo(property);
                viewPropertys.Add(runtimeProperty);
            }

            return viewPropertys.OrderBy(x => x.Order).ToArray();
        }
    }
}