using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Data;
using Ixq.Core.Entity;

namespace Ixq.Data.DataAnnotations
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class HideAttribute : Attribute, IDataAnnotations
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

        public void CopyTo(IEntityPropertyMetadata runtimeProperty)
        {
            if (runtimeProperty == null)
                throw new ArgumentNullException(nameof(runtimeProperty));
            runtimeProperty.IsHiddenOnCreate = IsHiddenOnCreate;
            runtimeProperty.IsHiddenOnDetail = IsHiddenOnDetail;
            runtimeProperty.IsHiddenOnView = IsHiddenOnView;
            runtimeProperty.IsHiddenOnEdit = IsHiddenOnEdit;

        }
    }
}
