using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ixq.Core.Dto;
using Ixq.Core.Entity;
using Ixq.Core.Repository;
using Ixq.Extensions;
using Ixq.UI.ComponentModel.DataAnnotations;
using Ixq.UI.Controls;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Ixq.Data.DataAnnotations;
using System.Web.Routing;
using Ixq.UI;

namespace Ixq.Web.Mvc
{
    public abstract class EntityController<TEntity, TDto> : BaseController
        where TEntity : class, IEntity<Guid>, new()
        where TDto : class, IDto<TEntity, Guid>, new()
    {
        public readonly IRepository<TEntity> Repository;
        public int[] SelectPageSize { get; set; }
        public IDatagridConfig DatagridConfig { get; set; }
        public RuntimeEntityMenberInfo RuntimeEntityMenberInfo { get; set; }

        protected EntityController(IRepository<TEntity> repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            SelectPageSize = new[] { 30, 60, 120, 150 };
            DatagridConfig = typeof(TDto).GetAttribute<DatagridAttribute>() ??
                                new DatagridAttribute();
            Repository = repository;
        }

        public virtual async Task<ActionResult> Index(string orderField, string orderDirection,
            int pageSize = 30, int pageCurrent = 1)
        {
            if (pageCurrent < 1)
            {
                pageCurrent = 1;
            }
            if (pageSize < 1)
            {
                pageSize = 1;
            }

            orderField = orderField ?? DatagridConfig.DefaultSortname ?? "Id";
            orderDirection = orderDirection ?? "asc";
            var queryable = orderDirection.Equals("asc")
                ? await GetQueryable().OrderByAsync(orderField)
                : await GetQueryable().OrderByAsync(orderField, ListSortDirection.Descending);

            var pagination = new Pagination
            {
                PageSize = pageSize,
                PageCurrent = pageCurrent,
                SelectPageSize = SelectPageSize,
                DefualtPageSize = 30,
                Total = queryable.Count(),
                OrderField = orderField,
                OrderDirection = orderDirection
            };

            var dataGrid = new Datagrid<TEntity, TDto>
            {
                DatagridConfig = DatagridConfig,
            };

            var pageViewModel = new PageViewModel<TEntity>()
            {
                RuntimeEntityMenberInfo = RuntimeEntityMenberInfo,
                EntityType = typeof (TEntity),
                DtoType = typeof (TDto),
                Pagination = pagination,
                Datagrid = dataGrid
            };

            return View(pageViewModel);
        }
        public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ?  Repository.GetAll() : Repository.Query(predicate);
        }

        public virtual ActionResult List(string orderField, string orderDirection,
            int pageSize = 30, int pageCurrent = 1)
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

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (RuntimeEntityMenberInfo == null)
                RuntimeEntityMenberInfo = new RuntimeEntityMenberInfo(typeof (TDto), User);
            base.OnActionExecuting(filterContext);
        }
    }
}