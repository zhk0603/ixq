using System.Collections.Generic;
using System.Reflection;

namespace Ixq.Core.Mapper
{
    /// <summary>
    ///     映射集合接口
    /// </summary>
    public interface IMapperCollection : IList<MapperDescriptor>
    {
        /// <summary>
        ///     初始化框架需要注册的映射信息。
        /// </summary>
        /// <param name="assembly"></param>
        void Init(Assembly[] assembly);
    }
}