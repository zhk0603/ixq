using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;

namespace Ixq.Core.Data
{
    /// <summary>
    ///     数据注释接口。
    /// </summary>
    public interface IDataAnnotations
    {
        /// <summary>
        ///     将设置复制到实体元数据对象中。
        /// </summary>
        /// <param name="runtimeProperty">实体元数据</param>
        void CopyTo(IEntityPropertyMetadata runtimeProperty);
    }
}
