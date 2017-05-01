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
using Ixq.Core.Cache;
using Newtonsoft.Json;
using Ixq.Core.Mapper;
using Ixq.UI.ComponentModel;

namespace Ixq.Web.Mvc
{
    public abstract class EntityController<TEntity, TDto, TKey> : BaseController
        where TEntity : class, IEntity<TKey>, new()
        where TDto : class, IDto<TEntity, TKey>, new()
    {
        public readonly IRepositoryBase<TEntity, TKey> Repository;
        public int[] SelectPageSize { get; set; }
        public IPageConfig PageConfig { get; set; }
        public RuntimeEntityMenberInfo RuntimeEntityMenberInfo { get; set; }

        protected EntityController(IRepositoryBase<TEntity, TKey> repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            SelectPageSize = new[] {30, 60, 120, 150};
            PageConfig = typeof (TDto).GetAttribute<PageAttribute>() ??
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
            orderDirection = string.IsNullOrWhiteSpace(orderDirection)
                ? PageConfig.IsDescending ? "desc" : "asc"
                : orderDirection;
            var queryable = orderDirection.Equals("asc")
                ? await EntityQueryable().OrderByAsync(orderField)
                : await EntityQueryable().OrderByAsync(orderField, ListSortDirection.Descending);

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

            var pageViewModel = new PageViewModel
            {
                RuntimeEntityMenberInfo = RuntimeEntityMenberInfo,
                EntityType = typeof (TEntity),
                DtoType = typeof (TDto),
                Pagination = pagination,
                PageConfig = PageConfig
            };


            return View(pageViewModel);
        }

        public virtual IQueryable<TEntity> EntityQueryable(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? Repository.GetAll() : Repository.Query(predicate);
        }

        [HttpPost]
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
            orderDirection = string.IsNullOrWhiteSpace(orderDirection)
                ? PageConfig.IsDescending ? "desc" : "asc"
                : orderDirection;
            var queryable = orderDirection.Equals("asc")
                ? await EntityQueryable().OrderByAsync(orderField)
                : await EntityQueryable().OrderByAsync(orderField, ListSortDirection.Descending);

            var pageListViewModel = new PageDataViewModel<TKey>(queryable.Count(), pageCurrent, pageSize)
            {
                Items = queryable
                    .Skip((pageCurrent - 1)*pageSize)
                    .Take(pageSize)
                    .ToDtoArray<TDto, TEntity, TKey>()
            };

            return Json(pageListViewModel, new JsonSerializerSettings {DateFormatString = "yyyy-MM-dd HH:mm:ss"});
        }

        public virtual async Task<ActionResult> Edit(string id)
        {
            var entity = string.IsNullOrWhiteSpace(id)
                ? Repository.Create()
                : await Repository.SingleByIdAsync(GetValue(id));

            var editModel = new PageEditViewModel<TDto, TKey>(entity.MapToDto<TDto, TKey>(),
                RuntimeEntityMenberInfo.EditPropertyInfo)
            {
                Title = (string.IsNullOrEmpty(id) ? "新增" : "编辑") + (PageConfig.TitleName ?? typeof (TEntity).Name)
            };
            return View(editModel);
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
            {
                if (CacheManager.IsEnable)
                {
                    var runtimeEntityMenberInfo =
                        CacheManager.GetGlobalCache().Get("RuntimeEntityMenberInfo:" + typeof (TDto)) as
                            RuntimeEntityMenberInfo;
                    if (runtimeEntityMenberInfo == null)
                    {
                        runtimeEntityMenberInfo = new RuntimeEntityMenberInfo(typeof (TDto), User);
                        CacheManager.GetGlobalCache()
                            .Set("RuntimeEntityMenberInfo:" + typeof (TDto),
                                runtimeEntityMenberInfo);
                    }
                    RuntimeEntityMenberInfo = runtimeEntityMenberInfo;
                }
                else
                    RuntimeEntityMenberInfo = new RuntimeEntityMenberInfo(typeof (TDto), User);
            }
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

        protected override System.Web.Mvc.JsonResult Json(object data, string contentType, Encoding contentEncoding,
            JsonRequestBehavior behavior)
        {
            return Json(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet, null);
        }

        protected JsonResult Json(object data, string contentType, Encoding contentEncoding,
            JsonRequestBehavior behavior,
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

        protected virtual object GetValue(string value)
        {
            var type = typeof (TKey);

            dynamic resultValue = value;
            if (type == typeof (Guid))
            {
                resultValue =  Guid.Parse(value);
            }
            if (type == typeof (int))
            {
                resultValue = Convert.ToInt32(value);
            }
            if (type == typeof (short))
            {
                resultValue = Convert.ToInt16(value);
            }
            if (type == typeof (long))
            {
                resultValue = Convert.ToInt64(value);
            }

            return resultValue;
        }
    }
}