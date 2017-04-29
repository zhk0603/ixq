using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
    }
}
