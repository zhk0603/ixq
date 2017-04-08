using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Extended
{
    public class LambdaExpression<T>
    {
        private static readonly ConcurrentDictionary<string, LambdaExpression> cache =
            new ConcurrentDictionary<string, LambdaExpression>();

        public static LambdaExpression GetKeySelector(string keyName)
        {
            var type = typeof(T);
            var key = type.FullName + "." + keyName;
            if (cache.ContainsKey(key))
            {
                return cache[key];
            }
            var param = Expression.Parameter(type);
            var propertyNames = keyName.Split('.');
            Expression propertyAccess = param;
            foreach (var propertyName in propertyNames)
            {
                var property = type.GetProperty(propertyName);
                if (property == null)
                {
                    throw new Exception($"指定对象中不存在名称为“{propertyName}”的属性。");
                }
                type = property.PropertyType;
                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
            }
            var keySelector = Expression.Lambda(propertyAccess, param);
            cache[key] = keySelector;
            return keySelector;
        }
    }
}
