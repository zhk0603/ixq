using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;

namespace Ixq.Security.Identity
{
    public interface IUser<TKey> : Microsoft.AspNet.Identity.IUser<TKey>, IEntity<TKey>
    {
        new TKey Id { get; set; }

        /// <summary>
        ///     最后一次登录时间。
        /// </summary>
        DateTime? LastSignInDate { get; set; }

        /// <summary>
        ///     最后一次登出时间。
        /// </summary>
        DateTime? LastSignOutDate { get; set; }

        /// <summary>
        ///     在登录完成后发生，由上下文自动执行。
        /// </summary>
        void OnSignInComplete();

        /// <summary>
        ///     在登出完成后发送，由上下文自动执行。
        /// </summary>
        void OnSignOutComplete();

    }
}
