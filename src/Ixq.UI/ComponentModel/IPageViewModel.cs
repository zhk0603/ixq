using System;
using Ixq.Core.Entity;
using Ixq.UI.Controls;

namespace Ixq.UI.ComponentModel
{
    /// <summary>
    ///     页面视图模型接口。
    /// </summary>
    public interface IPageViewModel
    {
        /// <summary>
        ///     获取或设置实体元数据。
        /// </summary>
        IEntityMetadata EntityMetadata { get; set; }

        /// <summary>
        ///     获取或设置实体类型。
        /// </summary>
        Type EntityType { get; set; }

        /// <summary>
        ///     获取或设置数据传输对象类型。
        /// </summary>
        Type DtoType { get; set; }

        /// <summary>
        ///     获取或设置页面配置信息。
        /// </summary>
        IPageConfig PageConfig { get; set; }

        /// <summary>
        ///     获取或设置页面分页组件。
        /// </summary>
        Pagination Pagination { get; set; }

        /// <summary>
        ///     生成供页面Jqgrid初始化使用的ColNames信息。
        ///     返回租户可查看实体的属性信息，即实体属性元数据中IsHiddenOnView=true的属性。
        /// </summary>
        /// <returns></returns>
        string GetColNames();

        /// <summary>
        ///     生成供页面Jqgrid初始化使用的ColModel信息。
        ///     返回租户可查看实体的属性信息，即实体属性元数据中IsHiddenOnView=true的属性。
        /// </summary>
        /// <returns></returns>
        string GetColModel();
    }
}