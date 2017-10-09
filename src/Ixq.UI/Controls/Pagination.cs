using System.Linq;

namespace Ixq.UI.Controls
{
    /// <summary>
    ///     分页组件。
    /// </summary>
    public class Pagination
    {
        /// <summary>
        ///     获取或设置页面大小。
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     获取或设置页面大小集合。
        /// </summary>
        public int[] PageSizeList { get; set; }

        /// <summary>
        ///     获取或设置默认展示的页面大小。
        /// </summary>
        public int DefualtPageSize { get; set; }

        /// <summary>
        ///     获取或设置当前页。
        /// </summary>
        public int PageCurrent { get; set; }

        /// <summary>
        ///     获取或设置数据总数量。
        /// </summary>
        public ulong Total { get; set; }

        /// <summary>
        ///     获取或设置排序字段。
        /// </summary>
        public string OrderField { get; set; }

        /// <summary>
        ///     获取或设置排序方向。
        /// </summary>
        public string OrderDirection { get; set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string GetPageSizeList()
        {
            var result = PageSizeList.Aggregate("[", (current, i) => current + (i + ","));
            result = result.Substring(0, result.Length - 1);
            result += ']';
            return result;
        }
    }
}