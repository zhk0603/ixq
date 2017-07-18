using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;

namespace Ixq.UI.ComponentModel
{
    /// <summary>
    ///     页面编辑模型接口。
    /// </summary>
    public interface IPageEditViewModel
    {
        /// <summary>
        ///     获取或设置标题。
        /// </summary>
        string Title { get; set; }
        /// <summary>
        ///     获取或设置实体数据传输对象。
        /// </summary>
        object EntityDto { get; set; }
        /// <summary>
        ///     获取或设置实体属性元数据。
        /// </summary>
        IEntityPropertyMetadata[] PropertyMenberInfo { get; set; }
    }
}
