using System;
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
using Ixq.UI.ComponentModel;
using Ixq.UI.Controls;

namespace Ixq.Web.Mvc
{
    public class EntityService<TEntity, TDto, TKey> : IEntityService<TEntity, TDto, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TDto : class, IDto<TEntity, TKey>, new()

    {

        public EntityService(IRepositoryBase<TEntity, TKey> repository, RequestContext requestContxt,
            IEntityControllerData entityControllerData)
        {
            if (entityControllerData == null)
                throw new ArgumentNullException(nameof(entityControllerData));
            if (requestContxt == null)
                throw new ArgumentNullException(nameof(requestContxt));
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            Repository = repository;
            EntityControllerData = entityControllerData;
            HttpContext = requestContxt.HttpContext;
        }

        public IRepositoryBase<TEntity, TKey> Repository { get; }
        public IEntityControllerData EntityControllerData { get; }

        public HttpContextBase HttpContext { get; }

        public virtual IQueryable<TEntity> Query()
        {
            return Repository.GetAll();
        }

        public virtual IQueryable<TEntity> QueryForList(string orderField, string orderDirection)
        {
            return orderDirection.Equals("asc")
                ? Query().OrderBy(orderField)
                : Query().OrderBy(orderField, ListSortDirection.Descending);
        }

        public virtual IQueryable<TEntity> QueryForSelector()
        {
            return Query();
        }

        public virtual PageViewModel CreateIndexModel(Pagination pagination)
        {
            var viewModel = new PageViewModel
            {
                EntityMetadata = EntityControllerData.EntityMetadata,
                EntityType = typeof(TEntity),
                DtoType = typeof(TDto),
                PageConfig = EntityControllerData.PageConfig,
                Pagination = pagination
            };
            return viewModel;
        }

        public virtual async Task<PageEditViewModel<TDto, TKey>> CreateEditModelAsync(string id)
        {
            var entity = string.IsNullOrWhiteSpace(id)
                ? Repository.Create()
                : await Repository.SingleByIdAsync(ParseEntityKey(id));
            return await CreateEditModelAsync(entity);
        }
        public virtual Task<PageEditViewModel<TDto, TKey>> CreateEditModelAsync(TEntity model)
        {
            return CreateEditModelAsync(model.MapToDto<TDto, TKey>());
        }

        public virtual async Task<PageEditViewModel<TDto, TKey>> CreateEditModelAsync(TDto model)
        {
            var editModel = new PageEditViewModel<TDto, TKey>(model,
                            EntityControllerData.EntityMetadata.EditPropertyMetadatas)
            {
                Title =
                    ((await Repository.SingleByIdAsync(model.Id)) == null ? "新增" : "编辑") +
                    (EntityControllerData.PageConfig.Title ?? typeof(TEntity).Name)
            };

            return editModel;
        }

        public virtual async Task<PageEditViewModel<TDto, TKey>> CreateDetailModelAsync(string id)
        {
            var entity = await Repository.SingleByIdAsync(ParseEntityKey(id));
            if (entity == null)
                throw new HttpException(404, null);

            var detailModel = new PageEditViewModel<TDto, TKey>(entity.MapToDto<TDto, TKey>(),
                EntityControllerData.EntityMetadata.DetailPropertyMetadatas)
            {
                Title = ""
            };
            return detailModel;
        }

        public virtual async Task<PageDataViewModel<TKey>> CreateListModelAsync(int pageSize, int pageCurrent,
            string orderField, string orderDirection)
        {
            var queryable = QueryForList(orderField, orderDirection);
            var pageListViewModel = new PageDataViewModel<TKey>(queryable.Count(), pageCurrent, pageSize)
            {
                Items = await queryable
                    .Skip((pageCurrent - 1) * pageSize)
                    .Take(pageSize)
                    .ToDtoListAsync<TDto, TEntity, TKey>()
            };
            return pageListViewModel;
        }

        public virtual async Task<bool> UpdateEntity(TEntity sourceEntity)
        {
            var addAction = false;
            var targetEntity = await Repository.SingleByIdAsync(sourceEntity.Id);
            if (targetEntity == null)
            {
                targetEntity = Repository.Create();
                addAction = true;
            }

            var editPropertyMetadata = EntityControllerData.EntityMetadata.EditPropertyMetadatas;
            foreach (var metadata in editPropertyMetadata)
            {
                await UpdateProperty(targetEntity, sourceEntity, metadata);
            }

            if (addAction)
            {
                await Repository.AddAsync(sourceEntity);
            }
            else
            {
                await Repository.EditAsync(targetEntity);
            }
            return true;
        }

        public virtual Task<bool> UpdateProperty(TEntity targetEntity, TEntity sourceEntity,
            IEntityPropertyMetadata metadata)
        {
            var entityProperty = typeof(TEntity).GetProperty(metadata.PropertyName);
            entityProperty.SetValue(targetEntity, entityProperty.GetValue(sourceEntity));
            return Task.FromResult(true);
        }

        /// <summary>
        ///     转换主键类型。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual object ParseEntityKey(string value)
        {
            return RepositoryExtensions.ParseEntityKey<TKey>(value);
        }
    }
}