using System;
using Ixq.Core.Entity;

namespace Ixq.Core.Repository
{
    public interface IRepository<TEntity> : IRepositoryBase<TEntity, Guid> where TEntity : class, IEntity<Guid>, new()
    {
    }
}