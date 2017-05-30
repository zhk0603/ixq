using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Entity
{
    /// <summary>
    ///     运行时实体元数据。
    /// </summary>
    public interface IRuntimeEntityMenberInfo
    {
        /// <summary>
        ///     获取实体在列表中的属性。
        /// </summary>
        IRuntimePropertyMenberInfo[] ViewPropertyInfo { get; }

        /// <summary>
        ///     获取实体在创建时的属性。
        /// </summary>
        IRuntimePropertyMenberInfo[] CreatePropertyInfo { get; }

        /// <summary>
        ///     获取实体在编辑时的属性。
        /// </summary>
        IRuntimePropertyMenberInfo[] EditPropertyInfo { get; }

        /// <summary>
        ///     获取在查看实体详情时的属性。
        /// </summary>
        IRuntimePropertyMenberInfo[] DetailPropertyInfo { get; }

        /// <summary>
        ///     获取能搜索实体的属性。
        /// </summary>
        IRuntimePropertyMenberInfo[] SearcherPropertyInfo { get; }
    }
}
