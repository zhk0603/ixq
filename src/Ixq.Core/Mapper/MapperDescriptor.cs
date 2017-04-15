using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Mapper
{
    /// <summary>
    /// 映射描述
    /// </summary>
    public class MapperDescriptor
    {
        public MapperDescriptor(Type sourceType, Type targetType)
        {
            this.TargetType = targetType;
            this.SourceType = sourceType;
        }

        public Type TargetType { get; }
        public Type SourceType { get; }
    }
}
