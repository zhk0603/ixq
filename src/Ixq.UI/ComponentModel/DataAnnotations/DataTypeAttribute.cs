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
    public class DataTypeAttribute : Attribute , IDataAnnotations
    {
        private DataType _dataType;

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

        public string CustomDataType { get; set; }
        public string PartialViewPath { get; set; }
        public void SetRuntimeProperty(IRuntimePropertyMenberInfo runtimeProperty)
        {
            if (runtimeProperty == null)
                throw new ArgumentNullException(nameof(runtimeProperty));
            runtimeProperty.DataType = DataType;
            runtimeProperty.CustomDataType = CustomDataType;
            runtimeProperty.PartialViewPath = PartialViewPath;
        }
    }
}
