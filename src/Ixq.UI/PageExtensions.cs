using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;
using Ixq.Core.Entity;
using Ixq.UI.ComponentModel;
using Ixq.UI.Controls;

namespace Ixq.UI
{
    public static class PageExtensions
    {
        public static MvcHtmlString PropertyViewer(this HtmlHelper helper, IRuntimePropertyMenberInfo runtimeProperty,
            object entityDto)
        {
            if (helper == null)
                throw new ArgumentNullException(nameof(helper));
            if (runtimeProperty == null)
                throw new ArgumentNullException(nameof(runtimeProperty));
            if (entityDto == null)
                throw new ArgumentNullException(nameof(entityDto));

            var model = new PropertyViewModel();
            if (runtimeProperty.DataType == Core.DataType.CustomDataType)
                return helper.Partial(runtimeProperty.PartialViewPath + runtimeProperty.CustomDataType + "Viewer", model);

            return helper.Partial(runtimeProperty.PartialViewPath + runtimeProperty.DataType + "Viewer", model);

        }

        public static MvcHtmlString PropertyEditor(this HtmlHelper helper, IRuntimePropertyMenberInfo runtimeProperty,
            object entityDto)
        {
            if (helper == null)
                throw new ArgumentNullException(nameof(helper));
            if (runtimeProperty == null)
                throw new ArgumentNullException(nameof(runtimeProperty));
            if (entityDto == null)
                throw new ArgumentNullException(nameof(entityDto));

            var model = new PropertyEditModel(runtimeProperty, entityDto);
            if (runtimeProperty.DataType == Core.DataType.CustomDataType)
                return helper.Partial(runtimeProperty.PartialViewPath + runtimeProperty.CustomDataType + "Editor", model);

            return helper.Partial(runtimeProperty.PartialViewPath + runtimeProperty.DataType + "Editor", model);
        }

        public static MvcHtmlString ButtonCustom(this HtmlHelper helper, Button[] customButton)
        {
            return helper.Partial("");
        }


        public static MvcHtmlString Script(this HtmlHelper helper, params Func<object, HelperResult>[] templates)
        {
            foreach (var tmp in templates)
            {
                helper.ViewContext.HttpContext.Items["_script_" + Guid.NewGuid()] = tmp;
            }
            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString Sytles(this HtmlHelper helper, params Func<object, HelperResult>[] templates)
        {
            foreach (var tmp in templates)
            {
                helper.ViewContext.HttpContext.Items["_styles_" + Guid.NewGuid()] = tmp;
            }
            return MvcHtmlString.Empty;
        }

        public static IHtmlString RenderScripts(this HtmlHelper helper)
        {
            foreach (object key in helper.ViewContext.HttpContext.Items.Keys)
            {
                if (key.ToString().StartsWith("_script_"))
                {
                    var template = helper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                    if (template != null)
                    {
                        helper.ViewContext.Writer.Write(template(null));
                    }
                }
            }
            return MvcHtmlString.Empty;
        }

        public static IHtmlString RenderSytles(this HtmlHelper helper)
        {
            foreach (object key in helper.ViewContext.HttpContext.Items.Keys)
            {
                if (key.ToString().StartsWith("_styles_"))
                {
                    var template = helper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                    if (template != null)
                    {
                        helper.ViewContext.Writer.Write(template(null));
                    }
                }
            }
            return MvcHtmlString.Empty;
        }
    }
}
