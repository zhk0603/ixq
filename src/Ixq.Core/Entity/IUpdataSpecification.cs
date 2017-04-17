using System;

namespace Ixq.Core.Entity
{
    /// <summary>
    ///     更新规范
    /// </summary>
    public interface IUpdataSpecification
    {
        DateTime? UpdataDate { get; set; }
        void OnUpdataComplete();
    }
}