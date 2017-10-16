using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Redis
{
    /// <summary>
    ///     序列化服务。
    /// </summary>
    public interface ISerializableService
    {
        /// <summary>
        ///     反序列化。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        object Deserialize(byte[] data);
        /// <summary>
        ///     序列化。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        byte[] Serialize(object obj);
    }
}
