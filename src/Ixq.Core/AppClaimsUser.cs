using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core
{
    public class AppClaimsUser
    {
        /// <summary>
        ///     获取或设置当前用户的委托方法。
        /// </summary>
        public static Func<CurrentUserWrap> Current { get; set; }
    }

    /// <summary>
    ///     包装当前授权用户。
    /// </summary>
    public class CurrentUserWrap
    {
        /// <summary>
        ///     获取或设置用户Id。
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        ///     获取或设置用户名称。
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        ///     获取或设置用户昵称。
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        ///     获取或设置手机号码。
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        ///     获取或设置邮箱地址。
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        ///     获取或设置登录次数。
        /// </summary>
        public int LoginCount { get; set; }
        /// <summary>
        ///     获取或设置登录时间。
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        ///     获取或设置登录Ip。
        /// </summary>
        public string LoginIp { get; set; }
        /// <summary>
        ///     获取或设置ClaimsPrincipal。
        /// </summary>
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
