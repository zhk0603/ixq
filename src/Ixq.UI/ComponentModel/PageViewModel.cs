using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;
using Ixq.Extensions;
using System.ComponentModel.DataAnnotations;
using Ixq.UI.Controls;

namespace Ixq.UI.ComponentModel
{
    public class PageViewModel : IPageViewModel
    {
        public IEntityMetadata EntityMetadata { get; set; }
        public Type EntityType { get; set; }
        public Type DtoType { get; set; }
        public Pagination Pagination { get; set; }

        public IPageConfig PageConfig { get; set; }

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
