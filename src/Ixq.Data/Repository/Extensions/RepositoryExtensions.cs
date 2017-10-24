using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Ixq.Core.Dto;
using Ixq.Core.Entity;
using Ixq.Core.Mapper;
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
        /// <typeparam name="TKey"></typeparam>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static DbContext GetDbContext<TEntity, TKey>(this IRepositoryBase<TEntity, TKey> repository)
            where TEntity : class, IEntity<TKey>, new()
        {
            if (repository.UnitOfWork == null)
            {
                throw new ArgumentNullException(nameof(repository.UnitOfWork), "未初始化工作单元");
            }
            return (DbContext) repository.UnitOfWork;
        }

        /// <summary>
        ///     获取上下文。
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDbContext"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static TDbContext GetDbContext<TEntity, TKey, TDbContext>(this IRepositoryBase<TEntity, TKey> repository)
            where TEntity : class, IEntity<TKey>, new()
            where TDbContext : DbContext
        {
            if (repository.UnitOfWork == null)
            {
                throw new ArgumentNullException(nameof(repository.UnitOfWork), "未初始化工作单元");
            }
            return (TDbContext) repository.UnitOfWork;
        }

        /// <summary>
        ///     针对对上下文和基础存储中给定类型的实体的访问返回一个 <see cref="DbSet{TEntity}" /> 实例。
        /// </summary>
        /// <typeparam name="TEntity">应为其返回一个集的类型实体。</typeparam>
        /// <typeparam name="TKey">实体主键类型。</typeparam>
        /// <param name="repository"></param>
        /// <returns>给定实体类型的集。</returns>
        public static DbSet<TEntity> GetDbSet<TEntity, TKey>(this IRepositoryBase<TEntity, TKey> repository)
            where TEntity : class, IEntity<TKey>, new()
        {
            if (repository.UnitOfWork == null)
            {
                throw new ArgumentNullException(nameof(repository.UnitOfWork), "未初始化工作单元");
            }

            return ((DbContext) repository.UnitOfWork).Set<TEntity>();
        }


        /// <summary>
        ///     将指定的 <see cref="IQueryable{TEntity}" /> 转为 <see cref="IDto" />
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public static TDto[] ToDtoArray<TDto, TEntity, TKey>(this IQueryable<TEntity> queryable)
            where TEntity : class, IEntity<TKey>, new()
            where TDto : class, IDto<TEntity, TKey>, new()
        {
            var entityColl = queryable.ToList();
            return entityColl.Select(item => item.MapToDto<TDto, TKey>()).ToArray();
        }

        /// <summary>
        ///     将指定的 <see cref="IQueryable{TEntity}" /> 转为 <see cref="IDto" />
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public static Task<TDto[]> ToDtoArrayAsync<TDto, TEntity, TKey>(this IQueryable<TEntity> queryable)
            where TEntity : class, IEntity<TKey>, new()
            where TDto : class, IDto<TEntity, TKey>, new()
        {
            return Task.FromResult(ToDtoArray<TDto, TEntity, TKey>(queryable));
        }

        /// <summary>
        ///     将 <see cref="IQueryable{TEntity}" /> 转为 <see cref="List{TDto}" />
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public static List<TDto> ToDtoList<TDto, TEntity, TKey>(this IQueryable<TEntity> queryable)
            where TEntity : class, IEntity<TKey>, new()
            where TDto : class, IDto<TEntity, TKey>, new()
        {
            var entityColl = queryable.ToList();
            return entityColl.Select(item => item.MapToDto<TDto, TKey>()).ToList();
        }

        /// <summary>
        ///     将 <see cref="IQueryable{TEntity}" /> 转为 <see cref="List{TDto}" />
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public static Task<List<TDto>> ToDtoListAsync<TDto, TEntity, TKey>(this IQueryable<TEntity> queryable)
            where TEntity : class, IEntity<TKey>, new()
            where TDto : class, IDto<TEntity, TKey>, new()
        {
            return Task.FromResult(ToDtoList<TDto, TEntity, TKey>(queryable));
        }

        public static object ParseEntityKey<TKey>(string value)
        {
            return Convert.ChangeType(value, typeof(TKey));
        }
    }
}