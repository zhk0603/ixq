using System;
using Ixq.Core.Entity;
using Ixq.Core.Repository;

namespace Ixq.Data.Repository
{

    /// <summary>
    ///     实体主键类型为<see cref="Guid"/>的仓储。
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : RepositoryBase<TEntity, Guid>, IRepository<TEntity>
        where TEntity : class, IEntity<Guid>, new()
    {

        /// <summary>
        ///     初始化一个<see cref="Repository{TEntity}"/>实例。
        /// </summary>
        /// <param name="unitOfWork"></param>
        public Repository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}