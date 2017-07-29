using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Dto;
using Ixq.Core.Entity;

namespace Ixq.Web.Mvc
{
    /// <summary>
    ///     实体更新器。
    /// </summary>
    public interface IEntityUpdater<TEntity, TDto, TKey>
        where TEntity : class, IEntity<TKey>, new()
          where TDto : class, IDto<TEntity, TKey>, new()
    {
        IEntityMetadata EntityMetadata { get; }
        IQueryable<TEntity> Query();

    }
}
