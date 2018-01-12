using System;

namespace Ixq.Core.Entity
{
    /// <summary>
    ///     软删除接口
    /// </summary>
    public interface ISoftDeleteSpecification
    {
        /// <summary>
        ///     删除时间
        /// </summary>
        DateTime? DeleteDate { get; set; }

        /// <summary>
        ///     删除人Id。
        /// </summary>
        string DeleteUserId { get; set; }

        /// <summary>
        ///     是否删除
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        ///     在软删除时发生，由上下文自动执行。
        /// </summary>
        void OnSoftDeleteComplete();
    }
}