using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ixq.Redis
{
    public class XmlSerializableService : ISerializableService
    {
        public object Deserialize(byte[] data)
        {
            var xs = new XmlSerializer(GetType());
            using (var memoryStream = new MemoryStream(data))
            {
                var result = xs.Deserialize(memoryStream);
                return result;
            }
        }

        public byte[] Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            using (var memoryStream = new MemoryStream())
            {
                xs.Serialize(memoryStream, obj);
                var data = memoryStream.ToArray();
                return data;
            }
        }
    }
}
