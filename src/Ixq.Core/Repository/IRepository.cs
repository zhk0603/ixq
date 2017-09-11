using System;
using Ixq.Core.Entity;

namespace Ixq.Core.Repository
{
    /// <summary>
    ///     实体仓储。实体主键为<see cref="Guid"/>。
    /// </summary>
    /// <typeparam name="TEntity">实体对象。</typeparam>
    public interface IRepository<TEntity> : IRepositoryBase<TEntity, Guid> where TEntity : class, IEntity<Guid>, new()
    {
    }
}