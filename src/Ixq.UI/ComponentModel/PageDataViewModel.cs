using System;
using System.Collections.Generic;
using Ixq.Core.Dto;
using Ixq.Core.Entity;
using Newtonsoft.Json;

namespace Ixq.UI.ComponentModel
{
    /// <summary>
    ///     页面数据模型。
    /// </summary>
    /// <typeparam name="TKey">实体主键类型。</typeparam>
    public class PageDataViewModel<TKey>
    {
        /// <summary>
        ///     初始化一个<see cref="PageDataViewModel{TKey}" />对象。
        /// </summary>
        /// <param name="total">数据总记录数。</param>
        /// <param name="pageCurrent">当前页。</param>
        /// <param name="pageSize">页面大小。</param>
        public PageDataViewModel(int total, int pageCurrent, int pageSize)
        {
            Total = total;
            PageCurrent = pageCurrent;
            TotalPage = (int) Math.Ceiling(total / (double) pageSize);
        }

        /// <summary>
        ///     获取或设置当前页面的数据项。
        /// </summary>
        [JsonProperty("rows")]
        public IEnumerable<IDto<IEntity<TKey>, TKey>> Items { get; set; }

        /// <summary>
        ///     获取总页数。
        /// </summary>
        [JsonProperty("total")]
        public int TotalPage { get; private set; }

        /// <summary>
        ///     获取或设置数据总记录数。
        /// </summary>
        [JsonProperty("records")]
        public int Total { get; set; }

        /// <summary>
        ///     获取设置当前页。
        /// </summary>
        [JsonProperty("page")]
        public int PageCurrent { get; set; }
    }
}