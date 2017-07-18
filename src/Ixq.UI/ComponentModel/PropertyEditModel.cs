using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;

namespace Ixq.UI.ComponentModel
{
    /// <summary>
    ///     属性编辑模型。
    /// </summary>
    public class PropertyEditModel : IPropertyEditModel
    {
        /// <summary>
        ///     初始化一个<see cref="PropertyEditModel"/>对象。
        /// </summary>
        public PropertyEditModel() { }
        /// <summary>
        ///     初始化一个<see cref="PropertyEditModel"/>对象。
        /// </summary>
        /// <param name="propertyMetadata">实体属性元数据。</param>
        /// <param name="entityDto">数据传输对象。</param>
        public PropertyEditModel(IEntityPropertyMetadata propertyMetadata, object entityDto)
        {
            this.EntityProperty = propertyMetadata;
            this.EntityDto = entityDto;
        }

        /// <summary>
        ///     获取或设置实体属性元数据。
        /// </summary>
        public IEntityPropertyMetadata EntityProperty { get; set; }
        /// <summary>
        ///     获取或设置数据传输对象。
        /// </summary>
        public object EntityDto { get; set; }

        /// <summary>
        ///     获取属性的值。
        /// </summary>
        public object Value => EntityProperty.PropertyInfo.GetValue(EntityDto);
    }
}
