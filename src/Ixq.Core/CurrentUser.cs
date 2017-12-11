using System;
using System.Threading;
using Ixq.Core.Security;

namespace Ixq.Core
{
    public class CurrentUser
    {
        /// <summary>
        ///     获取当前用户。
        /// </summary>
        public static CurrentUserWrap Current
        {
            get
            {
                var principal = Thread.CurrentPrincipal as AppPrincipal;
                return principal?.Identity.UserInfo;
            }
        }
    }

    /// <summary>
    ///     包装当前授权用户。
    /// </summary>
    public class CurrentUserWrap
    {
        /// <summary>
        ///     获取或设置登录时间。
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        ///     获取或设置登录Ip。
        /// </summary>
        public string LoginIp { get; set; }

        /// <summary>
        ///     获取或设置 AppPrincipal。
        /// </summary>
        public AppPrincipal ClaimsPrincipal { get; set; }
    }
}