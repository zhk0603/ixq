using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;

namespace Ixq.Core.Repository
{
    /// <summary>
    ///     实体仓储。实体主键为<see cref="int"/>。
    /// </summary>
    /// <typeparam name="TEntity">实体对象。</typeparam>
    public interface IRepositoryInt32<TEntity> : IRepositoryBase<TEntity, int> where TEntity : class, IEntity<int>, new()
    {
    }
}
