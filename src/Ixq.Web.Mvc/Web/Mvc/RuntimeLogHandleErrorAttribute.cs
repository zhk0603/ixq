using System;
using System.Web;
using System.Web.Mvc;
using Ixq.Core.Logging;

namespace Ixq.Web.Mvc
{
    /// <summary>
    ///     表示一个特性，该特性用于处理由操作方法引发的异常。
    ///     并使用日志记录器记录错误信息。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RuntimeLogHandleErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        ///     在发生异常时调用。
        /// </summary>
        /// <param name="filterContext">操作筛选器上下文。</param>
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException(nameof(filterContext));
            if (filterContext.IsChildAction)
                return;
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
                return;
            var exception = filterContext.Exception;
            if (new HttpException(null, exception).GetHttpCode() != 500)
                return;
            if (!ExceptionType.IsInstanceOfType(exception))
                return;

            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        error = true,
                        message = filterContext.Exception.Message
                    }
                };
            }
            else
            {
                var controllerName = (string) filterContext.RouteData.Values["controller"];
                var actionName = (string) filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };
            }

            // 记录异常。
            LogManager.GetLogger(GetType())
                ?.Error($"\r\n请求地址：{filterContext.HttpContext.Request.Url}\r\n" + exception.Message, exception);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}