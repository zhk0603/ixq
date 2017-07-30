using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Data.DataAnnotations
{
    /// <summary>
    ///     搜索特性，应用此特性在实体属性上时，在搜索查询条件将包含此实体属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SearcherAttribute :Attribute
    {
    }
}
