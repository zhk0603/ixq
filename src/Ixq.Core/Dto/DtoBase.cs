using System;
using Ixq.Core.Entity;
using Ixq.Core.Mapper;
using Newtonsoft.Json;

namespace Ixq.Core.Dto
{
    /// <summary>
    ///     数据传输对象基类。
    /// </summary>
    /// <typeparam name="TEntity">实体。</typeparam>
    public abstract class DtoBase<TEntity> : IDto<TEntity, Guid>
        where TEntity : class, IEntity<Guid>
    {
        /// <summary>
        ///     初始化一个数据传输对象。
        /// </summary>
        protected DtoBase() : this(MapperExtensions.Instance)
        {
        }
        /// <summary>
        ///     初始化一个数据传输对象。
        /// </summary>
        /// <param name="mapper">映射器。</param>
        protected DtoBase(IMapper mapper)
        {
            Mapper = mapper;
        }

        /// <summary>
        ///     获取或设置映射器。
        /// </summary>
        [JsonIgnore]
        public IMapper Mapper { get; set; }

        /// <summary>
        ///     获取或设置数据传输对象Id.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        ///     将数据传输对象装换为实体对象。
        /// </summary>
        /// <returns></returns>
        public virtual TEntity MapTo()
        {
            if (Mapper == null)
                throw new ArgumentNullException(nameof(Mapper), "尚未初始化Mapper组件。");
            return Mapper.MapTo<TEntity>(this);
        }
    }
}