using System;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using Ixq.Core;
using Ixq.Core.Entity;

namespace Ixq.Web.Mvc
{
    /// <summary>
    ///     实体属性元数据。
    /// </summary>
    public class EntityPropertyMetadata : IEntityPropertyMetadata
    {
        /// <summary>
        ///     初始化一个<see cref="EntityPropertyMetadata" />对象。
        /// </summary>
        /// <param name="dtoPropertyInfo"></param>
        public EntityPropertyMetadata(PropertyInfo dtoPropertyInfo)
        {
            if (dtoPropertyInfo == null)
                throw new ArgumentNullException(nameof(dtoPropertyInfo));

            PropertyInfo = dtoPropertyInfo;
            PropertyType = dtoPropertyInfo.PropertyType;
            PropertyName = PropertyInfo.Name;
        }


        /// <summary>
        ///     获取Dto属性名称。
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        ///     获取Dto属性信息。
        /// </summary>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        ///     获取Dto属性类型。
        /// </summary>
        public Type PropertyType { get; }

        /// <summary>
        ///     获取或设置实体属性授权角色。
        /// </summary>
        public string[] Roles { get; set; }

        /// <summary>
        ///     获取或设置实体属性授权用户。
        /// </summary>
        public string[] Users { get; set; }

        /// <summary>
        ///     获取或设置实体属性是否为主键。
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>
        ///     获取或设置实体属性是否隐藏在列表。
        /// </summary>
        public bool IsHiddenOnView { get; set; }

        /// <summary>
        ///     获取或设置实体属性是否隐藏在编辑时。
        /// </summary>
        public bool IsHiddenOnEdit { get; set; }

        /// <summary>
        ///     获取或设置实体属性是否隐藏在新增时。
        /// </summary>
        public bool IsHiddenOnCreate { get; set; }

        /// <summary>
        ///     获取或设置实体属性是否隐藏在查看详情时。
        /// </summary>
        public bool IsHiddenOnDetail { get; set; }

        /// <summary>
        ///     获取或设置实体属性是否能搜索。
        /// </summary>
        public bool IsSearcher { get; set; }

        /// <summary>
        ///     获取或设置实体属性在新增或编辑是否为必须。
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        ///     获取或设置实体属性的数据类型。
        /// </summary>
        public DataType DataType { get; set; }

        /// <summary>
        ///     获取或设置实体属性的自定义数据类型。
        /// </summary>
        public string CustomDataType { get; set; }

        /// <summary>
        ///     获取或设置实体属性局部视图路径。
        /// </summary>
        public string PartialViewPath { get; set; }

        /// <summary>
        ///     获取或设置步长，默认：0.01。
        /// </summary>
        public double? Step { get; set; } = 0.01;

        /// <summary>
        ///     获取或设置最大值。
        /// </summary>
        public long? Max { get; set; }

        /// <summary>
        ///     获取或设置最小值。
        /// </summary>
        public long? Min { get; set; }

        /// <summary>
        ///     确认用户是否有有访问此实体属性的权限。
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsAuthorization(IPrincipal user)
        {
            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
                return false;

            if (Users != null && Users.Any() && !Users.Contains(user.Identity.Name, StringComparer.OrdinalIgnoreCase))
                return false;
            if (Roles != null && !Roles.Any(user.IsInRole))
                return false;
            return true;
        }

        #region DisplayAttribute

        /// <summary>
        ///     获取或设置实体属性的显示名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置实体属性的排序顺序。
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        ///     获取或设置实体属性的描述信息。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     获取或设置实体属性的组名。
        /// </summary>
        public string GroupName { get; set; }

        #endregion

        #region ColModelAttribute

        /// <summary>
        ///     获取或设置引索，与后台交互的参数。
        /// </summary>
        public string ColModelIndex { get; set; }

        /// <summary>
        ///     获取或设置表格列的名称。
        /// </summary>
        public string ColModelName { get; set; }

        /// <summary>
        ///     获取或设置实体属性的css类。
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        ///     获取或设置实体属性的列宽。
        /// </summary>
        public int Width { get; set; } = 150;

        /// <summary>
        ///     获取或设置实体属性在列表时的文字对齐格式。
        /// </summary>
        public TextAlign Align { get; set; } = TextAlign.Center;

        /// <summary>
        ///     获取或设置实体属性是否能排序。
        /// </summary>
        public bool Sortable { get; set; }

        /// <summary>
        ///     获取或设置格式化脚本方法。
        /// </summary>
        public string FormatterScript { get; set; }

        /// <summary>
        ///     获取或设置反格式化脚本方法。
        /// </summary>
        public string UnFormatterScript { get; set; }

        /// <summary>
        ///     获取或设置在初始化表格时是否要隐藏此列。
        /// </summary>
        public bool Hidden { get; set; }

        #endregion
    }
}