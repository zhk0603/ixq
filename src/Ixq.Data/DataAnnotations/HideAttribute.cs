using System;
using Ixq.Core.DataAnnotations;
using Ixq.Core.Entity;

namespace Ixq.Data.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HideAttribute : Attribute, IPropertyMetadataAware
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

        public void OnPropertyMetadataCreating(IEntityPropertyMetadata runtimeProperty)
        {
            if (runtimeProperty == null)
            {
                throw new ArgumentNullException(nameof(runtimeProperty));
            }
            runtimeProperty.IsHiddenOnCreate = IsHiddenOnCreate;
            runtimeProperty.IsHiddenOnDetail = IsHiddenOnDetail;
            runtimeProperty.IsHiddenOnView = IsHiddenOnView;
            runtimeProperty.IsHiddenOnEdit = IsHiddenOnEdit;
        }
    }
}