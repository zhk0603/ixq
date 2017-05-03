using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core
{
    /// <summary>
    ///     数据类型。
    /// </summary>
    public enum DataType
    {
        Default,
        Text,
        Textarea,
        Image,
        SingleFile,
        MultipleFile,
        PhoneNumber,
        EmailAddress,
        Code,
        Ip,
        Url,
        Color,
        Password,

        Integer,
        Number,
        Currency,

        Select,
        Checkbox,
        Boolean,
        Sex,

        Enum,
        
        DateTime,
        Date,
        Time,

        CustomDataType
    }
}
