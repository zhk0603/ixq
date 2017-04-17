using System;
using System.Data.Entity;
using Ixq.Core.Entity;
using Ixq.Core.Repository;

namespace Ixq.Data.Repository.Extensions
{
    /// <summary>
    ///     仓储扩展。
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        ///     获取上下文。
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static DbContext GetDbContext<TEntity>(this IRepository<TEntity> repository)
            where TEntity : class, IEntity<Guid>, new()
        {
            if (repository.UnitOfWork == null)
                throw new ArgumentNullException(nameof(repository.UnitOfWork), @"未初始化工作单元");
            return (DbContext) repository.UnitOfWork;
        }

        /// <summary>
        ///     获取仓储。
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IRepository<TEntity> GetRepository<TEntity>(this IServiceProvider serviceProvider)
            where TEntity : class, IEntity<Guid>, new()
        {
            return (IRepository<TEntity>) serviceProvider.GetService(typeof (IRepository<TEntity>));
        }
    }
}