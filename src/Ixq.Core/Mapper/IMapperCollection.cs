using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
