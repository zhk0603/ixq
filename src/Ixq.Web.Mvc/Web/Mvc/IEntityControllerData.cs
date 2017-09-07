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
    /// <summary>
    ///     实体控制器数据接口，包含有实体控制的基本信息。
    /// </summary>
    public interface IEntityControllerData
    {
        /// <summary>
        ///     获取或设置页面大小集合。
        /// </summary>
        int[] PageSizeList { get; set; }
        /// <summary>
        ///     获取或设置页面配置信息。
        /// </summary>
        IPageConfig PageConfig { get; set; }
        /// <summary>
        ///     获取或设置实体元数据。
        /// </summary>
        IEntityMetadata EntityMetadata { get; set; }
    }
}
