using System;

namespace Ixq.Core.Entity
{
    /// <summary>
    ///     创建规范。
    /// </summary>
    public interface ICreateSpecification
    {
        /// <summary>
        ///     创建时间。
        /// </summary>
        DateTime CreateDate { get; set; }
        /// <summary>
        ///     在创建时发生，由上下文自动执行。
        /// </summary>
        void OnCreateComplete();
    }
}