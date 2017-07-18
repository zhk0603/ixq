using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.UI.Controls
{
    /// <summary>
    ///     按钮。
    /// </summary>
    public class Button
    {
        /// <summary>
        ///     俺就图标。
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        ///     获取或设置按钮的文本。
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        ///     获取或设置按钮点击是跳转的连接。
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        ///     获取或设置按钮点击响应的Javascript方法。
        /// </summary>
        public string Javascript { get; set; }
        /// <summary>
        ///     获取或设置按钮样式类。
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        ///     获取或设置按钮分组名称。
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        ///     初始化一个按钮。
        /// </summary>
        /// <param name="icon"></param>
        /// <param name="text"></param>
        public Button(string icon, string text)
        {
            Icon = icon;
            Text = text;
        }
        /// <summary>
        ///     初始化一个按钮。
        /// </summary>
        /// <param name="icon"></param>
        /// <param name="text"></param>
        /// <param name="javascript"></param>
        public Button(string icon, string text, string javascript):this(icon,text)
        {
            Javascript = javascript;
        }
        /// <summary>
        ///     初始化一个按钮。
        /// </summary>
        /// <param name="icon"></param>
        /// <param name="text"></param>
        /// <param name="link"></param>
        /// <param name="javascript"></param>
        /// <param name="className"></param>
        public Button(string icon, string text, string link, string javascript, string className) : this(icon, text, javascript)
        {
            Link = link;
            ClassName = className;
        }
    }
}
