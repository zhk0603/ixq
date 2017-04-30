using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;
using Ixq.Core.Repository;

namespace Ixq.Data.Repository
{
    public class RepositoryInt32<TEntity> : RepositoryBase<TEntity, int>, IRepositoryInt32<TEntity>
        where TEntity : class, IEntity<int>, new()
    {
        public RepositoryInt32(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
