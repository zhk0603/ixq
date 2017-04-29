using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using Ixq.Core.Dto;
using Ixq.Core.Entity;
using Ixq.Core.Repository;
using Ixq.Core.Mapper;

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
        public static TDbContext GetDbContext<TEntity, TDbContext>(this IRepository<TEntity> repository)
            where TEntity : class, IEntity<Guid>, new()
            where TDbContext : DbContext
        {
            if (repository.UnitOfWork == null)
                throw new ArgumentNullException(nameof(repository.UnitOfWork), @"未初始化工作单元");
            return (TDbContext)repository.UnitOfWork;
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


        /// <summary>
        /// 将指定的 <see cref="IQueryable{TEntity}"/> 转为 <see cref="IDto"/>
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public static TDto[] ToDtoArray<TDto, TEntity>(this IQueryable<TEntity> queryable)
            where TEntity : class, IEntity<Guid>, new()
            where TDto : class, IDto<IEntity<Guid>, Guid>, new()
        {
            return queryable.ToList().Select(item => item.MapToDto<TDto>()).ToArray();
        }

        /// <summary>
        /// 将 <see cref="IQueryable{TEntity}"/> 转为 <see cref="List{TDto}"/>
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public static List<TDto> ToDtoList<TDto, TEntity>(this IQueryable<TEntity> queryable)
            where TEntity : class, IEntity<Guid>, new()
            where TDto : class, IDto<IEntity<Guid>, Guid>, new()
        {
            return queryable.ToList().Select(item => item.MapToDto<TDto>()).ToList();
        }
    }
}