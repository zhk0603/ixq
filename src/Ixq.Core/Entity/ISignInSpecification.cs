using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Entity
{
    /// <summary>
    ///     登录规范。
    /// </summary>
    public interface ISignInSpecification
    {
        /// <summary>
        ///     最后一次登录时间。
        /// </summary>
        DateTime? LastSignInDate { get; set; }

        /// <summary>
        ///     在登录完成后发生，由上下文自动执行。
        /// </summary>
        void OnSignInComplete();
    }
}
