using System;
using System.Web.Mvc;

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
    }
}