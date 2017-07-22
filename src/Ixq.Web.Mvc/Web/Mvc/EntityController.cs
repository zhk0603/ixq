using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ixq.Core.DependencyInjection.Extensions;
using Ixq.Core.Dto;
using Ixq.Core.Entity;
using Ixq.Core.Mapper;
using Ixq.Core.Repository;
using Ixq.Data.Repository.Extensions;
using Ixq.Extensions;
using Ixq.UI;
using Ixq.UI.ComponentModel;
using Ixq.UI.ComponentModel.DataAnnotations;
using Ixq.UI.Controls;
using Newtonsoft.Json;

namespace Ixq.Web.Mvc
{
    /// <summary>
    ///     实体控制器。
    /// </summary>
    /// <typeparam name="TEntity">实体。</typeparam>
    /// <typeparam name="TDto">数据传输对象。</typeparam>
    /// <typeparam name="TKey">实体主键类型。</typeparam>
    public abstract class EntityController<TEntity, TDto, TKey> : BaseController
        where TEntity : class, IEntity<TKey>, new()
        where TDto : class, IDto<TEntity, TKey>, new()
    {
        /// <summary>
        ///     实体仓储。
        /// </summary>
        public readonly IRepositoryBase<TEntity, TKey> Repository;

        private IEntityMetadataProvider _entityMetadataProvider;

        /// <summary>
        ///     初始化一个<see cref="EntityController{TEntity, TDto, TKey}" />对象。
        /// </summary>
        /// <param name="repository">实体仓储。</param>
        protected EntityController(IRepositoryBase<TEntity, TKey> repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            PageSizeList = new[] {15, 30, 60, 120};
            PageConfig = typeof (TDto).GetAttribute<PageAttribute>() ??
                         new PageAttribute();
            Repository = repository;
        }

        /// <summary>
        ///     获取或设置页面大小集合。
        /// </summary>
        public int[] PageSizeList { get; set; }

        /// <summary>
        ///     获取或设置页面配置信息。
        /// </summary>
        public IPageConfig PageConfig { get; set; }

        /// <summary>
        ///     获取或设置实体元数据。
        /// </summary>
        public IEntityMetadata EntityMetadata { get; set; }

        /// <summary>
        ///     获取或设置实体元数据提供者。
        /// </summary>
        public IEntityMetadataProvider EntityMetadataProvider
        {
            get { return _entityMetadataProvider ?? (_entityMetadataProvider = CreateEntityMetadataProvider()); }
            set { _entityMetadataProvider = value; }
        }

        /// <summary>
        ///     根据查询谓词提取仓储元素。
        ///     所有需要获取数据的时候都应使用此方法，通过谓词可自定义过滤不需要的业务数据，
        ///     所以不推荐通过<see cref="Repository" />获取数据。
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> EntityQueryable(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? Repository.GetAll() : Repository.Query(predicate);
        }

        //public virtual async Task<ActionResult> Index(string orderField, string orderDirection,
        /// <summary>
        ///     Index 操作。
        /// </summary>
        /// <param name="orderField">排序字段。</param>
        /// <param name="orderDirection">排序方向。</param>
        /// <param name="pageSize">页面大小。</param>
        /// <param name="pageCurrent">当前页。</param>
        /// <returns></returns>
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

            var pagination = new Pagination
            {
                PageSize = pageSize,
                PageCurrent = pageCurrent,
                PageSizeList = PageSizeList,
                DefualtPageSize = PageSizeList[0],
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

        /// <summary>
        ///     List 操作。
        /// </summary>
        /// <param name="orderField">排序字段。</param>
        /// <param name="orderDirection">排序方向。</param>
        /// <param name="pageSize">页面大小。</param>
        /// <param name="pageCurrent">当前页。</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Detail 操作。
        /// </summary>
        /// <param name="id">实体主键。</param>
        /// <returns></returns>
        public virtual async Task<ActionResult> Detail(string id)
        {
            var entity = await Repository.SingleByIdAsync(ParseEntityKey(id));
            var detailModel = new PageEditViewModel<TDto, TKey>(entity.MapToDto<TDto, TKey>(),
                EntityMetadata.DetailPropertyMetadatas);
            return View(detailModel);
        }

        /// <summary>
        ///     Edit 操作。
        /// </summary>
        /// <param name="id">实体主键。</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Edit 操作。
        /// </summary>
        /// <param name="model">数据传输对象模型。</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(TDto model)
        {
            if (ModelState.IsValid)
            {
            }
            return Json("");
        }

        /// <summary>
        ///     Delete 操作。
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Delete()
        {
            return View();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Searchers()
        {
            return View();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult MultipleSelector()
        {
            return View();
        }
        /// <summary>
        ///     在调用操作方法前初始化实体元数据。
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            EntityMetadata = EntityMetadataProvider.GetEntityMetadata(typeof(TDto));
            base.OnActionExecuting(filterContext);
        }
        /// <summary>
        ///     创建一个<see cref="JsonReader" />对象，将指定的对象序列化为JavaScript Object Notation（JSON）。
        /// </summary>
        /// <param name="data">要序列化的对象。</param>
        /// <param name="serializerSettings">设置。</param>
        /// <returns></returns>
        protected virtual JsonResult Json(object data, JsonSerializerSettings serializerSettings)
        {
            return Json(data, null, null, JsonRequestBehavior.DenyGet, serializerSettings);
        }

        /// <summary>
        ///     创建一个<see cref="JsonReader" />对象，将指定的对象序列化为JavaScript Object Notation（JSON）。
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
        ///     创建一个<see cref="JsonReader" />对象，将指定的对象序列化为JavaScript Object Notation（JSON）。
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
        ///     创建一个<see cref="JsonReader" />对象，将指定的对象序列化为JavaScript Object Notation（JSON）。
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
        ///     创建实体元数据提供者，默认提供<see cref="Ixq.Web.Mvc.EntityMetadataProvider" />。可在派生类中重写。
        /// </summary>
        /// <returns></returns>
        protected virtual IEntityMetadataProvider CreateEntityMetadataProvider()
        {
            return ServiceProvider?.GetService<IEntityMetadataProvider>() ??
                   new EntityMetadataProvider();
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual object ParseEntityKey(string value)
        {
            return RepositoryExtensions.ParseEntityKey<TKey>(value);
        }
    }
}