using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.UI.Controls;

namespace Ixq.UI
{
    /// <summary>
    ///     页面配置信息接口。
    /// </summary>
    public interface IPageConfig
    {
        /// <summary>
        ///     获取或设置页面标题。
        /// </summary>
        string Title { get; set; }
        /// <summary>
        ///     获取或设置默认的排序字段。
        /// </summary>
        string DefaultSortname { get; set; }
        /// <summary>
        ///     获取或设置是否倒序排序。
        /// </summary>
        bool IsDescending { get; set; }
        /// <summary>
        ///     获取或设置请求数据的控制器操作名称。
        /// </summary>
        string DataAction { get; set; }
        /// <summary>
        ///     获取或设置查看详情的控制器操作名称。
        /// </summary>
        string DetailAction { get; set; }
        /// <summary>
        ///     获取或设置编辑数据的控制器操作名称。
        /// </summary>
        string EditAction { get; set; }
        /// <summary>
        ///     获取或设置删除数据的控制器操作名。
        /// </summary>
        string DelAction { get; set; }

        /// <summary>
        ///     获取或设置是否可多选。
        /// </summary>
        bool MultiSelect { get; set; }

        /// <summary>
        ///     只有当 <see cref="MultiSelect"/>= true.起作用，当<see cref="MultiBoxOnly"/> 为ture时只有选择checkbox才会起作用
        /// </summary>
        bool MultiBoxOnly { get; set; }

        /// <summary>
        ///     获取或设置是否显示行号。
        /// </summary>
        bool ShowRowNumber { get; set; }

        /// <summary>
        ///     获取或设置加载完后执行的脚本方法。
        /// </summary>
        string OnLoadCompleteScript { get; set; }
        /// <summary>
        ///     获取或设置自定义按钮。
        /// </summary>
        Button[] ButtonCustom { get; set; }
    }
}
