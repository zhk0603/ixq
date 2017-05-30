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
        protected DtoBase()
        {
            Mapper = MapperExtensions.Instance;
        }

        [JsonIgnore]
        public IMapper Mapper { get; set; }

        public virtual Guid Id { get; set; }

        public virtual TEntity MapTo()
        {
            if (Mapper == null)
                throw new ArgumentNullException(nameof(Mapper), "尚未初始化Mapper组件。");
            return Mapper.MapTo<TEntity>(this);
        }
    }
}