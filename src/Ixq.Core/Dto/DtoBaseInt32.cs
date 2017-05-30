using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;
using Ixq.Core.Mapper;
using Newtonsoft.Json;

namespace Ixq.Core.Dto
{
    /// <summary>
    ///     数据传输对象基类。
    /// </summary>
    /// <typeparam name="TEntity">实体。</typeparam>
    public abstract class DtoBaseInt32<TEntity> : IDto<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected DtoBaseInt32()
        {
            Mapper = MapperExtensions.Instance;
        }

        [JsonIgnore]
        public IMapper Mapper { get; set; }

        public virtual int Id { get; set; }

        public virtual TEntity MapTo()
        {
            if (Mapper == null)
                throw new ArgumentNullException(nameof(Mapper), "尚未初始化Mapper组件。");
            return Mapper.MapTo<TEntity>(this);
        }
    }
}
