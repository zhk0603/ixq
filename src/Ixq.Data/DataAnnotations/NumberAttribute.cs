using System;
using Ixq.Core.DataAnnotations;
using Ixq.Core.Entity;

namespace Ixq.Data.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NumberAttribute : Attribute, IPropertyMetadataAware
    {
        /// <summary>
        ///     获取或设置步长，默认：0.01。
        /// </summary>
        public double Step { get; set; } = 0.01;

        /// <summary>
        ///     获取或设置最大值。
        /// </summary>
        public long Max { get; set; }

        /// <summary>
        ///     获取或设置最小值。
        /// </summary>
        public long Min { get; set; }

        public void OnPropertyMetadataCreating(IEntityPropertyMetadata runtimeProperty)
        {
            if (runtimeProperty == null)
                throw new ArgumentNullException(nameof(runtimeProperty));

            runtimeProperty.Step = Step;
            runtimeProperty.Max = Max;
            runtimeProperty.Min = Min;
        }
    }
}