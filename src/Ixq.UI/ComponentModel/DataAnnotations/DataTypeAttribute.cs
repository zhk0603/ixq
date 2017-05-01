using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core;

namespace Ixq.UI.ComponentModel.DataAnnotations
{
    /// <summary>
    ///     数据类型特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DataTypeAttribute : Attribute
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
    }
}
