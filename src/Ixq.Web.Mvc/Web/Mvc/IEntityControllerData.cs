using Ixq.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ixq.Core.Entity;
using Ixq.Core.Repository;

namespace Ixq.Web.Mvc
{
    public interface IEntityControllerData
    {
        int[] PageSizeList { get; set; }
        IPageConfig PageConfig { get; set; }
        IEntityMetadata EntityMetadata { get; set; }
    }
}
