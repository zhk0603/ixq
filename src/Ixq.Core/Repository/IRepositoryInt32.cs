using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;

namespace Ixq.Core.Repository
{
    public interface IRepositoryInt32<TEntity> : IRepositoryBase<TEntity, int> where TEntity : class, IEntity<int>, new()
    {
    }
}
