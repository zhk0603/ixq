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
        ///     获取或设置页面标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     获取或设置默认的排序字段。
        /// </summary>
        public string DefaultSortname { get; set; }

        /// <summary>
        ///     获取或设置是否倒序排序。
        /// </summary>
        public bool IsDescending { get; set; }

        /// <summary>
        ///     获取或设置请求数据的控制器操作名称。
        /// </summary>
        public string DataAction { get; set; } = "List";

        /// <summary>
        ///     获取或设置查看详情的控制器操作名称。
        /// </summary>
        public string DetailAction { get; set; } = "Detail";

        /// <summary>
        ///     获取或设置编辑数据的控制器操作名称。
        /// </summary>
        public string EditAction { get; set; } = "Edit";

        /// <summary>
        ///     获取或设置删除数据的控制器操作名。
        /// </summary>
        public string DelAction { get; set; } = "Delete";

        /// <summary>
        ///     获取或设置自定义按钮。
        /// </summary>
        public Button[] ButtonCustom { get; set; }

        /// <summary>
        ///     获取或设置是否可多选，默认是true。
        /// </summary>
        public bool MultiSelect { get; set; } = true;

        /// <summary>
        ///     只有当 <see cref="MultiSelect"/>= true.起作用，当<see cref="MultiBoxOnly"/> 为ture时只有选择checkbox才会起作用，默认是true。
        /// </summary>
        public bool MultiBoxOnly { get; set; } = true;

        /// <summary>
        ///     获取或设置是否显示行号。
        /// </summary>
        public bool ShowRowNumber { get; set; }
        /// <summary>
        ///     获取或设置加载完后执行的脚本方法。
        /// </summary>

        public string OnLoadCompleteScript { get; set; }

    }
}
