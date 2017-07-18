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
    /// <summary>
    ///     HtmlHelper扩展方法。
    /// </summary>
    public static class PageExtensions
    {
        /// <summary>
        ///     属性查看器。
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="propertyMetadata">实体属性原始数据。</param>
        /// <param name="entityDto">实体数据传输对象。</param>
        /// <returns></returns>
        public static MvcHtmlString PropertyViewer(this HtmlHelper helper, IEntityPropertyMetadata propertyMetadata,
            object entityDto)
        {
            if (helper == null)
                throw new ArgumentNullException(nameof(helper));
            if (propertyMetadata == null)
                throw new ArgumentNullException(nameof(propertyMetadata));
            if (entityDto == null)
                throw new ArgumentNullException(nameof(entityDto));

            var model = new PropertyViewModel(propertyMetadata, entityDto);
            if (propertyMetadata.DataType == Core.DataType.CustomDataType)
                return helper.Partial(propertyMetadata.PartialViewPath + propertyMetadata.CustomDataType + "Viewer", model);

            return helper.Partial(propertyMetadata.PartialViewPath + propertyMetadata.DataType + "Viewer", model);

        }
        /// <summary>
        ///     属性编辑器。
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="propertyMetadata">实体属性原始数据。</param>
        /// <param name="entityDto">实体数据传输对象。</param>
        /// <returns></returns>
        public static MvcHtmlString PropertyEditor(this HtmlHelper helper, IEntityPropertyMetadata propertyMetadata,
            object entityDto)
        {
            if (helper == null)
                throw new ArgumentNullException(nameof(helper));
            if (propertyMetadata == null)
                throw new ArgumentNullException(nameof(propertyMetadata));
            if (entityDto == null)
                throw new ArgumentNullException(nameof(entityDto));

            var model = new PropertyEditModel(propertyMetadata, entityDto);
            if (propertyMetadata.DataType == Core.DataType.CustomDataType)
                return helper.Partial(propertyMetadata.PartialViewPath + propertyMetadata.CustomDataType + "Editor", model);

            return helper.Partial(propertyMetadata.PartialViewPath + propertyMetadata.DataType + "Editor", model);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="customButton"></param>
        /// <returns></returns>
        public static MvcHtmlString ButtonCustom(this HtmlHelper helper, Button[] customButton)
        {
            return helper.Partial("");
        }

        /// <summary>
        ///     注册资源。
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="type"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public static MvcHtmlString Resource(this HtmlHelper helper, string type, Func<object, HelperResult> template)
        {
            var htmlTemplate = new HtmlTemplate(template);
            if (helper.ViewContext.HttpContext.Items[type] != null)
            {
                ((List<HtmlTemplate>) helper.ViewContext.HttpContext.Items[type]).Add(htmlTemplate);
            }
            else
            {
                helper.ViewContext.HttpContext.Items[type] = new List<HtmlTemplate> {htmlTemplate};
            }
            return MvcHtmlString.Empty;
        }

        /// <summary>
        ///     渲染资源。
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IHtmlString RenderResources(this HtmlHelper helper, string type)
        {
            if (helper.ViewContext.HttpContext.Items[type] != null)
            {
                var resources = (List<HtmlTemplate>) helper.ViewContext.HttpContext.Items[type];

                foreach (var resource in resources)
                {
                    if (resource != null) helper.ViewContext.Writer.Write(resource.Template(null));
                }
            }

            return MvcHtmlString.Empty;
        }

        /// <summary>
        ///     注册脚本。
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public static MvcHtmlString Script(this HtmlHelper helper, Func<object, HelperResult> template)
        {
            var htmlTemplate = new ScriptTemplate(template);
            if (helper.ViewContext.HttpContext.Items["_scripts_"] != null)
            {
                ((List<ScriptTemplate>) helper.ViewContext.HttpContext.Items["_scripts_"]).Add(htmlTemplate);
            }
            else
            {
                helper.ViewContext.HttpContext.Items["_scripts_"] = new List<ScriptTemplate> {htmlTemplate};
            }
            return MvcHtmlString.Empty;
        }

        /// <summary>
        ///     注册样式。
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public static MvcHtmlString Styles(this HtmlHelper helper, Func<object, HelperResult> template)
        {
            var htmlTemplate = new StyleTemplate(template);
            if (helper.ViewContext.HttpContext.Items["_styles_"] != null)
            {
                ((List<StyleTemplate>) helper.ViewContext.HttpContext.Items["_styles_"]).Add(htmlTemplate);
            }
            else
            {
                helper.ViewContext.HttpContext.Items["_styles_"] = new List<StyleTemplate> {htmlTemplate};
            }
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
            if (helper.ViewContext.HttpContext.Items["_scripts_"] != null)
            {
                var resources = (List<ScriptTemplate>) helper.ViewContext.HttpContext.Items["_scripts_"];
                foreach (var resource in resources)
                {
                    if (!scripts.Any(x => x.Equals(resource)))
                    {
                        helper.ViewContext.Writer.Write(resource.Template(null));
                        scripts.Add(resource);
                    }
                }
            }
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
            if (helper.ViewContext.HttpContext.Items["_styles_"] != null)
            {
                var resources = (List<StyleTemplate>) helper.ViewContext.HttpContext.Items["_styles_"];
                foreach (var resource in resources)
                {
                    if (!styles.Any(x => x.Equals(resource)))
                    {
                        helper.ViewContext.Writer.Write(resource.Template(null));
                        styles.Add(resource);
                    }
                }
            }
            return MvcHtmlString.Empty;
        }
    }
}
