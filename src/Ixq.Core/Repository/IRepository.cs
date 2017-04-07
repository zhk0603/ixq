using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;

namespace Ixq.Core.Repository
{
    public interface IRepository<TEntity> : IRepositoryBase<TEntity, Guid> where TEntity : class, IEntity<Guid>, new()
    {
    }
}
