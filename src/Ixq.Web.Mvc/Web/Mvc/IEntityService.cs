using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Ixq.Core.Dto;
using Ixq.Core.Entity;
using Ixq.Core.Repository;
using Ixq.UI.ComponentModel;
using Ixq.UI.Controls;

namespace Ixq.Web.Mvc
{
    /// <summary>
    ///     实体服务接口。
    /// </summary>
    public interface IEntityService<TEntity, TDto, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TDto : class, IDto<TEntity, TKey>, new()
    {
        /// <summary>
        ///     获取HTTP上下文。
        /// </summary>
        HttpContextBase HttpContext { get; }
        /// <summary>
        ///     获取仓储。
        /// </summary>
        IRepositoryBase<TEntity, TKey> Repository { get; }
        /// <summary>
        ///     获取控制器基本数据。
        /// </summary>
         IEntityControllerData EntityControllerData { get; }
        /// <summary>
        ///     从仓储中提取默认的数据，默认直接提取 <see cref="Repository"/> 中所有数据。
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> EntityDefaultData();
        /// <summary>
        ///     从仓储中提取控制器 List Action 的数据，默认以 <see cref="EntityDefaultData"/>  作为数据源。
        /// </summary>
        /// <param name="orderField">排序字段。</param>
        /// <param name="orderDirection">排序方向，desc：降序排序。asc：升序排序。</param>
        /// <returns></returns>
        IQueryable<TEntity> EntityListData(string orderField, string orderDirection);
        /// <summary>
        ///     从仓储中提取控制器 Selector Action 的数据，默认以 <see cref="EntityDefaultData"/>  作为数据源。
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> EntitySelectorData();
        /// <summary>
        ///     创建一个<see cref="PageViewModel"/>实例。一般用于 Index Action。
        /// </summary>
        /// <param name="pagination">分页信息。</param>
        /// <returns></returns>
        PageViewModel CreateIndexModel(Pagination pagination);
        /// <summary>
        ///     创一个<see cref="PageEditViewModel{TDto, TKey}"/>实例。一般用于 Edit Action。
        /// </summary>
        /// <param name="id">实体主键。</param>
        /// <returns></returns>
        Task<PageEditViewModel<TDto, TKey>> CreateEditModelAsync(string id);
        /// <summary>
        ///     创一个<see cref="PageEditViewModel{TDto, TKey}"/>实例。一般用于 Edit Action。
        /// </summary>
        /// <param name="model">实体对象。</param>
        /// <returns></returns>
        Task<PageEditViewModel<TDto, TKey>> CreateEditModelAsync(TEntity model);
        /// <summary>
        ///     创一个<see cref="PageEditViewModel{TDto, TKey}"/>实例。一般用于 Edit Action。
        /// </summary>
        /// <param name="model">数据传输对象。</param>
        /// <returns></returns>
        Task<PageEditViewModel<TDto, TKey>> CreateEditModelAsync(TDto model);
        /// <summary>
        ///     创建一个<see cref="PageDataViewModel{TKey}"/>实例。一般用于 List Action。
        /// </summary>
        /// <param name="pageSize">页面大小。</param>
        /// <param name="pageCurrent">当前页。</param>
        /// <param name="orderField">排序字段。</param>
        /// <param name="orderDirection">排序方向。</param>
        /// <returns></returns>
        Task<PageDataViewModel<TKey>> CreateListModelAsync(int pageSize, int pageCurrent, string orderField,
            string orderDirection);
        /// <summary>
        ///     创建一个<see cref="PageEditViewModel{TDto, TKey}"/>实例。一般用于 Detail Action。
        /// </summary>
        /// <param name="id">实体主键。</param>
        /// <returns></returns>
        Task<PageEditViewModel<TDto, TKey>> CreateDetailModelAsync(string id);
        /// <summary>
        ///     更新实体。
        /// </summary>
        /// <param name="sourceEntity">源实体。</param>
        /// <returns></returns>
        Task<bool> UpdateEntity(TEntity sourceEntity);
        /// <summary>
        ///     更新属性。
        /// </summary>
        /// <param name="targetEntity">目标实体。</param>
        /// <param name="sourceEntity">源实体。</param>
        /// <param name="metadata">属性元数据。</param>
        /// <returns></returns>
        Task<bool> UpdateProperty(TEntity targetEntity, TEntity sourceEntity, IEntityPropertyMetadata metadata);
    }
}