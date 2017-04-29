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
using Ixq.Data.Repository.Extensions;
using Ixq.UI;
using System.Text;
using Newtonsoft.Json;

namespace Ixq.Web.Mvc
{
    public abstract class EntityController<TEntity, TDto> : BaseController
        where TEntity : class, IEntity<Guid>, new()
        where TDto : class, IDto<TEntity, Guid>, new()
    {
        public readonly IRepository<TEntity> Repository;
        public int[] SelectPageSize { get; set; }
        public IPageConfig PageConfig { get; set; }
        public RuntimeEntityMenberInfo RuntimeEntityMenberInfo { get; set; }

        protected EntityController(IRepository<TEntity> repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            SelectPageSize = new[] { 30, 60, 120, 150 };
            PageConfig = typeof(TDto).GetAttribute<PageAttribute>() ??
                                new PageAttribute();
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

            orderField = string.IsNullOrWhiteSpace(orderField) ? PageConfig.DefaultSortname ?? "Id" : orderField;
            orderDirection = string.IsNullOrWhiteSpace(orderDirection) ? "asc" : orderDirection;
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

            var pageViewModel = new PageViewModel<TEntity>()
            {
                RuntimeEntityMenberInfo = RuntimeEntityMenberInfo,
                EntityType = typeof (TEntity),
                DtoType = typeof (TDto),
                Pagination = pagination,
                PageConfig = PageConfig
            };
            return View(pageViewModel);
        }
        public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ?  Repository.GetAll() : Repository.Query(predicate);
        }

        public virtual async Task<ActionResult> List(string orderField, string orderDirection,
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

            orderField = string.IsNullOrWhiteSpace(orderField) ? PageConfig.DefaultSortname ?? "Id" : orderField;
            orderDirection = string.IsNullOrWhiteSpace(orderDirection) ? "asc" : orderDirection;
            var queryable = orderDirection.Equals("asc")
                ? await GetQueryable().OrderByAsync(orderField)
                : await GetQueryable().OrderByAsync(orderField, ListSortDirection.Descending);

            var pageListViewModel = new PageDataViewModel(queryable.Count(), pageCurrent, pageSize)
            {
                Items = queryable
                    .Skip((pageCurrent - 1)*pageSize)
                    .Take(pageSize)
                    .ToDtoList<TDto, TEntity>()
            };

            return Json(pageListViewModel, new JsonSerializerSettings { DateFormatString = "yyyy-MM-dd HH:mm:ss" });
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

        protected JsonResult Json(object data, JsonSerializerSettings serializerSettings)
        {
            return Json(data, null, null, JsonRequestBehavior.DenyGet, serializerSettings);
        }
        protected override System.Web.Mvc.JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return Json(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet);
        }
        protected override System.Web.Mvc.JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return Json(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet, null);
        }
        protected JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior,
            JsonSerializerSettings serializerSettings)
        {
            return new JsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                SerializerSettings = serializerSettings
            };
        }
    }
}