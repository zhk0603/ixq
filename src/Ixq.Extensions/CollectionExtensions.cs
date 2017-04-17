using System.ComponentModel;
using Ixq.Extensions;

namespace System.Linq
{
    /// <summary>
    ///     集合扩展方法类
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///     按指定的属性名称对<see cref="IQueryable{T}" />序列进行排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source,
            string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            dynamic keySelector = LambdaExpression<T>.GetKeySelector(propertyName);
            return sortDirection == ListSortDirection.Ascending
                ? Queryable.OrderBy(source, keySelector)
                : Queryable.OrderByDescending(source, keySelector);
        }
    }
}