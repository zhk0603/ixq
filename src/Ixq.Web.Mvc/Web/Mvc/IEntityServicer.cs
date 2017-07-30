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
    ///     实体服务商。
    /// </summary>
    public interface IEntityServicer<TEntity, TDto, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TDto : class, IDto<TEntity, TKey>, new()
    {
        HttpContextBase HttpContext { get; }
        IRepositoryBase<TEntity, TKey> Repository { get; }
        IEntityControllerData EntityControllerData { get; }
        IQueryable<TEntity> Query();
        IQueryable<TEntity> QueryForList(string orderField, string orderDirection);
        IQueryable<TEntity> QueryForSelector();
        PageViewModel CreateIndexModel(Pagination pagination);
        Task<PageEditViewModel<TDto, TKey>> CreateEditModelAsync(string id);

        Task<PageDataViewModel<TKey>> CreateListModelAsync(int pageSize, int pageCurrent, string orderField,
            string orderDirection);

        Task<PageEditViewModel<TDto, TKey>> CreateDetailModelAsync(string id);
        Task<bool> UpdateEntity(TEntity sourceEntity);
        Task<bool> UpdateProperty(TEntity targetEntity, TEntity sourceEntity, IEntityPropertyMetadata metadata);
    }
}