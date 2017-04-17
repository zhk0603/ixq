using System;
using Ixq.Core.Entity;
using Ixq.Core.Repository;

namespace Ixq.Data.Repository
{
    public class Repository<TEntity> : RepositoryBase<TEntity, Guid>, IRepository<TEntity>
        where TEntity : class, IEntity<Guid>, new()
    {
        public Repository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}