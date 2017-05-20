using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace Ixq.UI
{
    public class HtmlTemplate
    {
        public string Order { get; set; }
        public Func<object, HelperResult> Template { get; set; }
        public HtmlTemplate(Func<object, HelperResult> template,string order)
        {
            this.Template = template;
            Order = order;
        }
    }
}
