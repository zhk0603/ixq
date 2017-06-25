using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ixq.UI.Controls
{
    /// <summary>
    /// 分页组件。
    /// </summary>
    public class Pagination
    {
        public int PageSize { get; set; }
        public int[] PageSizeList { get; set; }
        public int DefualtPageSize { get; set; }
        public int PageCurrent { get; set; }
        public int Total { get; set; }
        public string OrderField { get; set; }
        public string OrderDirection { get; set; }

        public string GetPageSizeList()
        {
            var result = PageSizeList.Aggregate("[", (current, i) => current + (i + ","));
            result = result.Substring(0, result.Length - 1);
            result += ']';
            return result;
        }
    }
}
