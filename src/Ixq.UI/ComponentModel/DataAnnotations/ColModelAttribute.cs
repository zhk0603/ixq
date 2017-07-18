using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core;
using Ixq.Core.Data;
using Ixq.Core.Entity;

namespace Ixq.UI.ComponentModel.DataAnnotations
{
    /// <summary>
    ///     JQgrid表格的colModel配置信息约束。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColModelAttribute : Attribute, IDataAnnotations
    {
        /// <summary>
        ///     获取或设置引索，与后台交互的参数。
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        ///     获取或设置表格列的名称。
        /// </summary>
        public string Name { get; set; }

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
        public string FormatterScript { get; set; }
        /// <summary>
        ///     反格式化。
        /// </summary>
        public string UnFormatterScript { get; set; }
        /// <summary>
        ///     是否可排序。
        /// </summary>
        public bool Sortable { get; set; }

        /// <summary>
        ///     获取或设置在初始化表格时是否要隐藏此列。
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        ///     将设置复制到实体元数据对象中。
        /// </summary>
        /// <param name="runtimeProperty">实体元数据</param>
        public void CopyTo(IEntityPropertyMetadata runtimeProperty)
        {
            if (runtimeProperty == null)
                throw new ArgumentNullException(nameof(runtimeProperty));
            runtimeProperty.Width = Width;
            runtimeProperty.Align = Align;
            runtimeProperty.FormatterScript = FormatterScript;
            runtimeProperty.UnFormatterScript = UnFormatterScript;
            runtimeProperty.Sortable = Sortable;
            runtimeProperty.CssClass = CssClass;
        }
    }
}
