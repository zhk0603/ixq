using Ixq.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Repository
{
    /// <summary>
    ///     仓储接口，如果要查询被软删除的数据可通过 SqlQuery 开头的Api 查询，其他api 不能查询到这些数据。
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepositoryBase<TEntity, TKey>  
        where TEntity : class, IEntity<TKey>, new() 
        where TKey : struct
    {
        /// <summary>
        ///     工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        ///     根据上下文创建一个对象，并执行对象的 OnCreate 方法；
        /// </summary>
        /// <returns>创建好的对象</returns>
        TEntity Create();
        /// <summary>
        /// 查找带给定主键值的实体。
        /// 如果上下文中存在带给定主键值的实体，则立即返回该实体，而不会向存储区发送请求。
        /// 否则，会向存储区发送查找带给定主键值的实体的请求，如果找到该实体，则将其附加到上下文并返回。
        /// 如果未在上下文或存储区中找到实体，则返回 null。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        TEntity SingleById(TKey index);
        /// <summary>
        /// 异步查找带给定主键值的实体。
        /// 如果上下文中存在带给定主键值的实体，则立即返回该实体，而不会向存储区发送请求。
        /// 否则，会向存储区发送查找带给定主键值的实体的请求，如果找到该实体，则将其附加到上下文并返回。
        /// 如果未在上下文或存储区中找到实体，则返回 null。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Task<TEntity> SingleByIdAsync(TKey index);

        /// <summary>
        ///     返回符合条件的第一个对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity SingleBy(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     异步的返回符合条件的第一个对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> SingleByAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     添加一个对象
        /// </summary>
        /// <param name="entity"></param>
        bool Add(TEntity entity);

        /// <summary>
        ///     异步添加一个对象
        /// </summary>
        /// <param name="entity"></param>
        Task<bool> AddAsync(TEntity entity);

        /// <summary>
        ///     添加一个集合中的数据
        /// </summary>
        /// <param name="entities"></param>
        bool AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        ///     异步添加一个集合中的数据
        /// </summary>
        /// <param name="entities"></param>
        Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        ///     编辑一个对象
        /// </summary>
        /// <param name="entity"></param>
        bool Edit(TEntity entity);

        /// <summary>
        ///     异步编辑一个对象
        /// </summary>
        /// <param name="entity"></param>
        Task<bool> EditAsync(TEntity entity);

        /// <summary>
        ///     删除一个对象
        /// </summary>
        /// <param name="entity"></param>
        bool Remove(TEntity entity);

        Task<bool> RemoveAsync(TEntity entity);

        /// <summary>
        ///     删除一个对象
        /// </summary>
        /// <param name="index"></param>
        bool Remove(TKey index);

        /// <summary>
        ///     删除一个对象
        /// </summary>
        /// <param name="index"></param>
        Task<bool> RemoveAsync(TKey index);
        /// <summary>
        /// 移除指定的集合元素。
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        bool RemoveRange(IEnumerable<TEntity> range);
        /// <summary>
        /// 异步移除指定的集合元素。
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        Task<bool> RemoveRangeAsync(IEnumerable<TEntity> range);
        /// <summary>
        /// 移除指定的集合元素。
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        bool RemoveRange(IEnumerable<TKey> range);
        /// <summary>
        /// 异步移除指定的集合元素。
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        Task<bool> RemoveRangeAsync(IEnumerable<TKey> range);
        /// <summary>
        ///     提取所有元素
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        ///     异步提取所有元素
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetAllAsync();

        /// <summary>
        ///     升序排序
        /// </summary>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        IQueryable<TEntity> OrderBy(string propertyName, ListSortDirection sortDirection = ListSortDirection.Ascending);

        /// <summary>
        ///     异步升序排序
        /// </summary>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> OrderByAsync(string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending);

        /// <summary>
        ///     降序排序
        /// </summary>
        /// <param name="propertyName">排序属性名</param>
        /// <returns></returns>
        IQueryable<TEntity> OrderByDesc(string propertyName);

        /// <summary>
        ///     异步降序排序
        /// </summary>
        /// <param name="propertyName">排序属性名</param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> OrderByDescAsync(string propertyName);

        /// <summary>
        ///     根据谓词查询数据；
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     异步根据谓词查询数据；
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     数据持久化到数据库
        /// </summary>
        /// <returns></returns>
        bool Save();

        /// <summary>
        ///     采用异步的方式将数据持久化到数据库
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAsync();

        #region Sql查询
        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回此集中的实体。 
        /// 默认情况下，上下文会跟踪返回的实体；可通过对返回的 DbRawSqlQuery 调用 AsNoTracking 来更改此设置。 
        /// 请注意返回实体的类型始终是此集的类型，而不会是派生的类型。 
        /// 如果查询的一个或多个表可能包含其他实体类型的数据，则必须编写适当的 SQL 查询以确保只返回适当类型的实体。 
        /// 与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。 
        /// 您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。 
        /// 您提供的任何参数值都将自动转换为 DbParameter。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @p0", userSuppliedAuthor); 
        /// 或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。 
        /// context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        /// </summary>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <param name="sql">SQL 查询字符串。</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数。 如果使用输出参数，则它们的值在完全读取结果之前不可用。 这是由于 DbDataReader 的基础行为而导致的，有关详细信息，请参见 http://go.microsoft.com/fwlink/?LinkID=398589。</param>
        /// <returns></returns>
        IEnumerable<TEntity> SqlQuery(string sql, bool trackEnabled = true, params object[] parameters);

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回此集中的实体。 
        /// 默认情况下，上下文会跟踪返回的实体；可通过对返回的 DbRawSqlQuery 调用 AsNoTracking 来更改此设置。 
        /// 请注意返回实体的类型始终是此集的类型，而不会是派生的类型。 
        /// 如果查询的一个或多个表可能包含其他实体类型的数据，则必须编写适当的 SQL 查询以确保只返回适当类型的实体。 
        /// 与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。 
        /// 您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。 
        /// 您提供的任何参数值都将自动转换为 DbParameter。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @p0", userSuppliedAuthor); 
        /// 或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。 
        /// context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        /// </summary>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <param name="sql">SQL 查询字符串。</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数。 如果使用输出参数，则它们的值在完全读取结果之前不可用。 这是由于 DbDataReader 的基础行为而导致的，有关详细信息，请参见 http://go.microsoft.com/fwlink/?LinkID=398589。</param>
        /// <returns></returns>
        IEnumerable<T2> SqlQuery<T2, TKey2>(string sql, bool trackEnabled = true, params object[] parameters)
            where T2 : class, IEntity<TKey2>, new() 
            where TKey2 : struct;

        /// <summary>
        /// 通过Sql语句查找带给定主键值的实体。
        /// 如果找到该实体，则将其附加到上下文并返回。
        /// 可通过 trackEnabled 设置是否跟踪返回实体
        /// 如果未在上下文或存储区中找到实体，则返回 null。
        /// </summary>
        /// <param name="index">主键</param>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <returns></returns>
        TEntity SqlQuerySingle(TKey index, bool trackEnabled = true);
        /// <summary>
        /// 通过Sql语句查找带给定主键值的实体。
        /// 如果找到该实体，则将其附加到上下文并返回。
        /// 可通过 trackEnabled 设置是否跟踪返回实体
        /// 如果未在上下文或存储区中找到实体，则返回 null。
        /// </summary>
        /// <param name="index">主键</param>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <returns></returns>
        T2 SqlQuerySingle<T2, TKey2>(TKey index, bool trackEnabled = true) 
            where T2 : class, IEntity<TKey2>, new() 
            where TKey2 : struct;
        /// <summary>
        /// 异步的创建一个原始 SQL 查询，该查询将返回此集中的实体。 
        /// 默认情况下，上下文会跟踪返回的实体；可通过对返回的 DbRawSqlQuery 调用 AsNoTracking 来更改此设置。 
        /// 请注意返回实体的类型始终是此集的类型，而不会是派生的类型。 
        /// 如果查询的一个或多个表可能包含其他实体类型的数据，则必须编写适当的 SQL 查询以确保只返回适当类型的实体。 
        /// 与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。 
        /// 您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。 
        /// 您提供的任何参数值都将自动转换为 DbParameter。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @p0", userSuppliedAuthor); 
        /// 或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。 
        /// context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        /// </summary>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <param name="sql">SQL 查询字符串。</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数。 如果使用输出参数，则它们的值在完全读取结果之前不可用。 这是由于 DbDataReader 的基础行为而导致的，有关详细信息，请参见 http://go.microsoft.com/fwlink/?LinkID=398589。</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> SqlQueryAsync(string sql, bool trackEnabled = true, params object[] parameters);

        /// <summary>
        /// 异步的创建一个原始 SQL 查询，该查询将返回此集中的实体。 
        /// 默认情况下，上下文会跟踪返回的实体；可通过对返回的 DbRawSqlQuery 调用 AsNoTracking 来更改此设置。 
        /// 请注意返回实体的类型始终是此集的类型，而不会是派生的类型。 
        /// 如果查询的一个或多个表可能包含其他实体类型的数据，则必须编写适当的 SQL 查询以确保只返回适当类型的实体。 
        /// 与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。 
        /// 您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。 
        /// 您提供的任何参数值都将自动转换为 DbParameter。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @p0", userSuppliedAuthor); 
        /// 或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。 
        /// context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        /// </summary>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <param name="sql">SQL 查询字符串。</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数。 如果使用输出参数，则它们的值在完全读取结果之前不可用。 这是由于 DbDataReader 的基础行为而导致的，有关详细信息，请参见 http://go.microsoft.com/fwlink/?LinkID=398589。</param>
        /// <returns></returns>
        Task<IEnumerable<T2>> SqlQueryAsync<T2, TKey2>(string sql, bool trackEnabled = true, params object[] parameters)
            where T2 : class, IEntity<TKey2>, new() 
            where TKey2 : struct;
        /// <summary>
        /// 通过Sql语句查找带给定主键值的实体。
        /// 如果找到该实体，则将其附加到上下文并返回。
        /// 可通过 trackEnabled 设置是否跟踪返回实体
        /// 如果未在上下文或存储区中找到实体，则返回 null。
        /// </summary>
        /// <param name="index">主键</param>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <returns></returns>
        Task<TEntity> SqlQuerySingleAsync(TKey index, bool trackEnabled = true);
        /// <summary>
        /// 通过Sql语句查找带给定主键值的实体。
        /// 如果找到该实体，则将其附加到上下文并返回。
        /// 可通过 trackEnabled 设置是否跟踪返回实体
        /// 如果未在上下文或存储区中找到实体，则返回 null。
        /// </summary>
        /// <param name="index">主键</param>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <returns></returns>
        Task<T2> SqlQuerySingleAsync<T2, TKey2>(TKey index, bool trackEnabled = true) where T2 : class, IEntity<TKey2>, new() 
            where TKey2 : struct;
        #endregion

        #region Dto
        /// <summary>
        /// 根据Id获取Dto对象
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        TDto GetSingleDtoById<TDto>(TKey index);
        /// <summary>
        /// 异步根据Id获取Dto对象
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        TDto GetSingleDtoByIdAsync<TDto>(TKey index);
        #endregion
    }
}
