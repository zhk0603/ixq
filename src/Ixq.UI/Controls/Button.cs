using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.UI.Controls
{
    public class Button
    {
        public string Icon { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        public string Javascript { get; set; }
        public string ClassName { get; set; }
        public string GroupName { get; set; }
        public Button(string icon, string text)
        {
            Icon = icon;
            Text = text;
        }

        public Button(string icon, string text, string javascript):this(icon,text)
        {
            Javascript = javascript;
        }

        public Button(string icon, string text, string link, string javascript, string className) : this(icon, text, javascript)
        {
            Link = link;
            ClassName = className;
        }
    }
}
