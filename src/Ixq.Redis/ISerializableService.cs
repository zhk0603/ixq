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
        object Deserialize(byte[] data);
        byte[] Serialize(object obj);
    }
}
