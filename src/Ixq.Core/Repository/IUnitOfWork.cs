using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;

namespace Ixq.Core.Repository
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork : ISingletonDependency
    {
        /// <summary>
        ///     提交当前单元操作的更改。
        /// </summary>
        /// <returns>操作影响的行数</returns>
        int Save();

        /// <summary>
        ///     异步提交当前单元操作的更改。
        /// </summary>
        /// <returns>操作影响的行数</returns>
        Task<int> SaveAsync();

        /// <summary>
        ///     回滚事务
        /// </summary>
        void Rollback();

    }
}
