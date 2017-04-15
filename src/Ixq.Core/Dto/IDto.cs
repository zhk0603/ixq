using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Mapper;

namespace Ixq.Core.Dto
{
    /// <summary>
    /// 数据传输对象。
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDto<out TEntity, TKey> : IDto
    {
        /// <summary>
        /// 主键标识。
        /// </summary>
        TKey Index { get; set; }

        TEntity MapTo();
        IMapperCollection Mapper { get; set; }
    }
    public interface IDto { }
}
