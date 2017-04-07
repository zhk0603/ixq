using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Entity
{
    /// <summary>
    /// 实体接口
    /// </summary>
    public interface IEntity<TKey> where TKey: struct
    {
        TKey Id { get; set; }
    }
}
