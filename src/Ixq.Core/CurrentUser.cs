using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
                var principal = System.Threading.Thread.CurrentPrincipal as AppPrincipal;
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
