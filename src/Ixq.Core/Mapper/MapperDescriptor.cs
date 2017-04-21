using System;

namespace Ixq.Core.Mapper
{
    /// <summary>
    ///     映射描述。
    /// </summary>
    public class MapperDescriptor
    {
        /// <summary>
        ///     初始化一个<see cref="MapperDescriptor" />实例。
        /// </summary>
        /// <param name="sourceType">原类型。</param>
        /// <param name="targetType">目标类型。</param>
        public MapperDescriptor(Type sourceType, Type targetType)
        {
            TargetType = targetType;
            SourceType = sourceType;
        }

        /// <summary>
        ///     获取原类型。
        /// </summary>
        public Type TargetType { get; }

        /// <summary>
        ///     获取目标类型。
        /// </summary>
        public Type SourceType { get; }
    }
}