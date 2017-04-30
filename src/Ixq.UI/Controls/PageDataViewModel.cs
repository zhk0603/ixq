using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Dto;
using Ixq.Core.Entity;
using Newtonsoft.Json;

namespace Ixq.UI.Controls
{
    public class PageDataViewModel<TKey>
    {
        public PageDataViewModel(int total, int pageCurrent,int pageSize)
        {
            this.Total = total;
            this.PageCurrent = pageCurrent;
            this.TotalPage = (int)Math.Ceiling(total / (double)pageSize);
        }

        [JsonProperty("rows")]
        public IEnumerable<IDto<IEntity<TKey>, TKey>> Items { get; set; }

        [JsonProperty("total")]
        public int TotalPage { get; private set; }
        

        [JsonProperty("records")]
        public int Total { get; set;}
        [JsonProperty("page")]
        public int PageCurrent { get; set; }
    }
}
