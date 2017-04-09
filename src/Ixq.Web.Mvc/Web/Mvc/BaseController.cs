using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ixq.Web.Mvc
{
    /// <summary>
    /// 基础控制器。
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// IoC服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        public bool IsLogin()
        {
            return !string.IsNullOrEmpty(User.Identity.Name);
        }

        public Guid? GetUserId()
        {
            Guid userId;
            Guid.TryParse(User.Identity.Name, out userId);
            return userId;
        }
    }

}
