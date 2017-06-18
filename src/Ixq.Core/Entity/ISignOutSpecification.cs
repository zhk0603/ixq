using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Entity
{
    /// <summary>
    ///     登出规范。
    /// </summary>
    public interface ISignOutSpecification
    {
        /// <summary>
        ///     最后一次登出时间。
        /// </summary>
        DateTime? LastSignOutDate { get; set; }
        /// <summary>
        ///     在登出完成后发送，由上下文自动执行。
        /// </summary>
        void OnSignOutComplete();
    }
}
