using System;
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

namespace Ixq.Web.Mvc
{
    public class EntityController<TEntity, TDto> : BaseController
        where TEntity : class, IEntity<Guid>, new()
        where TDto : class, IDto<TEntity, Guid>, new()
    {
        public readonly IRepository<TEntity> Repository;
        public int[] SelectPageSize { get; set; }
        public DatagridAttribute DatagridAttribute { get; set; }

        protected EntityController(IRepository<TEntity> repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            SelectPageSize = new[] { 30, 60, 120, 150 };
            DatagridAttribute = typeof(TDto).GetAttribute<DatagridAttribute>() ??
                                new DatagridAttribute();
            Repository = repository;
        }

        protected virtual async Task<ActionResult> Index(string orderField = "CreateDate", string orderDirection = "asc",
            int pageSize = 30, int pageCurrent = 1)
        {
            var queryable = orderDirection.Equals("asc")
                ? await Repository.OrderByAsync(orderField)
                : await Repository.OrderByDescAsync(orderField);
            var pagination = new Pagination
            {
                PageSize = pageSize,
                PageCurrent = pageCurrent,
                SelectPageSize = SelectPageSize,
                DefualtPageSize = 30,
                Total = queryable.Count(),
                OrderField = orderField
            };
            var dataGrid = new Datagrid<TEntity, TDto>
            {
                Pagination = pagination,
                EntityType = typeof (TEntity),
                DtoType = typeof (TDto),
                DatagridAttribute = DatagridAttribute,
            };
            var properties =
                dataGrid.DtoType.GetProperties()
                    .Where(x => x.HasAttribute<DisplayAttribute>())
                    .OrderBy(x => x.GetAttribute<DisplayAttribute>().Order).ToArray();
            dataGrid.ColumnsPropertyInfo = properties;
            return View();
        }

        protected virtual ActionResult List()
        {
            return View();
        }

        protected virtual ActionResult Edit()
        {
            return View();
        }

        protected virtual ActionResult Delete()
        {
            return View();
        }

        protected virtual ActionResult Searchers()
        {
            return View();
        }

        protected virtual ActionResult Selector()
        {
            return View();
        }

        protected virtual ActionResult MultipleSelector()
        {
            return View();
        }
    }
}