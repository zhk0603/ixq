using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;
using Ixq.Core.Repository;

namespace Ixq.Data.Repository
{
    /// <summary>
    ///     实体主键类型为<see cref="Int32"/>的仓储。
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryInt32<TEntity> : RepositoryBase<TEntity, int>, IRepositoryInt32<TEntity>
        where TEntity : class, IEntity<int>, new()
    {
        /// <summary>
        ///     初始化一个<see cref="IRepositoryInt32{TEntity}"/>实例。
        /// </summary>
        /// <param name="unitOfWork"></param>
        public RepositoryInt32(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
