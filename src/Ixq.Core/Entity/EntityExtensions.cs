using System;
using System.Reflection;

namespace Ixq.Core.Entity
{
    /// <summary>
    ///     实体扩展方法。
    /// </summary>
    public class EntityExtensions
    {
        /// <summary>
        ///     获取实体属性的数据类型。
        /// </summary>
        /// <param name="propertyInfo">实体属性。</param>
        /// <returns></returns>
        public static DataType GetDataType(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException(nameof(propertyInfo));

            var propertyType = propertyInfo.PropertyType;
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                propertyType = propertyType.GetGenericArguments()[0];

            var type = DataType.Default;
            if (propertyType == typeof(string))
                type = DataType.Text;
            else if (propertyType == typeof(DateTime))
                type = DataType.Date;
            else if (propertyType == typeof(TimeSpan))
                type = DataType.Time;
            else if (propertyType == typeof(bool))
                type = DataType.Boolean;
            else if (propertyType == typeof(decimal))
                type = DataType.Currency;
            else if (propertyType == typeof(short) || propertyType == typeof(int) || propertyType == typeof(long) ||
                     propertyType == typeof(ushort) || propertyType == typeof(uint) || propertyType == typeof(ulong))
                type = DataType.Integer;
            else if (propertyType == typeof(float) || propertyType == typeof(double))
                type = DataType.Number;
            else if (propertyType.IsEnum)
                type = DataType.Enum;

            return type;
        }
    }
}