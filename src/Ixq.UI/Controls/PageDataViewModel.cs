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
    public class PageDataViewModel
    {
        [JsonProperty("rows")]
        public IEnumerable<IDto<IEntity<Guid>, Guid>> List { get; set; }
        [JsonProperty("total")]
        public int TotalPage { get; set; }
        [JsonProperty("records")]
        public int Total { get; set;}
        [JsonProperty("page")]
        public int PageCurrent { get; set; }
    }
}
