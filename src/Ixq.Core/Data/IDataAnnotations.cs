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
        ///     初始化 <see cref="IRuntimePropertyMenberInfo"/>。
        /// </summary>
        /// <param name="runtimeProperty"></param>
        void SetRuntimeProperty(IRuntimePropertyMenberInfo runtimeProperty);
    }
}
