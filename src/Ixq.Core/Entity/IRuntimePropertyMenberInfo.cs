using System;
using System.Reflection;

namespace Ixq.Core.Entity
{
    public interface IRuntimePropertyMenberInfo
    {
        PropertyInfo PropertyInfo { get; set; }
        Type PropertyType { get; }
        string[] Roles { get; set; }
        string[] Users { get; set; }
        bool IsKey { get; set; }
        bool IsHiddenOnView { get; set; }
        bool IsHiddenOnEdit { get; set; }
        bool IsHiddenOnCreate { get; set; }
        bool IsHiddenOnDetail { get; set; }
        bool IsSearcher { get; set; }
        bool IsRequired { get; set; }
        string Name { get; set; }
        int? Order { get; set; }
        string Description { get; set; }
        string GroupName { get; set; }

        string CssClass { get; set; }
        int Width { get; set; }
        TextAlign Align { get; set; }
        bool Sortable { get; set; }
        string Formatter { get; set; }
        string UnFormatter { get; set; }
        DataType DataType { get; set; }
        string CustomDataType { get; set; }
        string PartialViewPath { get; set; }

        /// <summary>
        ///     获取或设置步长。
        /// </summary>
        double? Step { get; set; }
        /// <summary>
        ///     获取或设置最大值。
        /// </summary>
        long? Max { get; set; }
        /// <summary>
        ///     获取或设置最小值。
        /// </summary>
        long? Min { get; set; }
    }
}