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
        public EntityUpdater(IRepositoryBase<TEntity, TKey> repository, IEntityControllerData entityControllerData)
        {
            if (entityControllerData == null)
                throw new ArgumentNullException(nameof(entityControllerData));
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            this.Repository = repository;
            this.EntityControllerData = entityControllerData;
        }

        public IRepositoryBase<TEntity, TKey> Repository { get; }
        public IEntityControllerData EntityControllerData { get; }
        public virtual IQueryable<TEntity> Query()
        {
            return Repository.GetAll();
        }

        public virtual PageViewModel CreatePageViewModel()
        {
            var viewModel = new PageViewModel
            {
                EntityMetadata = EntityControllerData.EntityMetadata,
                EntityType = typeof (TEntity),
                DtoType = typeof (TDto)
            };

            return viewModel;
        }

        public virtual PageEditViewModel<TDto, TKey> CreatePageEditViewModel()
        {
            throw new NotImplementedException();
        }

        public virtual PageDataViewModel<TKey> CreatePageDataViewModel()
        {
            throw new NotImplementedException();
        }
    }
}
