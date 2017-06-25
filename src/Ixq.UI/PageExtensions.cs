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
        public static MvcHtmlString PropertyViewer(this HtmlHelper helper, IEntityPropertyMetadata runtimeProperty,
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

        public static MvcHtmlString PropertyEditor(this HtmlHelper helper, IEntityPropertyMetadata runtimeProperty,
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

        /// <summary>
        ///     注册脚本。
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="order"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public static MvcHtmlString Script(this HtmlHelper helper, string order, Func<object, HelperResult> template)
        {
            helper.ViewContext.HttpContext.Items["_script_" + Guid.NewGuid()] = new ScriptTemplate(template, order);
            return MvcHtmlString.Empty;
        }

        /// <summary>
        ///     注册样式。
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="order"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public static MvcHtmlString Styles(this HtmlHelper helper, string order, Func<object, HelperResult> template)
        {
            helper.ViewContext.HttpContext.Items["_styles_" + Guid.NewGuid()] = new StyleTemplate(template, order);
            return MvcHtmlString.Empty;
        }

        /// <summary>
        ///     渲染脚本。
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IHtmlString RenderScripts(this HtmlHelper helper)
        {
            var scripts = new List<ScriptTemplate>();
            foreach (ScriptTemplate htmlTemplate in helper.ViewContext.HttpContext.Items.Values.OfType<ScriptTemplate>().OrderBy(x => x.Order))
            {
                if (!scripts.Any(x => x.Equals(htmlTemplate)))
                {
                    helper.ViewContext.Writer.Write(htmlTemplate.Template(null));
                    scripts.Add(htmlTemplate);
                }
            }
            scripts = null;
            return MvcHtmlString.Empty;
        }

        /// <summary>
        ///     渲染样式。
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IHtmlString RenderStyles(this HtmlHelper helper)
        {
            var styles = new List<StyleTemplate>();
            foreach (StyleTemplate htmlTemplate in helper.ViewContext.HttpContext.Items.Values.OfType<StyleTemplate>().OrderBy(x => x.Order))
            {
                if (!styles.Any(x => x.Equals(htmlTemplate)))
                {
                    helper.ViewContext.Writer.Write(htmlTemplate.Template(null));
                    styles.Add(htmlTemplate);
                }
            }
            styles = null;
            return MvcHtmlString.Empty;
        }
    }
}
