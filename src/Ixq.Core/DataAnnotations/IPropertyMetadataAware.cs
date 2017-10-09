using Ixq.Core.Entity;

namespace Ixq.Core.DataAnnotations
{
    /// <summary>
    ///     定义用于描述实体属性的Attribute，需要继承此接口。
    ///     在创建属性元数据时，将会自动查找所有实现此接口的Attribute并执行<see cref="OnPropertyMetadataCreating(IEntityPropertyMetadata)" />方法。
    /// </summary>
    public interface IPropertyMetadataAware
    {
        /// <summary>
        ///     在属性元数据创建时执行。
        /// </summary>
        /// <param name="runtimeProperty">实体元数据</param>
        void OnPropertyMetadataCreating(IEntityPropertyMetadata runtimeProperty);
    }
}