using Ixq.Core.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Guid Id { get; set; }

        public TEntity MapTo()
        {
            if (Mapper == null)
                throw new ArgumentNullException("Mapper", "尚未初始化Mapper组件。");
            return Mapper.MapTo<TEntity>(this);
        }
    }
}
