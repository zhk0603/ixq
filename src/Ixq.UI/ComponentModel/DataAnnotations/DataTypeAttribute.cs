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
        public DataType DataType { get; set; }
    }
}
