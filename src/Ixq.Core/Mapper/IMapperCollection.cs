using System.Collections.Generic;
using System.Reflection;

namespace Ixq.Core.Mapper
{
    /// <summary>
    ///     映射集合接口
    /// </summary>
    public interface IMapperCollection : IList<MapperDescriptor>
    {
        void Init(Assembly[] assembly);
    }
}