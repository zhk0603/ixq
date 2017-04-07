using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Entity
{
    /// <summary>
    /// 软删除接口
    /// </summary>
    public interface ISoftDeleteSpecification
    {
        /// <summary>
        ///     删除时间
        /// </summary>
        DateTime? DeleteDate { get; set; }

        /// <summary>
        ///     是否删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
