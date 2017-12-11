using System;
using System.Text.RegularExpressions;
using System.Web.WebPages;

namespace Ixq.UI
{
    /// <summary>
    ///     script标签模版。
    /// </summary>
    public class ScriptTemplate : HtmlTemplate
    {
        /// <summary>
        ///     初始化一个<see cref="ScriptTemplate" />对象。
        /// </summary>
        /// <param name="template">模版委托方法。</param>
        public ScriptTemplate(Func<object, HelperResult> template) : base(template)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is ScriptTemplate))
                return false;

            var pattern = @"<script.*?src=""(.*?)""";

            var value1 = Template(null).ToHtmlString();
            var match1 = Regex.Match(value1, pattern);

            var value2 = (obj as ScriptTemplate).Template(null).ToHtmlString();
            var match2 = Regex.Match(value2, pattern);

            if (!match1.Success || !match2.Success)
                return false;

            return match1.Groups[1].Value == match2.Groups[1].Value;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(ScriptTemplate a, ScriptTemplate b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if ((object) a == null || (object) b == null)
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(ScriptTemplate a, ScriptTemplate b)
        {
            return !(a == b);
        }
    }
}