using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.UI.Controls;

namespace Ixq.UI.ComponentModel.DataAnnotations
{
    /// <summary>
    ///     页面配置属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PageAttribute : Attribute, IPageConfig
    {
        /// <summary>
        ///     标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     默认排序字段名称。
        /// </summary>
        public string DefaultSortname { get; set; }

        /// <summary>
        ///     是否降序排序。
        /// </summary>
        public bool IsDescending { get; set; }

        /// <summary>
        ///     Ajax请求数据的 Actin 名。
        /// </summary>
        public string DataAction { get; set; } = "List";

        /// <summary>
        ///     保存编辑、添加数据的  Actin 名。
        /// </summary>
        public string EditAction { get; set; } = "Edit";

        /// <summary>
        ///     删除数据的  Actin 名。
        /// </summary>
        public string DelAction { get; set; } = "Delete";

        /// <summary>
        ///     自定义Button。
        /// </summary>
        public Button[] ButtonCustom { get; set; }

        /// <summary>
        ///     获取或设置是否可多选。
        /// </summary>
        public bool MultiSelect { get; set; }

        /// <summary>
        ///     只有当 <see cref="MultiSelect"/>= true.起作用，当<see cref="MultiBoxOnly"/> 为ture时只有选择checkbox才会起作用
        /// </summary>
        public bool MultiBoxOnly { get; set; }

        /// <summary>
        ///     获取或设置是否显示行号。
        /// </summary>
        public bool ShowRowNumber { get; set; }
        /// <summary>
        ///     获取或设置加载完成后执行的JavaScript方法。
        /// </summary>

        public string OnLoadCompleteScript { get; set; }

    }
}
