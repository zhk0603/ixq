using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core;
using Ixq.Core.Entity;
using Ixq.Data.DataAnnotations;
using Ixq.Extensions;
using Ixq.UI.ComponentModel.DataAnnotations;
using DataType = Ixq.Core.DataType;

namespace Ixq.Web.Mvc
{
    public class RuntimePropertyMenberInfo : IRuntimePropertyMenberInfo
    {
        public RuntimePropertyMenberInfo(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException(nameof(propertyInfo));

            PropertyInfo = propertyInfo;
            PropertyType = propertyInfo.PropertyType;
            Initialization();
        }

        internal void Initialization()
        {
            IsSearcher = PropertyInfo.HasAttribute<SerializableAttribute>();
            IsRequired = PropertyInfo.HasAttribute<RequiredAttribute>();
            IsKey = PropertyInfo.HasAttribute<KeyAttribute>();

            var authorizationAttribute = PropertyInfo.GetAttribute<PropertyAuthorizationAttribute>();
            authorizationAttribute?.SetRuntimeProperty(this);

            var hideAttribute = PropertyInfo.GetAttribute<HideAttribute>();
            hideAttribute?.SetRuntimeProperty(this);

            var numberAttribute = PropertyInfo.GetAttribute<NumberAttribute>();
            numberAttribute?.SetRuntimeProperty(this);

            var colModelAttribute = PropertyInfo.GetAttribute<ColModelAttribute>();
            colModelAttribute?.SetRuntimeProperty(this);

            var displayAttribute = PropertyInfo.GetAttribute<DisplayAttribute>();
            if (displayAttribute != null)
            {
                Name = displayAttribute.GetName() ?? PropertyInfo.Name;
                Order = displayAttribute.GetOrder();
                Description = displayAttribute.GetDescription();
                GroupName = displayAttribute.GetGroupName();
            }
            
            var dataTypeAttribute = PropertyInfo.GetAttribute<UI.ComponentModel.DataAnnotations.DataTypeAttribute>();
            if (dataTypeAttribute != null)
            {
                dataTypeAttribute.SetRuntimeProperty(this);
            }
            else
            {
                DataType = EntityExtensions.GetDataType(PropertyInfo);
            }
        }

        public PropertyInfo PropertyInfo { get; set; }
        public Type PropertyType { get; }
        public string[] Roles { get; set; }
        public string[] Users { get; set; }
        public bool IsKey { get; set; }
        public bool IsHiddenOnView { get; set; }
        public bool IsHiddenOnEdit { get; set; }
        public bool IsHiddenOnCreate { get; set; }
        public bool IsHiddenOnDetail { get; set; }
        public bool IsSearcher { get; set; }
        public bool IsRequired { get; set; }

        public string Name { get; set; }
        public int? Order { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }

        public string CssClass { get; set; }
        public int Width { get; set; } = 150;
        public TextAlign Align { get; set; }
        public bool Sortable { get; set; }
        public string Formatter { get; set; }
        public string UnFormatter { get; set; }
        public DataType DataType { get; set; }
        public string CustomDataType { get; set; }
        public string PartialViewPath { get; set; }

        /// <summary>
        ///     获取或设置步长，默认：0.01。
        /// </summary>
        public double? Step { get; set; } = 0.01;
        /// <summary>
        ///     获取或设置最大值。
        /// </summary>
        public long? Max { get; set; }
        /// <summary>
        ///     获取或设置最小值。
        /// </summary>
        public long? Min { get; set; }

    }
}
