using System;
using System.Text;
using Ixq.Core.Entity;
using Ixq.UI.Controls;

namespace Ixq.UI.ComponentModel
{
    /// <summary>
    ///     页面视图模型。
    /// </summary>
    public class PageViewModel : IPageViewModel
    {
        /// <summary>
        ///     获取或设置实体元数据。
        /// </summary>
        public IEntityMetadata EntityMetadata { get; set; }

        /// <summary>
        ///     获取或设置实体类型。
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        ///     获取或设置数据传输对象类型。
        /// </summary>
        public Type DtoType { get; set; }

        /// <summary>
        ///     获取或设置页面分页组件。
        /// </summary>
        public Pagination Pagination { get; set; }

        /// <summary>
        ///     获取或设置页面配置信息。
        /// </summary>
        public IPageConfig PageConfig { get; set; }

        /// <summary>
        ///     生成供页面Jqgrid初始化使用的ColNames信息。
        ///     返回租户可查看实体的属性信息，即实体属性元数据中IsHiddenOnView=true的属性。
        /// </summary>
        /// <returns></returns>
        public virtual string GetColNames()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            IEntityPropertyMetadata item = null;
            var viewProMetadatas = EntityMetadata.ViewPropertyMetadatas;
            var length = viewProMetadatas.Length;
            for (var i = 0; i < length; i++)
            {
                item = viewProMetadatas[i];
                sb.Append($"'{item.Name}'");
                if (i != length - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        ///     生成供页面Jqgrid初始化使用的ColModel信息。
        ///     返回租户可查看实体的属性信息，即实体属性元数据中IsHiddenOnView=true的属性。
        /// </summary>
        /// <returns></returns>
        public virtual string GetColModel()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            IEntityPropertyMetadata item = null;
            var viewProMetadatas = EntityMetadata.ViewPropertyMetadatas;
            var length = viewProMetadatas.Length;
            for (var i = 0; i < length; i++)
            {
                item = viewProMetadatas[i];
                sb.Append("{");

                sb.Append($"name:'{item.PropertyInfo.Name}'" +
                          $",index:'{item.PropertyInfo.Name}'" +
                          $",width: {item.Width}" +
                          $",align: '{item.Align.ToString().ToLower()}'" +
                          $",sortable: {item.Sortable.ToString().ToLower()}");
                if (item.IsKey)
                {
                    sb.Append(",hidedlg: true,key: true");
                }
                if (!string.IsNullOrWhiteSpace(item.CssClass))
                {
                    sb.Append($",classes: '{item.CssClass}'");
                }
                if (!string.IsNullOrWhiteSpace(item.FormatterScript))
                {
                    sb.Append($",formatter: {item.FormatterScript}");
                }
                if (!string.IsNullOrWhiteSpace(item.UnFormatterScript))
                {
                    sb.Append($",unformatter: {item.UnFormatterScript}");
                }

                sb.Append("}");
                if (i != length - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}