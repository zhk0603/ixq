using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;
using Ixq.Core.Dto;
using Ixq.Core.Entity;
using Ixq.Core.Mapper;
using Ixq.Core.Repository;
using Ixq.Extensions;
using System.Data.Entity.Infrastructure;

namespace Ixq.Data.Repository
{
    /// <summary>
    ///     EntityFramework的仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>, IScopeDependency
        where TEntity : class, IEntity<TKey>, new()
    {
        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        private DbSet<TEntity> Table => ((DbContext) UnitOfWork).Set<TEntity>();
        public IUnitOfWork UnitOfWork { get; }


        public virtual bool Add(TEntity entity)
        {
            Table.Add(entity);
            return Save();
        }

        public virtual Task<bool> AddAsync(TEntity entity)
        {
            Table.Add(entity);
            return SaveAsync();
        }

        public virtual bool AddRange(IEnumerable<TEntity> entities)
        {
            Table.AddRange(entities);
            return Save();
        }

        public virtual Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            Table.AddRange(entities);
            return SaveAsync();
        }

        public virtual TEntity Create()
        {
            var entity = Table.Create();
            return entity;
        }

        public virtual bool Edit(TEntity entity)
        {
            var entry = ((DbContext) UnitOfWork).Entry(entity);
            entry.State = EntityState.Modified;
            return Save();
        }

        public virtual Task<bool> EditAsync(TEntity entity)
        {
            var entry = ((DbContext) UnitOfWork).Entry(entity);
            entry.State = EntityState.Modified;
            return SaveAsync();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Table.AsNoTracking();
        }

        public virtual Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        /// <summary>
        ///     指定要包括在查询结果中的相关对象。
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAllInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var res = GetAll();
            foreach (var includePropertie in includeProperties)
            {
                res = res.Include(includePropertie);
            }
            return res;
        }

        /// <summary>
        ///     指定要包括在查询结果中的相关对象。
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual Task<IQueryable<TEntity>> GetAllIncludeAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Task.FromResult(GetAllInclude(includeProperties));
        }

        public IQueryable<TEntity> GetAllBy(IEnumerable<IEntityPropertyMetadata> metadata)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> GetAllByAsync(IEnumerable<IEntityPropertyMetadata> metadata)
        {
            throw new NotImplementedException();
        }

        public virtual TDto GetSingleDtoById<TDto>(TKey index) where TDto : class, IDto<TEntity, TKey>
        {
            var entity = SingleById(index);
            return entity.MapToDto<TDto, TEntity, TKey>();
        }

        public virtual Task<TDto> GetSingleDtoByIdAsync<TDto>(TKey index)
            where TDto : class, IDto<TEntity, TKey>
        {
            return Task.FromResult(GetSingleDtoById<TDto>(index));
        }

        public virtual IQueryable<TEntity> OrderBy(string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            return GetAll().OrderBy(propertyName, sortDirection);
        }

        public virtual Task<IQueryable<TEntity>> OrderByAsync(string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            return Task.FromResult(OrderBy(propertyName, sortDirection));
        }

        public virtual IQueryable<TEntity> OrderByDesc(string propertyName)
        {
            return OrderBy(propertyName, ListSortDirection.Descending);
        }

        public virtual Task<IQueryable<TEntity>> OrderByDescAsync(string propertyName)
        {
            return OrderByAsync(propertyName, ListSortDirection.Descending);
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public virtual Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Query(predicate));
        }

        public virtual bool Remove(TKey index)
        {
            var entity = SingleById(index);
            return Remove(entity);
        }

        public virtual bool Remove(TEntity entity)
        {
            Table.Remove(entity);
            return Save();
        }

        public virtual Task<bool> RemoveAsync(TKey index)
        {
            var entity = SingleById(index);
            return RemoveAsync(entity);
        }

        public virtual Task<bool> RemoveAsync(TEntity entity)
        {
            Table.Remove(entity);
            return SaveAsync();
        }

        public virtual bool RemoveRange(IEnumerable<TKey> range)
        {
            foreach (var index in range)
            {
                var entity = SingleById(index);
                if (entity != null)
                    Table.Remove(entity);
            }
            return Save();
        }

        public virtual bool RemoveRange(IEnumerable<TEntity> range)
        {
            Table.RemoveRange(range);
            return Save();
        }

        public virtual Task<bool> RemoveRangeAsync(IEnumerable<TKey> range)
        {
            foreach (var index in range)
            {
                var entity = SingleById(index);
                Table.Remove(entity);
            }
            return SaveAsync();
        }

        public virtual Task<bool> RemoveRangeAsync(IEnumerable<TEntity> range)
        {
            Table.RemoveRange(range);
            return SaveAsync();
        }

        public virtual bool Save()
        {
            try
            {
                var count = UnitOfWork.Save();
                return count > 0;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public virtual async Task<bool> SaveAsync()
        {
            try
            {
                var count = await UnitOfWork.SaveAsync();
                return count > 0;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public virtual IQueryable<TResult> FilterField<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
           return GetAll().Select(selector);
        }

        public virtual TEntity SingleBy(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().SingleOrDefault(predicate);
        }

        public virtual Task<TEntity> SingleByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().SingleOrDefaultAsync(predicate);
        }

        public virtual TEntity SingleById(TKey index)
        {
            return Table.Find(index);
        }

        public virtual Task<TEntity> SingleByIdAsync(TKey index)
        {
            return Table.FindAsync(index);
        }

        public virtual TEntity SingleById(params object[] index)
        {
            return Table.Find(index);
        }

        public virtual Task<TEntity> SingleByIdAsync(params object[] index)
        {
            return Table.FindAsync(index);
        }


        public virtual IEnumerable<TEntity> SqlQuery(string sql, bool trackEnabled = true, params object[] parameters)
        {
            return trackEnabled
                ? Table.SqlQuery(sql, parameters)
                : Table.SqlQuery(sql, parameters).AsNoTracking();
        }

        public virtual Task<IEnumerable<TEntity>> SqlQueryAsync(string sql, bool trackEnabled = true,
            params object[] parameters)
        {
            return Task.FromResult(SqlQuery(sql, trackEnabled, parameters));
        }

        public virtual TEntity SqlQuerySingle(TKey index, bool trackEnabled = true)
        {
            var tableName = GetTableName<TEntity>();

            var sql = "select * from " + tableName + " where [Id] = @index";
            return trackEnabled
                ? Table.SqlQuery(sql, new SqlParameter("@index", index))
                    .FirstOrDefault()
                : Table.SqlQuery(sql, new SqlParameter("@index", index))
                    .AsNoTracking()
                    .FirstOrDefault();
        }

        public virtual Task<TEntity> SqlQuerySingleAsync(TKey index, bool trackEnabled = true)
        {
            return Task.FromResult(SqlQuerySingle(index, trackEnabled));
        }

        public virtual IEnumerable<T2> SqlQuery<T2, TKey2>(string sql, bool trackEnabled = true,
            params object[] parameters)
            where T2 : class, IEntity<TKey2>, new()
        {
            var table = ((DbContext) UnitOfWork).Set<T2>();
            return trackEnabled
                ? table.SqlQuery(sql, parameters)
                : table.SqlQuery(sql, parameters).AsNoTracking();
        }

        public virtual Task<IEnumerable<T2>> SqlQueryAsync<T2, TKey2>(string sql, bool trackEnabled,
            params object[] parameters)
            where T2 : class, IEntity<TKey2>, new()
        {
            return Task.FromResult(SqlQuery<T2, TKey2>(sql, trackEnabled, parameters));
        }

        public virtual T2 SqlQuerySingle<T2, TKey2>(TKey index, bool trackEnabled)
            where T2 : class, IEntity<TKey2>, new()
        {
            var table = ((DbContext) UnitOfWork).Set<T2>();

            var tableName = GetTableName<T2>();
            var sql = "select * from " + tableName + " where [Index] = @index";

            return trackEnabled
                ? table.SqlQuery(sql, new SqlParameter("@index", index))
                    .FirstOrDefault()
                : table.SqlQuery(sql, new SqlParameter("@index", index))
                    .AsNoTracking()
                    .FirstOrDefault();
        }

        public virtual Task<T2> SqlQuerySingleAsync<T2, TKey2>(TKey index, bool trackEnabled)
            where T2 : class, IEntity<TKey2>, new()
        {
            return Task.FromResult(SqlQuerySingle<T2, TKey2>(index, trackEnabled));
        }


        /// <summary>
        ///     查看实体是否有 <see cref="TableAttribute" /> 特性，如果有则返回 <see cref="TableAttribute.Name" />.
        ///     默认返回实体的类名。
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <returns></returns>
        protected virtual string GetTableName<TType>()
        {
            var type = typeof (TType);
            var tableName = GetEntityDataBaseTableName<TType>((DbContext) UnitOfWork) ?? type.Name;
            var tableAttribute = type.GetAttribute<TableAttribute>();
            if (tableAttribute != null)
            {
                tableName = tableAttribute.Name;
            }
            return tableName;
        }

        /// <summary>
        ///     获取数据库中的表名。
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="contextAdapter"></param>
        /// <returns></returns>
        private string GetEntityDataBaseTableName<T>(IObjectContextAdapter contextAdapter)
        {
            if (contextAdapter == null)
            {
                throw new ArgumentNullException(nameof(contextAdapter));
            }

            var ojbectContext = contextAdapter.ObjectContext;
            var className = typeof (T).Name;

            var container = ojbectContext.MetadataWorkspace.GetItemCollection(DataSpace.SSpace).GetItems<EntityContainer>().Single();
            var entityTableName = (from meta in container.BaseEntitySets.OfType<EntitySet>()
                where
                    (!meta.MetadataProperties.Contains("Type") || meta.MetadataProperties["Type"].ToString() == "Tables") &&
                    meta.ElementType.Name == className
                select meta.Table).FirstOrDefault();

            return entityTableName;
        }
    }
}