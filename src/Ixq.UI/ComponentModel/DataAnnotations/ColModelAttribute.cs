using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core;

namespace Ixq.UI.ComponentModel.DataAnnotations
{
    /// <summary>
    ///     JQgrid表格的colModel配置信息约束。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColModelAttribute : Attribute
    {
        /// <summary>
        ///     css样式类。
        /// </summary>
        public string CssClass { get; set; }
        /// <summary>
        ///     列宽。
        /// </summary>
        public int Width { get; set; } = 150;
        /// <summary>
        ///     对齐方式。
        /// </summary>
        public TextAlign Align { get; set; } = TextAlign.Left;
        /// <summary>
        ///     格式化。
        /// </summary>
        public string Formatter { get; set; }
        /// <summary>
        ///     反格式化。
        /// </summary>
        public string UnFormatter { get; set; }
        /// <summary>
        ///     是否可排序。
        /// </summary>
        public bool Sortable { get; set; }
    }
}
