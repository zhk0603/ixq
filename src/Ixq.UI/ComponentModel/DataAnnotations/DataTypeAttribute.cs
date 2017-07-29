using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core;
using Ixq.Core.Data;
using Ixq.Core.Entity;

namespace Ixq.UI.ComponentModel.DataAnnotations
{
    /// <summary>
    ///     数据类型特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DataTypeAttribute : Attribute , IPropertyMetadataAware
    {
        private DataType _dataType;

        /// <summary>
        ///     获取或设置数据类型
        /// </summary>
        public DataType DataType
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(CustomDataType))
                {
                    return DataType.CustomDataType;
                }
                return _dataType;
            }
            set { _dataType = value; }
        }

        /// <summary>
        ///     获取或设置自定义数据类型。
        /// </summary>
        public string CustomDataType { get; set; }
        /// <summary>
        ///     获取或设置局部视图路径。
        /// </summary>
        public string PartialViewPath { get; set; }
        /// <summary>
        ///     将设置复制到实体元数据对象中。
        /// </summary>
        /// <param name="runtimeProperty">实体元数据</param>
        public void OnPropertyMetadataCreating(IEntityPropertyMetadata runtimeProperty)
        {
            if (runtimeProperty == null)
                throw new ArgumentNullException(nameof(runtimeProperty));
            runtimeProperty.DataType = DataType;
            runtimeProperty.CustomDataType = CustomDataType;
            runtimeProperty.PartialViewPath = PartialViewPath;
        }
    }
}
