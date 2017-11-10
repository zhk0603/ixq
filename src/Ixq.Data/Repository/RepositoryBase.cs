using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
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

namespace Ixq.Data.Repository
{
    /// <summary>
    ///     EntityFramework的仓储实现。
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>, IScopeDependency
        where TEntity : class, IEntity<TKey>, new()
    {
        private readonly DbSet<TEntity> _table;
        private readonly DbContext _dbContext;

        /// <summary>
        ///     初始化一个<see cref="RepositoryBase{TEntity, TKey}" />实例。
        /// </summary>
        /// <param name="unitOfWork"></param>
        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _dbContext = (DbContext) unitOfWork ??
                         throw new ArgumentNullException(
                             $"无法将类型：{typeof(IUnitOfWork).FullName} 转换为：{typeof(DbContext).FullName}。");
            _table = _dbContext.Set<TEntity>();
        }

        /// <summary>
        ///     工作单元。
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        ///     添加一个对象。
        /// </summary>
        /// <param name="entity"></param>
        public virtual bool Add(TEntity entity)
        {
            _table.Add(entity);
            return Save();
        }

        /// <summary>
        ///     异步添加一个对象。
        /// </summary>
        /// <param name="entity"></param>
        public virtual Task<bool> AddAsync(TEntity entity)
        {
            _table.Add(entity);
            return SaveAsync();
        }

        /// <summary>
        ///     添加一个集合中的数据。
        /// </summary>
        /// <param name="entities"></param>
        public virtual bool AddRange(IEnumerable<TEntity> entities)
        {
            _table.AddRange(entities);
            return Save();
        }

        /// <summary>
        ///     异步添加一个集合中的数据。
        /// </summary>
        /// <param name="entities"></param>
        public virtual Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            _table.AddRange(entities);
            return SaveAsync();
        }

        /// <summary>
        ///     根据上下文创建一个对象。
        /// </summary>
        /// <returns>创建好的对象</returns>
        public virtual TEntity Create()
        {
            var entity = _table.Create();
            return entity;
        }

        /// <summary>
        ///     编辑一个对象。
        /// </summary>
        /// <param name="entity"></param>
        public virtual bool Edit(TEntity entity)
        {
            var entry = _dbContext.Entry(entity);
            entry.State = EntityState.Modified;
            return Save();
        }

        /// <summary>
        ///     异步编辑一个对象。
        /// </summary>
        /// <param name="entity"></param>
        public virtual Task<bool> EditAsync(TEntity entity)
        {
            var entry = _dbContext.Entry(entity);
            entry.State = EntityState.Modified;
            return SaveAsync();
        }

        /// <summary>
        ///     提取所有元素。
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAll()
        {
            return _table.AsNoTracking();
        }

        /// <summary>
        ///     异步提取所有元素。
        /// </summary>
        /// <returns></returns>
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
        public virtual Task<IQueryable<TEntity>> GetAllIncludeAsync(
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Task.FromResult(GetAllInclude(includeProperties));
        }

        /// <summary>
        ///     根据Id获取Dto对象。
        /// </summary>
        /// <typeparam name="TDto">数据传输对象类型。</typeparam>
        /// <param name="index">主键</param>
        /// <returns></returns>
        public virtual TDto GetSingleDtoById<TDto>(TKey index) where TDto : class, IDto<TEntity, TKey>
        {
            var entity = SingleById(index);
            return entity.MapToDto<TDto, TEntity, TKey>();
        }

        /// <summary>
        ///     异步根据Id获取Dto对象。
        /// </summary>
        /// <typeparam name="TDto">数据传输对象类型。</typeparam>
        /// <param name="index">主键</param>
        /// <returns></returns>
        public virtual Task<TDto> GetSingleDtoByIdAsync<TDto>(TKey index)
            where TDto : class, IDto<TEntity, TKey>
        {
            return Task.FromResult(GetSingleDtoById<TDto>(index));
        }

        /// <summary>
        ///     升序排序。
        /// </summary>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> OrderBy(string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            return GetAll().OrderBy(propertyName, sortDirection);
        }

        /// <summary>
        ///     异步升序排序。
        /// </summary>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public virtual Task<IQueryable<TEntity>> OrderByAsync(string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            return Task.FromResult(OrderBy(propertyName, sortDirection));
        }

        /// <summary>
        ///     降序排序。
        /// </summary>
        /// <param name="propertyName">排序属性名</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> OrderByDesc(string propertyName)
        {
            return OrderBy(propertyName, ListSortDirection.Descending);
        }

        /// <summary>
        ///     异步降序排序。
        /// </summary>
        /// <param name="propertyName">排序属性名</param>
        /// <returns></returns>
        public virtual Task<IQueryable<TEntity>> OrderByDescAsync(string propertyName)
        {
            return OrderByAsync(propertyName, ListSortDirection.Descending);
        }

        /// <summary>
        ///     根据谓词查询数据。
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        /// <summary>
        ///     异步根据谓词查询数据。
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Query(predicate));
        }

        /// <summary>
        ///     删除一个对象。
        /// </summary>
        /// <param name="index"></param>
        public virtual bool Remove(TKey index)
        {
            var entity = SingleById(index);
            return Remove(entity);
        }

        /// <summary>
        ///     删除一个对象
        /// </summary>
        /// <param name="entity"></param>
        public virtual bool Remove(TEntity entity)
        {
            _table.Remove(entity);
            return Save();
        }

        /// <summary>
        ///     删除一个对象。
        /// </summary>
        /// <param name="index"></param>
        public virtual Task<bool> RemoveAsync(TKey index)
        {
            var entity = SingleById(index);
            return RemoveAsync(entity);
        }

        /// <summary>
        ///     异步删除一个对象。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task<bool> RemoveAsync(TEntity entity)
        {
            _table.Remove(entity);
            return SaveAsync();
        }

        /// <summary>
        ///     移除指定的集合元素。
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public virtual bool RemoveRange(IEnumerable<TKey> range)
        {
            foreach (var index in range)
            {
                var entity = SingleById(index);
                if (entity != null)
                {
                    _table.Remove(entity);
                }
            }
            return Save();
        }

        /// <summary>
        ///     移除指定的集合元素。
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public virtual bool RemoveRange(IEnumerable<TEntity> range)
        {
            _table.RemoveRange(range);
            return Save();
        }

        /// <summary>
        ///     异步移除指定的集合元素。
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public virtual Task<bool> RemoveRangeAsync(IEnumerable<TKey> range)
        {
            foreach (var index in range)
            {
                var entity = SingleById(index);
                _table.Remove(entity);
            }
            return SaveAsync();
        }

        /// <summary>
        ///     异步移除指定的集合元素。
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public virtual Task<bool> RemoveRangeAsync(IEnumerable<TEntity> range)
        {
            _table.RemoveRange(range);
            return SaveAsync();
        }

        /// <summary>
        ///     数据持久化到数据库。
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        ///     采用异步的方式将数据持久化到数据库。
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        ///     将序列中的每个元素投影到新表单。
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual IQueryable<TResult> FilterField<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return GetAll().Select(selector);
        }

        /// <summary>
        ///     返回符合条件的第一个对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity SingleBy(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().SingleOrDefault(predicate);
        }

        /// <summary>
        ///     异步的返回符合条件的第一个对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual Task<TEntity> SingleByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().SingleOrDefaultAsync(predicate);
        }

        /// <summary>
        ///     查找带给定主键值的实体。
        ///     如果上下文中存在带给定主键值的实体，则立即返回该实体，而不会向存储区发送请求。
        ///     否则，会向存储区发送查找带给定主键值的实体的请求，如果找到该实体，则将其附加到上下文并返回。
        ///     如果未在上下文或存储区中找到实体，则返回 null。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual TEntity SingleById(TKey index)
        {
            return _table.Find(index);
        }

        /// <summary>
        ///     异步查找带给定主键值的实体。
        ///     如果上下文中存在带给定主键值的实体，则立即返回该实体，而不会向存储区发送请求。
        ///     否则，会向存储区发送查找带给定主键值的实体的请求，如果找到该实体，则将其附加到上下文并返回。
        ///     如果未在上下文或存储区中找到实体，则返回 null。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual Task<TEntity> SingleByIdAsync(TKey index)
        {
            return _table.FindAsync(index);
        }

        /// <summary>
        ///     获取一个对象。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual TEntity SingleById(params object[] index)
        {
            return _table.Find(index);
        }

        /// <summary>
        ///     异步获取一个对象。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual Task<TEntity> SingleByIdAsync(params object[] index)
        {
            return _table.FindAsync(index);
        }


        /// <summary>
        ///     创建一个原始 SQL 查询，该查询将返回此集中的实体。
        ///     默认情况下，上下文会跟踪返回的实体；可通过对返回的 DbRawSqlQuery 调用 AsNoTracking 来更改此设置。
        ///     请注意返回实体的类型始终是此集的类型，而不会是派生的类型。
        ///     如果查询的一个或多个表可能包含其他实体类型的数据，则必须编写适当的 SQL 查询以确保只返回适当类型的实体。
        ///     与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。
        ///     您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。
        ///     您提供的任何参数值都将自动转换为 DbParameter。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @p0",
        ///     userSuppliedAuthor);
        ///     或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。
        ///     context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author",
        ///     userSuppliedAuthor));
        /// </summary>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <param name="sql">SQL 查询字符串。</param>
        /// <param name="parameters">
        ///     要应用于 SQL 查询字符串的参数。 如果使用输出参数，则它们的值在完全读取结果之前不可用。 这是由于 DbDataReader 的基础行为而导致的，有关详细信息，请参见
        ///     http://go.microsoft.com/fwlink/?LinkID=398589。
        /// </param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> SqlQuery(string sql, bool trackEnabled = true, params object[] parameters)
        {
            return trackEnabled
                ? _table.SqlQuery(sql, parameters)
                : _table.SqlQuery(sql, parameters).AsNoTracking();
        }

        /// <summary>
        ///     异步的创建一个原始 SQL 查询，该查询将返回此集中的实体。
        ///     默认情况下，上下文会跟踪返回的实体；可通过对返回的 DbRawSqlQuery 调用 AsNoTracking 来更改此设置。
        ///     请注意返回实体的类型始终是此集的类型，而不会是派生的类型。
        ///     如果查询的一个或多个表可能包含其他实体类型的数据，则必须编写适当的 SQL 查询以确保只返回适当类型的实体。
        ///     与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。
        ///     您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。
        ///     您提供的任何参数值都将自动转换为 DbParameter。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @p0",
        ///     userSuppliedAuthor);
        ///     或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。
        ///     context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author",
        ///     userSuppliedAuthor));
        /// </summary>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <param name="sql">SQL 查询字符串。</param>
        /// <param name="parameters">
        ///     要应用于 SQL 查询字符串的参数。 如果使用输出参数，则它们的值在完全读取结果之前不可用。 这是由于 DbDataReader 的基础行为而导致的，有关详细信息，请参见
        ///     http://go.microsoft.com/fwlink/?LinkID=398589。
        /// </param>
        /// <returns></returns>
        public virtual Task<IEnumerable<TEntity>> SqlQueryAsync(string sql, bool trackEnabled = true,
            params object[] parameters)
        {
            return Task.FromResult(SqlQuery(sql, trackEnabled, parameters));
        }

        /// <summary>
        ///     通过Sql语句查找带给定主键值的实体。
        ///     如果找到该实体，则将其附加到上下文并返回。
        ///     可通过 trackEnabled 设置是否跟踪返回实体
        ///     如果未在上下文或存储区中找到实体，则返回 null。
        /// </summary>
        /// <param name="index">主键</param>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <returns></returns>
        public virtual TEntity SqlQuerySingle(TKey index, bool trackEnabled = true)
        {
            var tableName = GetTableName<TEntity>();
            var sql = "select * from " + tableName + " where [Id] = @index";
            var dbSqlQuery = _table.SqlQuery(sql, new SqlParameter("@index", index));
            if (trackEnabled)
            {
                dbSqlQuery = dbSqlQuery.AsNoTracking();
            }
            return dbSqlQuery.FirstOrDefault();
        }

        /// <summary>
        ///     通过Sql语句查找带给定主键值的实体。
        ///     如果找到该实体，则将其附加到上下文并返回。
        ///     可通过 trackEnabled 设置是否跟踪返回实体
        ///     如果未在上下文或存储区中找到实体，则返回 null。
        /// </summary>
        /// <param name="index">主键</param>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <returns></returns>
        public virtual Task<TEntity> SqlQuerySingleAsync(TKey index, bool trackEnabled = true)
        {
            return Task.FromResult(SqlQuerySingle(index, trackEnabled));
        }

        /// <summary>
        ///     创建一个原始 SQL 查询，该查询将返回此集中的实体。
        ///     默认情况下，上下文会跟踪返回的实体；可通过对返回的 DbRawSqlQuery 调用 AsNoTracking 来更改此设置。
        ///     请注意返回实体的类型始终是此集的类型，而不会是派生的类型。
        ///     如果查询的一个或多个表可能包含其他实体类型的数据，则必须编写适当的 SQL 查询以确保只返回适当类型的实体。
        ///     与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。
        ///     您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。
        ///     您提供的任何参数值都将自动转换为 DbParameter。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @p0",
        ///     userSuppliedAuthor);
        ///     或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。
        ///     context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author",
        ///     userSuppliedAuthor));
        /// </summary>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <param name="sql">SQL 查询字符串。</param>
        /// <param name="parameters">
        ///     要应用于 SQL 查询字符串的参数。 如果使用输出参数，则它们的值在完全读取结果之前不可用。 这是由于 DbDataReader 的基础行为而导致的，有关详细信息，请参见
        ///     http://go.microsoft.com/fwlink/?LinkID=398589。
        /// </param>
        /// <returns></returns>
        public virtual IEnumerable<T2> SqlQuery<T2, TKey2>(string sql, bool trackEnabled = true,
            params object[] parameters)
            where T2 : class, IEntity<TKey2>, new()
        {
            var table = _dbContext.Set<T2>();
            return trackEnabled
                ? table.SqlQuery(sql, parameters)
                : table.SqlQuery(sql, parameters).AsNoTracking();
        }

        /// <summary>
        ///     异步的创建一个原始 SQL 查询，该查询将返回此集中的实体。
        ///     默认情况下，上下文会跟踪返回的实体；可通过对返回的 DbRawSqlQuery 调用 AsNoTracking 来更改此设置。
        ///     请注意返回实体的类型始终是此集的类型，而不会是派生的类型。
        ///     如果查询的一个或多个表可能包含其他实体类型的数据，则必须编写适当的 SQL 查询以确保只返回适当类型的实体。
        ///     与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。
        ///     您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。
        ///     您提供的任何参数值都将自动转换为 DbParameter。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @p0",
        ///     userSuppliedAuthor);
        ///     或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。
        ///     context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author",
        ///     userSuppliedAuthor));
        /// </summary>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <param name="sql">SQL 查询字符串。</param>
        /// <param name="parameters">
        ///     要应用于 SQL 查询字符串的参数。 如果使用输出参数，则它们的值在完全读取结果之前不可用。 这是由于 DbDataReader 的基础行为而导致的，有关详细信息，请参见
        ///     http://go.microsoft.com/fwlink/?LinkID=398589。
        /// </param>
        /// <returns></returns>
        public virtual Task<IEnumerable<T2>> SqlQueryAsync<T2, TKey2>(string sql, bool trackEnabled,
            params object[] parameters)
            where T2 : class, IEntity<TKey2>, new()
        {
            return Task.FromResult(SqlQuery<T2, TKey2>(sql, trackEnabled, parameters));
        }

        /// <summary>
        ///     通过Sql语句查找带给定主键值的实体。
        ///     如果找到该实体，则将其附加到上下文并返回。
        ///     可通过 trackEnabled 设置是否跟踪返回实体
        ///     如果未在上下文或存储区中找到实体，则返回 null。
        /// </summary>
        /// <param name="index">主键</param>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <returns></returns>
        public virtual T2 SqlQuerySingle<T2, TKey2>(TKey index, bool trackEnabled)
            where T2 : class, IEntity<TKey2>, new()
        {
            var table = _dbContext.Set<T2>();

            var tableName = GetTableName<T2>();
            var sql = "select * from " + tableName + " where [Index] = @index";

            var dbSqlQuery = table.SqlQuery(sql, new SqlParameter("@index", index));
            if (trackEnabled)
            {
                dbSqlQuery = dbSqlQuery.AsNoTracking();
            }

            return dbSqlQuery.FirstOrDefault();
        }

        /// <summary>
        ///     通过Sql语句查找带给定主键值的实体。
        ///     如果找到该实体，则将其附加到上下文并返回。
        ///     可通过 trackEnabled 设置是否跟踪返回实体
        ///     如果未在上下文或存储区中找到实体，则返回 null。
        /// </summary>
        /// <param name="index">主键</param>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <returns></returns>
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
            var type = typeof(TType);
            var tableAttribute = type.GetAttribute<TableAttribute>();

            var tableName = tableAttribute != null
                ? tableAttribute.Name
                : GetEntityDataBaseTableName<TType>(_dbContext);

            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new InvalidOperationException($"无法获取类型：{type.FullName} 的数据库表名称。");
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
            var className = typeof(T).Name;

            var container = ojbectContext.MetadataWorkspace.GetItemCollection(DataSpace.SSpace)
                .GetItems<EntityContainer>().Single();
            var entityTableName = (from meta in container.BaseEntitySets.OfType<EntitySet>()
                where
                    (!meta.MetadataProperties.Contains("Type") ||
                     meta.MetadataProperties["Type"].ToString() == "Tables") &&
                    meta.ElementType.Name == className
                select meta.Table).FirstOrDefault();

            return entityTableName;
        }
    }
}