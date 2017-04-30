using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;
using Ixq.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Ixq.UI.Controls
{
    public class PageViewModel<TEntity, TKey> : IPageViewModel
        where TEntity : class, IEntity<TKey>, new()
    {
        public IRuntimeEntityMenberInfo RuntimeEntityMenberInfo { get; set; }
        public Type EntityType { get; set; }
        public Type DtoType { get; set; }
        public Pagination Pagination { get; set; }

        public IPageConfig PageConfig { get; set; }

        public string GetColNames()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            IRuntimePropertyMenberInfo item = null;
            var length = RuntimeEntityMenberInfo.ViewPropertyInfo.Length;
            for (var i = 0; i < length; i++)
            {
                item = RuntimeEntityMenberInfo.ViewPropertyInfo[i];
                sb.Append($"'{item.Name}'");
                if (i != length - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append("]");
            return sb.ToString();
        }

        public string GetColModel()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            IRuntimePropertyMenberInfo item = null;
            var length = RuntimeEntityMenberInfo.ViewPropertyInfo.Length;
            for (var i = 0; i < length; i++)
            {
                item = RuntimeEntityMenberInfo.ViewPropertyInfo[i];
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
                if (!string.IsNullOrWhiteSpace(item.Formatter))
                {
                    sb.Append($",formatter: {item.Formatter}");
                }
                if (!string.IsNullOrWhiteSpace(item.UnFormatter))
                {
                    sb.Append($",unformatter: {item.UnFormatter}");
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
