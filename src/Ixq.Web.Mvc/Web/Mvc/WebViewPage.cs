using Ixq.Core.Security;

namespace Ixq.Web.Mvc
{
    public abstract class WebViewPage : System.Web.Mvc.WebViewPage
    {
        public new virtual AppPrincipal User => base.User as AppPrincipal;
    }

    public abstract class WebViewPage<T> : System.Web.Mvc.WebViewPage<T>
    {
        public new virtual AppPrincipal User => base.User as AppPrincipal;
    }
}