using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace Ixq.UI
{
    public class ScriptTemplate : HtmlTemplate
    {
        public ScriptTemplate(Func<object, HelperResult> template, string order) : base(template, order)
        {
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is ScriptTemplate)) return false;

            string pattern = @"<script.*?src=""(.*?)""";

            var value1 = Template(null).ToHtmlString();
            var match1 = Regex.Match(value1, pattern);

            var value2 = (obj as ScriptTemplate).Template(null).ToHtmlString();
            var match2 = Regex.Match(value2, pattern);

            if (!match1.Success || !match2.Success)
                return false;

            return match1.Groups[1].Value == match2.Groups[1].Value;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(ScriptTemplate a, ScriptTemplate b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(ScriptTemplate a, ScriptTemplate b)
        {
            return !(a == b);
        }
    }
}
