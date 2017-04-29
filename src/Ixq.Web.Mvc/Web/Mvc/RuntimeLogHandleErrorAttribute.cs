using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ixq.Core.Logging;

namespace Ixq.Web.Mvc
{
    /// <summary>
    /// 日志记录器。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class RuntimeLogHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }
            if (filterContext.IsChildAction)
            {
                return;
            }
            if (filterContext.ExceptionHandled /*|| !filterContext.HttpContext.IsCustomErrorEnabled*/)
            {
                return;
            }
            Exception exception = filterContext.Exception;
            if (new HttpException(null, exception).GetHttpCode() != 500)
            {
                return;
            }
            if (!ExceptionType.IsInstanceOfType(exception))
            {
                return;
            }
            LogManager.GetLogger(GetType())?.Error(exception.Message, exception);
            base.OnException(filterContext);
        }
    }
}
