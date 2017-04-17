using System;

namespace Ixq.Core.Entity
{
    /// <summary>
    ///     创建规范
    /// </summary>
    public interface ICreateSpecification
    {
        DateTime CreateDate { get; set; }
        void OnCreateComplete();
    }
}