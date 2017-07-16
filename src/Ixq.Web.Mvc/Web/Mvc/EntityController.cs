using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ixq.Core.Dto;
using Ixq.Core.Entity;
using Ixq.Core.Repository;
using Ixq.Core.DependencyInjection.Extensions;
using Ixq.Extensions;
using Ixq.UI.ComponentModel.DataAnnotations;
using Ixq.UI.Controls;
using System.Linq.Expressions;
using Ixq.Data.DataAnnotations;
using System.Web.Routing;
using Ixq.Data.Repository.Extensions;
using Ixq.UI;
using System.Text;
using Ixq.Core.Cache;
using Newtonsoft.Json;
using Ixq.Core.Mapper;
using Ixq.Core.Security;
using Ixq.UI.ComponentModel;
using System.Web.Mvc.Filters;

namespace Ixq.Web.Mvc
{
    public abstract class EntityController<TEntity, TDto, TKey> : BaseController
        where TEntity : class, IEntity<TKey>, new()
        where TDto : class, IDto<TEntity, TKey>, new()
    {
        private IEntityMetadataProvider _entityMetadataProvider;

        public readonly IRepositoryBase<TEntity, TKey> Repository;
        public int[] PageSizeList { get; set; }
        public IPageConfig PageConfig { get; set; }
        public IEntityMetadata EntityMetadata { get; set; }

        public IEntityMetadataProvider EntityMetadataProvider
        {
            get { return _entityMetadataProvider ?? (_entityMetadataProvider = CreateEntityMetadataProvider()); }
            set { _entityMetadataProvider = value; }
        }


        protected EntityController(IRepositoryBase<TEntity, TKey> repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            PageSizeList = new[] {15, 30, 60, 120};
            PageConfig = typeof (TDto).GetAttribute<PageAttribute>() ??
                         new PageAttribute();
            Repository = repository;
        }

        //public virtual async Task<ActionResult> Index(string orderField, string orderDirection,
        public virtual ActionResult Index(string orderField, string orderDirection,
            int pageSize = 30, int pageCurrent = 1)
        {
            if (pageCurrent < 1)
            {
                pageCurrent = 1;
            }
            if (pageSize < 1)
            {
                pageSize = PageSizeList[0];
            }

            orderField = string.IsNullOrWhiteSpace(orderField) ? PageConfig.DefaultSortname ?? "Id" : orderField;
            orderDirection = string.IsNullOrWhiteSpace(orderDirection)
                ? PageConfig.IsDescending ? "desc" : "asc"
                : orderDirection;
            //var queryable = orderDirection.Equals("asc")
            //    ? await EntityQueryable().OrderByAsync(orderField)
            //    : await EntityQueryable().OrderByAsync(orderField, ListSortDirection.Descending);

            var pagination = new Pagination
            {
                PageSize = pageSize,
                PageCurrent = pageCurrent,
                PageSizeList = PageSizeList,
                DefualtPageSize = pageSize,
                //Total = queryable.Count(),
                OrderField = orderField,
                OrderDirection = orderDirection
            };

            var pageViewModel = new PageViewModel
            {
                EntityMetadata = EntityMetadata,
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
                pageSize = PageSizeList[0];
            }

            orderField = string.IsNullOrWhiteSpace(orderField) ? PageConfig.DefaultSortname ?? "Id" : orderField;
            orderDirection = string.IsNullOrWhiteSpace(orderDirection)
                ? PageConfig.IsDescending ? "desc" : "asc"
                : orderDirection;

            var queryable = orderDirection.Equals("asc")
                ? EntityQueryable().OrderBy(orderField)
                : EntityQueryable().OrderBy(orderField, ListSortDirection.Descending);

            var pageListViewModel = new PageDataViewModel<TKey>(queryable.Count(), pageCurrent, pageSize)
            {
                Items = await queryable
                    .Skip((pageCurrent - 1)*pageSize)
                    .Take(pageSize)
                    .ToDtoListAsync<TDto, TEntity, TKey>()
            };

            return Json(pageListViewModel, new JsonSerializerSettings {DateFormatString = "yyyy-MM-dd HH:mm:ss"});
        }

        public virtual async Task<ActionResult> Edit(string id)
        {
            var entity = string.IsNullOrWhiteSpace(id)
                ? Repository.Create()
                : await Repository.SingleByIdAsync(ParseEntityKey(id));

            var editModel = new PageEditViewModel<TDto, TKey>(entity.MapToDto<TDto, TKey>(),
                EntityMetadata.EditPropertyMetadatas)
            {
                Title = (string.IsNullOrEmpty(id) ? "新增" : "编辑") + (PageConfig.Title ?? typeof (TEntity).Name)
            };
            return View(editModel);
        }

        [HttpPost]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(TDto model)
        {
            if (ModelState.IsValid)
            {

            }
            else
            {

            }
            return Json("");
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

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            if (EntityMetadata == null)
            {
                EntityMetadata = this.EntityMetadataProvider.GetEntityMetadata(
                    typeof (TDto));
            }
            base.OnAuthentication(filterContext);
        }

        /// <summary>
        ///     创建一个<see cref="JsonReader"/>对象，将指定的对象序列化为JavaScript Object Notation（JSON）。
        /// </summary>
        /// <param name="data">要序列化的对象。</param>
        /// <param name="serializerSettings">设置。</param>
        /// <returns></returns>
        protected virtual JsonResult Json(object data, JsonSerializerSettings serializerSettings)
        {
            return Json(data, null, null, JsonRequestBehavior.DenyGet, serializerSettings);
        }

        /// <summary>
        ///     创建一个<see cref="JsonReader"/>对象，将指定的对象序列化为JavaScript Object Notation（JSON）。
        /// </summary>
        /// <param name="data">要序列化的对象。</param>
        /// <param name="contentType">内容类型（MIME类型）。</param>
        /// <param name="contentEncoding">内容编码。</param>
        /// <returns></returns>
        protected override System.Web.Mvc.JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return Json(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        ///     创建一个<see cref="JsonReader"/>对象，将指定的对象序列化为JavaScript Object Notation（JSON）。
        /// </summary>
        /// <param name="data">要序列化的对象。</param>
        /// <param name="contentType">内容类型（MIME类型）。</param>
        /// <param name="contentEncoding">内容编码。</param>
        /// <param name="behavior">JSON请求行为。</param>
        /// <returns></returns>
        protected override System.Web.Mvc.JsonResult Json(object data, string contentType, Encoding contentEncoding,
            JsonRequestBehavior behavior)
        {
            return Json(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet, null);
        }

        /// <summary>
        ///     创建一个<see cref="JsonReader"/>对象，将指定的对象序列化为JavaScript Object Notation（JSON）。
        /// </summary>
        /// <param name="data">要序列化的对象。</param>
        /// <param name="contentType">内容类型（MIME类型）。</param>
        /// <param name="contentEncoding">内容编码。</param>
        /// <param name="behavior">JSON请求行为。</param>
        /// <param name="serializerSettings">设置。</param>
        /// <returns></returns>
        protected virtual JsonResult Json(object data, string contentType, Encoding contentEncoding,
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

        /// <summary>
        ///     创建实体元数据提供者，默认提供<see cref="Ixq.Web.Mvc.EntityMetadataProvider"/>。可在派生类中重写。
        /// </summary>
        /// <returns></returns>
        protected virtual IEntityMetadataProvider CreateEntityMetadataProvider()
        {
            return ServiceProvider.GetService<IEntityMetadataProvider>() ??
                   new EntityMetadataProvider();
        }

        protected virtual object ParseEntityKey(string value)
        {
            return RepositoryExtensions.ParseEntityKey<TKey>(value);
        }
    }
}