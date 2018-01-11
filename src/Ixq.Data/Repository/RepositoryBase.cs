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
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _table;

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
        public virtual void Add(TEntity entity)
        {
            _table.Add(entity);
        }

        /// <summary>
        ///     添加一个集合中的数据。
        /// </summary>
        /// <param name="entities"></param>
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _table.AddRange(entities);
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
        public virtual void Edit(TEntity entity)
        {
            var entry = _dbContext.Entry(entity);
            entry.State = EntityState.Modified;
        }

        /// <summary>
        ///     提取所有元素。
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAll()
        {
            return _table.AsNoTracking();
        }

        public IQueryable<T2> GetAll<T2, TKey2>() where T2 : class, IEntity<TKey2>, new()
        {
            return _dbContext.Set<T2>();
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
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TKey2"></typeparam>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IQueryable<T2> GetAllInclude<T2, TKey2>(params Expression<Func<T2, object>>[] includeProperties)
            where T2 : class, IEntity<TKey2>, new()
        {
            var res = GetAll<T2, TKey2>();
            foreach (var includePropertie in includeProperties)
            {
                res = res.Include(includePropertie);
            }
            return res;
        }

        /// <summary>
        ///     对数据库异步执行给定的 DDL/DML 命令。与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。您可以在
        ///     SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。您提供的任何参数值都将自动转换为 DbParameter。context.Database.ExecuteSqlCommandAsync("UPDATE
        ///     dbo.Posts SET Rating = 5 WHERE Author = @p0", userSuppliedAuthor); 或者，您还可以构造一个
        ///     DbParameter 并将它提供给 SqlQuery。这允许您在 SQL 查询字符串中使用命名参数。context.Database.ExecuteSqlCommandAsync("UPDATE
        ///     dbo.Posts SET Rating = 5 WHERE Author = @author", new SqlParameter("@author",
        ///     userSuppliedAuthor));
        /// </summary>
        /// <param name="sql">命令字符串。</param>
        /// <param name="parameters">要应用于命令字符串的参数。</param>
        /// <returns></returns>
        public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return _dbContext.Database.ExecuteSqlCommandAsync(sql, parameters);
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
        ///     降序排序。
        /// </summary>
        /// <param name="propertyName">排序属性名</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> OrderByDesc(string propertyName)
        {
            return OrderBy(propertyName, ListSortDirection.Descending);
        }

        /// <summary>
        ///     删除一个对象。
        /// </summary>
        /// <param name="index"></param>
        public virtual void Remove(TKey index)
        {
            var entity = SingleById(index);
            Remove(entity);
        }

        /// <summary>
        ///     删除一个对象
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Remove(TEntity entity)
        {
            _table.Remove(entity);
        }

        /// <summary>
        ///     删除一个对象。
        /// </summary>
        /// <param name="index"></param>
        public virtual async Task RemoveAsync(TKey index)
        {
            var entity = await SingleByIdAsync(index);
            _table.Remove(entity);
        }

        /// <summary>
        ///     移除指定的集合元素。
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public virtual void RemoveRange(IEnumerable<TKey> range)
        {
            foreach (var index in range)
            {
                var entity = SingleById(index);
                if (entity != null)
                {
                    _table.Remove(entity);
                }
            }
        }

        /// <summary>
        ///     移除指定的集合元素。
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public virtual void RemoveRange(IEnumerable<TEntity> range)
        {
            _table.RemoveRange(range);
        }

        /// <summary>
        ///     异步移除指定的集合元素。
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public virtual async Task RemoveRangeAsync(IEnumerable<TKey> range)
        {
            foreach (var index in range)
            {
                var entity = await SingleByIdAsync(index);
                _table.Remove(entity);
            }
        }

        /// <summary>
        ///     数据持久化到数据库。
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {
            return UnitOfWork.Save();
        }

        /// <summary>
        ///     采用异步的方式将数据持久化到数据库。
        /// </summary>
        /// <returns></returns>
        public virtual Task<int> SaveAsync()
        {
            return UnitOfWork.SaveAsync();
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
        ///     返回符合条件的第一个对象。
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TKey2"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T2 SingleBy<T2, TKey2>(Expression<Func<T2, bool>> predicate) where T2 : class, IEntity<TKey2>, new()
        {
            var table = _dbContext.Set<T2>();
            return table.SingleOrDefault(predicate);
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
        ///     异步的返回符合条件的第一个对象。
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TKey2"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<T2> SingleByAsync<T2, TKey2>(Expression<Func<T2, bool>> predicate)
            where T2 : class, IEntity<TKey2>, new()
        {
            var table = _dbContext.Set<T2>();
            return table.SingleOrDefaultAsync(predicate);
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
        ///     创建一个原始 SQL 查询，该查询将返回给定泛型类型的元素。类型可以是包含与从查询返回的列名匹配的属性的任何类型，也可以是简单的基元类型。该类型不必是实体类型。即使返回对象的类型是实体类型，上下文也决不会跟踪此查询的结果。使用
        ///     System.Data.Entity.DbSet`1.SqlQuery(System.String,System.Object[]) 方法可返回上下文跟踪的实体。与接受
        ///     SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。您提供的任何参数值都将自动转换为
        ///     DbParameter。context.Database.SqlQuery&lt;Post&gt;("SELECT * FROM dbo.Posts WHERE
        ///     Author = @p0", userSuppliedAuthor); 或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。这允许您在
        ///     SQL 查询字符串中使用命名参数。context.Database.SqlQuery&lt;Post&gt;("SELECT * FROM dbo.Posts
        ///     WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="sql">命令字符串。</param>
        /// <param name="parameters">要应用于命令字符串的参数。</param>
        /// <returns></returns>
        public IEnumerable<TElement> DbSqlQuery<TElement>(string sql, params object[] parameters)
        {
            return _dbContext.Database.SqlQuery<TElement>(sql, parameters);
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