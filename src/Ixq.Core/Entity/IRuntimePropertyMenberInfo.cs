using System;
using System.Reflection;

namespace Ixq.Core.Entity
{
    /// <summary>
    ///     运行时实体属性元数据。
    /// </summary>
    public interface IRuntimePropertyMenberInfo
    {
        /// <summary>
        ///     获取或设置实体属性。
        /// </summary>
        PropertyInfo PropertyInfo { get; set; }

        /// <summary>
        ///     获取实体属性类型。
        /// </summary>
        Type PropertyType { get; }

        /// <summary>
        ///     获取或设置实体属性归属角色。
        /// </summary>
        string[] Roles { get; set; }

        /// <summary>
        ///     获取或设置实体属性归属用户。
        /// </summary>
        string[] Users { get; set; }

        /// <summary>
        ///     获取或设置实体属性是否为主键。
        /// </summary>
        bool IsKey { get; set; }

        /// <summary>
        ///     获取或设置实体属性是否隐藏在列表。
        /// </summary>
        bool IsHiddenOnView { get; set; }

        /// <summary>
        ///     获取或设置实体属性是否隐藏在编辑时。
        /// </summary>
        bool IsHiddenOnEdit { get; set; }

        /// <summary>
        ///     获取或设置实体属性是否隐藏在新增时。
        /// </summary>
        bool IsHiddenOnCreate { get; set; }

        /// <summary>
        ///     获取或设置实体属性是否隐藏在查看详情时。
        /// </summary>
        bool IsHiddenOnDetail { get; set; }

        /// <summary>
        ///     获取或设置实体属性是否能搜索。
        /// </summary>
        bool IsSearcher { get; set; }

        /// <summary>
        ///     获取或设置实体属性在新增或编辑是否为必须。
        /// </summary>
        bool IsRequired { get; set; }

        /// <summary>
        ///     获取或设置实体属性的显示名称。
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     获取或设置实体属性的排序顺序。
        /// </summary>
        int? Order { get; set; }

        /// <summary>
        ///     获取或设置实体属性的描述信息。
        /// </summary>
        string Description { get; set; }

        /// <summary>
        ///     获取或设置实体属性的组名。
        /// </summary>
        string GroupName { get; set; }

        /// <summary>
        ///     获取或设置实体属性的css类。
        /// </summary>
        string CssClass { get; set; }

        /// <summary>
        ///     获取或设置实体属性的列宽。
        /// </summary>
        int Width { get; set; }

        /// <summary>
        ///     获取或设置实体属性在列表时的文字对齐格式。
        /// </summary>
        TextAlign Align { get; set; }

        /// <summary>
        ///     获取或设置实体属性是否能排序。
        /// </summary>
        bool Sortable { get; set; }

        /// <summary>
        ///     获取或设置实体属性的格式化Javascript方法名称。
        /// </summary>
        string Formatter { get; set; }

        /// <summary>
        ///     获取或设置实体属性的反格式化Javascript方法名称。
        /// </summary>
        string UnFormatter { get; set; }

        /// <summary>
        ///     获取或设置实体属性的数据类型。
        /// </summary>
        DataType DataType { get; set; }

        /// <summary>
        ///     获取或设置实体属性的自定义数据类型。
        /// </summary>
        string CustomDataType { get; set; }

        /// <summary>
        ///     获取或设置实体属性局部视图路径。
        /// </summary>
        string PartialViewPath { get; set; }

        /// <summary>
        ///     获取或设置实体属性的步长。
        /// </summary>
        double? Step { get; set; }

        /// <summary>
        ///     获取或设置实体属性的最大值。
        /// </summary>
        long? Max { get; set; }

        /// <summary>
        ///     获取或设置实体属性的最小值。
        /// </summary>
        long? Min { get; set; }
    }
}