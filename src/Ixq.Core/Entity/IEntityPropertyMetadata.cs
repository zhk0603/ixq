using System;
using System.Reflection;
using System.Security.Principal;

namespace Ixq.Core.Entity
{
    /// <summary>
    ///     实体属性元数据。
    /// </summary>
    public interface IEntityPropertyMetadata
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
        ///     获取或设置实体属性授权角色。
        /// </summary>
        string[] Roles { get; set; }

        /// <summary>
        ///     获取或设置实体属性授权用户。
        /// </summary>
        string[] Users { get; set; }

        /// <summary>
        ///     获取或设置实体属性是否为主键。
        /// </summary>
        bool IsKey { get; set; }

        /// <summary>
        ///     获取或设置实体属性是否能搜索。
        /// </summary>
        bool IsSearcher { get; set; }

        /// <summary>
        ///     获取或设置实体属性在新增或编辑是否为必须。
        /// </summary>
        bool IsRequired { get; set; }

        #region HideAttribute
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

        #endregion

        #region DisplayAttribute
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

        #endregion

        #region ColModelAttribute

        /// <summary>
        ///     获取或设置引索，与后台交互的参数。
        /// </summary>
        string ColModelIndex { get; set; }

        /// <summary>
        ///     获取或设置表格列的名称。
        /// </summary>
        string ColModelName { get; set; }

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
        ///     获取或设置格式化脚本方法。
        /// </summary>
        string FormatterScript { get; set; }

        /// <summary>
        ///     获取或设置反格式化脚本方法。
        /// </summary>
        string UnFormatterScript { get; set; }

        /// <summary>
        ///     获取或设置在初始化表格时是否要隐藏此列。
        /// </summary>
        bool Hidden { get; set; }

        #endregion

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

        /// <summary>
        ///     确认用户是否有有访问此实体属性的权限。
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool IsAuthorization(IPrincipal user);
    }
}