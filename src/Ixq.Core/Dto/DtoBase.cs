using System;
using Ixq.Core.Mapper;
using Newtonsoft.Json;

namespace Ixq.Core.Dto
{
    public abstract class DtoBase<TEntity> : IDto<TEntity, Guid>
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