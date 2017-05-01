using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Ixq.Core.Entity;

namespace Ixq.Web.Mvc.Html
{
    public static class MvcExtensions
    {
        public static MvcHtmlString PropertyViewer(this HtmlHelper helper, IRuntimePropertyMenberInfo runtimeProperty, object entity)
        {
            return helper.Partial("");
        }
        public static MvcHtmlString PropertyEditor(this HtmlHelper helper, IRuntimePropertyMenberInfo runtimeProperty, object entity)
        {
            return helper.Partial("");
        }
    }
}
