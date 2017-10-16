using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Redis
{
    [Serializable]
    public class RedisCacheValue
    {
        public RedisCacheValue()
        {
            InsertTime = DateTime.Now;
        }
        public string Key { get; set; }
        public object Value { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime? AbsoluteExpiration { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }
    }
}
