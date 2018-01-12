using System;

namespace Ixq.Core.Entity
{
    /// <summary>
    ///     更新规范。
    /// </summary>
    public interface IUpdateSpecification
    {
        /// <summary>
        ///     最后一次更新时间。
        /// </summary>
        DateTime? UpdataDate { get; set; }

        /// <summary>
        ///     最后一次更新操作者Id。
        /// </summary>
        string UpdateUserId { get; set; }

        /// <summary>
        ///     在更新时发生，由上下文自动执行。
        /// </summary>
        void OnUpdataComplete();
    }
}