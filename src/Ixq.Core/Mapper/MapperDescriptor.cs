using System;

namespace Ixq.Core.Mapper
{
    /// <summary>
    ///     映射描述
    /// </summary>
    public class MapperDescriptor
    {
        public MapperDescriptor(Type sourceType, Type targetType)
        {
            TargetType = targetType;
            SourceType = sourceType;
        }

        public Type TargetType { get; }
        public Type SourceType { get; }
    }
}