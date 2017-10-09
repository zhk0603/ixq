using System;
using System.Web.WebPages;

namespace Ixq.UI
{
    /// <summary>
    ///     html模版。
    /// </summary>
    public class HtmlTemplate
    {
        /// <summary>
        ///     初始化一个<see cref="HtmlTemplate" />对象。
        /// </summary>
        /// <param name="template">模版委托方法。</param>
        public HtmlTemplate(Func<object, HelperResult> template)
        {
            Template = template;
        }

        /// <summary>
        ///     获取或设置模版委托方法。
        /// </summary>
        public Func<object, HelperResult> Template { get; set; }
    }
}