using Ixq.Core.Entity;

namespace Ixq.UI.ComponentModel
{
    /// <summary>
    ///     属性编辑模型接口。
    /// </summary>
    public interface IPropertyEditModel
    {
        /// <summary>
        ///     获取或设置实体属性元数据。
        /// </summary>
        IEntityPropertyMetadata EntityProperty { get; set; }

        /// <summary>
        ///     获取或设置数据传输对象。
        /// </summary>
        object EntityDto { get; set; }

        /// <summary>
        ///     获取属性的值。
        /// </summary>
        object Value { get; }
    }
}