using System;
using System.Web.Mvc;
using Ixq.Core.Logging;

namespace Ixq.Web.Mvc
{
    /// <summary>
    ///     基础控制器。
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        ///     依赖注入服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        public ILogger Logger { get; set; }

        protected BaseController()
        {
            Logger = LogManager.GetLogger(GetType());
        }
    }
}