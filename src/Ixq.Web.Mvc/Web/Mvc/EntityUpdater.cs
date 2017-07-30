using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Dto;
using Ixq.Core.Entity;
using Ixq.Core.Repository;
using Ixq.UI.ComponentModel;

namespace Ixq.Web.Mvc
{
    public class EntityUpdater<TEntity, TDto, TKey> : IEntityUpdater<TEntity, TDto, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TDto : class, IDto<TEntity, TKey>, new()

    {
        public EntityUpdater(IEntityMetadata metadata, IRepositoryBase<TEntity, TKey> repository, IEntityControllerData entityControllerData)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            this.EntityMetadata = metadata;
            this.Repository = repository;
            this.EntityControllerData = entityControllerData;
        }

        public IRepositoryBase<TEntity, TKey> Repository { get; }
        public IEntityMetadata EntityMetadata { get; }
        public IEntityControllerData EntityControllerData { get; }
        public virtual IQueryable<TEntity> Query()
        {
            return Repository.GetAll();
        }

        public PageViewModel CreatePageViewModel()
        {
            var viewModel = new PageViewModel
            {
                EntityMetadata = EntityMetadata,
                EntityType = typeof (TEntity),
                DtoType = typeof (TDto)
            };

            return viewModel;
        }

        public PageEditViewModel<TDto, TKey> CreatePageEditViewModel()
        {
            throw new NotImplementedException();
        }

        public PageDataViewModel<TKey> CreatePageDataViewModel()
        {
            throw new NotImplementedException();
        }
    }
}
