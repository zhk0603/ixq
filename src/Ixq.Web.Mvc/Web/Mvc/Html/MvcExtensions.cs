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
        public static MvcHtmlString PropertyView(this HtmlHelper helper, IRuntimePropertyMenberInfo runtimeProperty)
        {
            return helper.Partial("");
        }
    }
}
