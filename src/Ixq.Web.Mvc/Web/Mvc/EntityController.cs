using System;
using System.Web.Mvc;
using Ixq.Core.Dto;
using Ixq.Core.Entity;
using Ixq.Core.Repository;

namespace Ixq.Web.Mvc
{
    public class EntityController<TEntity, TDto> : BaseController
        where TEntity : class, IEntity<Guid>, new()
        where TDto : class, IDto<TEntity, Guid>, new()
    {
        public readonly IRepository<TEntity> Repository;

        protected EntityController(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult List()
        {
            return View();
        }

        public virtual ActionResult Edit()
        {
            return View();
        }

        public virtual ActionResult Delete()
        {
            return View();
        }

        public virtual ActionResult Searchers()
        {
            return View();
        }

        public virtual ActionResult Selector()
        {
            return View();
        }

        public virtual ActionResult MultipleSelector()
        {
            return View();
        }
    }
}