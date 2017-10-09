using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;
using Ixq.Core;
using Ixq.Core.Entity;
using Ixq.UI.ComponentModel;
using Ixq.UI.Controls;

;
using System.Globalization;

namespace Ixq.UI
{
    /// <summary>
    ///     HtmlHelper扩展方法。
    /// </summary>
    public static class HtmlHelperExtensions
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
        {            {
                throw new ArgumentNullException(nameof(helper));
            }mentNullException(nameof(helper));
                     {
                throw new ArgumentNullException(nameof(propertyMetadata));
            }ception(nameof(propertyMetadata));
              {
                throw new ArgumentNullException(nameof(entityDto));
            }tNullException(nameof(entityDto));

            var model = new PropertyViewModel(propertyMetadata, entityDto);
            if (propertyMetadata.Data            {
                return helper.Partial(propertyMetadata.PartialViewPath + propertyMetadata.CustomDataType + "Viewer",
                    model);
            } "Viewer", model);

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
            if             {
                throw new ArgumentNullException(nameof(helper));
            }n(nameof(helper));
            if (propertyM            {
                throw new ArgumentNullException(nameof(propertyMetadata));
            }ropertyMetadata));
            if (en            {
                throw new ArgumentNullException(nameof(entityDto));
            }ameof(entityDto));

            var model = new PropertyEditModel(propertyMetadata, entityDto);
            if (propertyMetadata.DataType == Core.Dat            {
                return helper.Partial(propertyMetadata.PartialViewPath + propertyMetadata.CustomDataType + "Editor",
                    model);
            });

            return helper.Partial(propertyMetadata.PartialViewPath + propertyMetadata.DataType + "Editor", model);
        }

        /// <summary>
        ///     根据指定的属性名或模型对象的名称，从模型错误中显示一个错误信息对应的HTML标记。
        /// </summary>
        /// <param name="helper">HTML帮助器实例。</param>
        /// <param name="modelName">所验证的属性或模型对象的名称。</param>
        /// <param name="htmlAttributes">包含元素 HTML 特性的对象。</param>
        /// <returns></returns>
        public static MvcHtmlString PropertyValidationMessage(this HtmlHelper helper, string modelName,
            object htmlAttributes)
        {
            modelName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(modelName);
            if (!helper.ViewData.ModelState.ContainsKey(modelName))            {
                return null;
            }     return null;
            }

            ModelState modelState = helper.ViewData.ModelState[modelName];
            ModelErrorCollection modelErrors = (modelState == null) ? null : modelState.Errors;
            ModelError modelError = (((modelErrors == null) || (modelErrors.Count == 0)) ? null : modelErrors.FirstOrDefault(m => !String.IsNullOrEmpty(m.ErrorMessage)) ?? modelErrors[0]);            {
                return null;
            } == null)
            {
                return null;
            }

            TagBuilder builder = new TagBuilder("label");
            builder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            builder.AddCssClass("error");
            builder.MergeAttribute("id", $"{modelName}-error");
            builder.MergeAttribute("for", modelName);

            var iTag = new TagBuilder("i");
            iTag.AddCssClass("fa fa-times-circle");

            builder.InnerHtml = iTag.ToString(TagRenderMode.Normal) + GetUserErrorMessageOrDefault(helper.ViewContext.HttpContext, modelError, modelState);
            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }
        /// <summary>
        ///     根据指定的属性名或模型对象的名称，从模型错误中显示一个错误信息对应的HTML标记。
        /// </summary>
        /// <param name="helper">HTML帮助器实例。</param>
        /// <param name="modelName">所验证的属性或模型对象的名称。</param>
        /// <returns></returns>
        public static MvcHtmlString PropertyValidationMessage(this HtmlHelper helper, string modelName)
        {
            return PropertyValidationMessage(helper, modelName, null);
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
                      {
                ((List<HtmlTemplate>) helper.ViewContext.HttpContext.Items[type]).Add(htmlTemplate);
            }lTemplate>)helper.Vi            {
                helper.ViewContext.HttpContext.Items[type] = new List<HtmlTemplate> {htmlTemplate};
            } {
                helper.ViewContext.HttpContext.Items[type] = new List<HtmlTemplate> { htmlTemplate };
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
                var resources = (                {
                    if (resource != null)
                    {
                        helper.ViewContext.Writer.Write(resource.Template(null));
                    }
                }urce in resources)
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
        public static MvcHtmlString Script(this HtmlHelper helper, Func<object, HelperResult> tem            {
                ((List<ScriptTemplate>) helper.ViewContext.HttpContext.Items["_scripts_"]).Add(htmlTemplate);
            }ontext.HttpContext.I            {
                helper.ViewContext.HttpContext.Items["_scripts_"] = new List<ScriptTemplate> {htmlTemplate};
            }text.Items["_scripts_"]).Add(htmlTemplate);
            }
            else
            {
                helper.ViewContext.HttpContext.Items["_scripts_"] = new List<ScriptTemplate> { htmlTemplate };
            }
            return MvcHtmlString.Empty;
        }

        /// <summary>
        ///     注册样式。
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public static MvcHtmlStrin            {
                ((List<StyleTemplate>) helper.ViewContext.HttpContext.Items["_styles_"]).Add(htmlTemplate);
            }ate = new StyleTempl            {
                helper.ViewContext.HttpContext.Items["_styles_"] = new List<StyleTemplate> {htmlTemplate};
            }                ((List<StyleTemplate>)helper.ViewContext.HttpContext.Items["_styles_"]).Add(htmlTemplate);
            }
            else
            {
                helper.ViewContext.HttpContext.Items["_styles_"] = new List<StyleTemplate> { htmlTemplate };
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
            var scripts = n                {
                    if (!scripts.Any(x => x.Equals(resource)))
                    {
                        helper.ViewContext.Writer.Write(resource.Template(null));
                        scripts.Add(resource);
                    }
                }  foreach (var resource in resources)
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
        public static IHtmlString RenderStyles(this HtmlHelper helpe                {
                    if (!styles.Any(x => x.Equals(resource)))
                    {
                        helper.ViewContext.Writer.Write(resource.Template(null));
                        styles.Add(resource);
                    }
                }text.Items["_styles_"];
                foreach (var resource in resources)
                {
                    if (!styles.Any(x => x.Equals(resource)))
                    {
                        helper.ViewContext.Writer.Write(resource.Template(null));
                               {
                return error.ErrorMessage;
            }      }
                }
                       {
                return null;
            }mlString.Empty;
        }

        private static string GetUserErrorMessageOrDefault(HttpContextBase httpContext, ModelError error, ModelState modelState)
        {
            if (!String.IsNullOrEmpty(error.ErrorMessage))
            {
                return error.ErrorMessage;
            }
            if (modelState == null)
            {
                return null;
            }

            string attemptedValue = (modelState.Value != null) ? modelState.Value.AttemptedValue : null            {
                resourceValue = httpContext.GetGlobalResourceObject(ValidationExtensions.ResourceClassKey,
                    "InvalidPropertyValue", CultureInfo.CurrentUICulture) as string;
            }Resource(HttpContextBase httpContext)
        {
            string resourceVa
    null;
            if (!String.IsNullOrEmpty(ValidationExtensions.ResourceClassKey) && (httpContext != null))
            {
                resourceValue = httpContext.GetGlobalResourceObject(ValidationExtensions.ResourceClassKey, "InvalidPropertyValue", CultureInfo.CurrentUICulture) as string;
            }
            return resourceValue ?? "The value '{0}' is invalid.";
        }
    }
}
