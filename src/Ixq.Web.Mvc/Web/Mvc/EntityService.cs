using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using Ixq.Core.Dto;
using Ixq.Core.Entity;
using Ixq.Core.Mapper;
using Ixq.Core.Repository;
using Ixq.Data.Repository.Extensions;
using Ixq.Extensions;
using Ixq.UI.ComponentModel;
using Ixq.UI.Controls;

namespace Ixq.Web.Mvc
{
    /// <summary>
    ///     默认的实体服务。
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class EntityService<TEntity, TDto, TKey> : IEntityService<TEntity, TDto, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TDto : class, IDto<TEntity, TKey>, new()

    {
        /// <summary>
        ///     初始化一个<see cref="EntityService{TEntity, TDto, TKey}" />实例。
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="requestContxt"></param>
        /// <param name="entityControllerData"></param>
        public EntityService(IRepositoryBase<TEntity, TKey> repository, RequestContext requestContxt,
            IEntityControllerDescriptor entityControllerData)
        {
            if (entityControllerData == null)
                throw new ArgumentNullException(nameof(entityControllerData));
            if (requestContxt == null)
                throw new ArgumentNullException(nameof(requestContxt));
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            Repository = repository;
            EntityControllerDescriptor = entityControllerData;
            RequestContext = requestContxt;
        }

        /// <summary>
        ///     获取仓储。
        /// </summary>
        public IRepositoryBase<TEntity, TKey> Repository { get; }

        /// <summary>
        ///     获取控制器基本信息。
        /// </summary>
        public IEntityControllerDescriptor EntityControllerDescriptor { get; }

        /// <summary>
        ///     获取HTTP请求上下文。
        /// </summary>
        public RequestContext RequestContext { get; }

        /// <summary>
        ///     从仓储中提取默认的数据，默认直接提取 <see cref="Repository" /> 中所有数据。
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetEntityData()
        {
            return Repository.GetAll();
        }

        /// <summary>
        ///     从仓储中提取控制器 List Action 的数据，默认以 <see cref="GetEntityData" />  作为数据源。
        /// </summary>
        /// <param name="orderField">排序字段。</param>
        /// <param name="orderDirection">排序方向，desc：降序排序。asc：升序排序。</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetEntityListData(string orderField, string orderDirection)
        {
            return orderDirection.Equals("asc")
                ? GetEntityData().OrderBy(orderField)
                : GetEntityData().OrderBy(orderField, ListSortDirection.Descending);
        }

        /// <summary>
        ///     从仓储中提取控制器 Selector Action 的数据，默认以 <see cref="GetEntityData" />  作为数据源。
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetEntitySelectorData()
        {
            return GetEntityData();
        }

        /// <summary>
        ///     创建一个<see cref="PageViewModel" />实例。一般用于 Index Action。
        /// </summary>
        /// <param name="pagination">分页信息。</param>
        /// <returns></returns>
        public virtual PageViewModel CreateIndexModel(Pagination pagination)
        {
            var viewModel = new PageViewModel
            {
                EntityMetadata = EntityControllerDescriptor.EntityMetadata,
                EntityType = typeof(TEntity),
                DtoType = typeof(TDto),
                PageConfig = EntityControllerDescriptor.PageConfig,
                Pagination = pagination
            };
            return viewModel;
        }

        /// <summary>
        ///     创一个<see cref="PageEditViewModel{TDto, TKey}" />实例。一般用于 Edit Action。
        /// </summary>
        /// <param name="id">实体主键。</param>
        /// <returns></returns>
        public virtual async Task<PageEditViewModel<TDto, TKey>> CreateEditModelAsync(string id)
        {
            var entity = string.IsNullOrWhiteSpace(id)
                ? Repository.Create()
                : await Repository.SingleByIdAsync(ParseEntityKey(id));
            return await CreateEditModelAsync(entity);
        }

        /// <summary>
        ///     创一个<see cref="PageEditViewModel{TDto, TKey}" />实例。一般用于 Edit Action。
        /// </summary>
        /// <param name="model">实体对象。</param>
        /// <returns></returns>
        public virtual Task<PageEditViewModel<TDto, TKey>> CreateEditModelAsync(TEntity model)
        {
            return CreateEditModelAsync(model.MapToDto<TDto, TKey>());
        }

        /// <summary>
        ///     创一个<see cref="PageEditViewModel{TDto, TKey}" />实例。一般用于 Edit Action。
        /// </summary>
        /// <param name="model">数据传输对象。</param>
        /// <returns></returns>
        public virtual async Task<PageEditViewModel<TDto, TKey>> CreateEditModelAsync(TDto model)
        {
            var editModel = new PageEditViewModel<TDto, TKey>(model,
                EntityControllerDescriptor.EntityMetadata.EditPropertyMetadatas)
            {
                Title =
                    (await Repository.SingleByIdAsync(model.Id) == null ? "新增" : "编辑") +
                    (EntityControllerDescriptor.PageConfig.Title ?? typeof(TEntity).Name)
            };

            return editModel;
        }

        /// <summary>
        ///     创建一个<see cref="PageEditViewModel{TDto, TKey}" />实例。一般用于 Detail Action。
        /// </summary>
        /// <param name="id">实体主键。</param>
        /// <returns></returns>
        public virtual async Task<PageEditViewModel<TDto, TKey>> CreateDetailModelAsync(string id)
        {
            var entity = await Repository.SingleByIdAsync(ParseEntityKey(id));
            if (entity == null)
                throw new HttpException(404, null);

            var detailModel = new PageEditViewModel<TDto, TKey>(entity.MapToDto<TDto, TKey>(),
                EntityControllerDescriptor.EntityMetadata.DetailPropertyMetadatas)
            {
                Title = ""
            };
            return detailModel;
        }

        /// <summary>
        ///     创建一个<see cref="PageDataViewModel{TKey}" />实例。一般用于 List Action。
        /// </summary>
        /// <param name="pageSize">页面大小。</param>
        /// <param name="pageCurrent">当前页。</param>
        /// <param name="orderField">排序字段。</param>
        /// <param name="orderDirection">排序方向。</param>
        /// <returns></returns>
        public virtual async Task<PageDataViewModel<TKey>> CreateListModelAsync(int pageSize, int pageCurrent,
            string orderField, string orderDirection)
        {
            var queryable = GetEntityListData(orderField, orderDirection);
            var pageListViewModel = new PageDataViewModel<TKey>(queryable.Count(), pageCurrent, pageSize)
            {
                Items = await queryable
                    .Skip((pageCurrent - 1) * pageSize)
                    .Take(pageSize)
                    .ToDtoListAsync<TDto, TEntity, TKey>()
            };
            return pageListViewModel;
        }

        /// <summary>
        ///     更新实体。
        /// </summary>
        /// <param name="sourceEntity">源实体。</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateEntity(TEntity sourceEntity)
        {
            var addAction = false;
            var targetEntity = await Repository.SingleByIdAsync(sourceEntity.Id);
            if (targetEntity == null)
            {
                targetEntity = Repository.Create();
                addAction = true;
            }

            var editPropertyMetadata = EntityControllerDescriptor.EntityMetadata.EditPropertyMetadatas;
            foreach (var metadata in editPropertyMetadata)
                await UpdateProperty(targetEntity, sourceEntity, metadata);

            if (addAction)
                Repository.Add(sourceEntity);
            else
                Repository.Edit(targetEntity);

            await Repository.SaveAsync();
            return true;
        }

        /// <summary>
        ///     更新属性。
        /// </summary>
        /// <param name="targetEntity">目标实体。</param>
        /// <param name="sourceEntity">源实体。</param>
        /// <param name="metadata">属性元数据。</param>
        /// <returns></returns>
        public virtual Task<bool> UpdateProperty(TEntity targetEntity, TEntity sourceEntity,
            IEntityPropertyMetadata metadata)
        {
            var entityProperty = typeof(TEntity).GetProperty(metadata.PropertyName);
            if (entityProperty != null) entityProperty.SetValue(targetEntity, entityProperty.GetValue(sourceEntity));
            return Task.FromResult(true);
        }

        /// <summary>
        ///     移除指定的集合元素。
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public virtual async Task<bool> RemoveRange(IEnumerable<TKey> range)
        {
            this.Repository.RemoveRange(range);
            var count = await Repository.SaveAsync();
            return count > 0;
        }

        /// <summary>
        ///     转换主键类型。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static object ParseEntityKey(string value)
        {
            return TypeExtensions.ChangeType(value, typeof(TKey));
        }
    }
}