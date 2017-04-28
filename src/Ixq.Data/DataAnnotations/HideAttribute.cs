using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Data.DataAnnotations
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class HideAttribute : Attribute
    {
        public HideAttribute()
        {
            IsHiddenOnCreate = true;
            IsHiddenOnDetail = true;
            IsHiddenOnView = true;
            IsHiddenOnEdit = true;
        }
        public bool IsHiddenOnView { get; set; }
        public bool IsHiddenOnEdit { get; set; }
        public bool IsHiddenOnCreate { get; set; }
        public bool IsHiddenOnDetail { get; set; }
    }
}
