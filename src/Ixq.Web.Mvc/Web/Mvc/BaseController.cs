using System;
using System.Web.Mvc;
using Ixq.Core.Logging;
using Ixq.Core.Security;

namespace Ixq.Web.Mvc
{
    /// <summary>
    ///     基础控制器。
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        ///     初始化BaseController。
        /// </summary>
        protected BaseController()
        {
            Logger = LogManager.GetLogger(GetType());
        }

        /// <summary>
        ///     依赖注入服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        ///     日志记录器。
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        ///     获取当前 HTTP 请求的安全信息。
        /// </summary>
        public new AppPrincipal User => HttpContext?.User as AppPrincipal;
    }
}