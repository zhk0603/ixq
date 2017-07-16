using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;
using Ixq.Core.Entity;

namespace Ixq.Web.Mvc
{
    /// <summary>
    ///     实体元数据提供者。
    /// </summary>
    public interface IEntityMetadataProvider : IScopeDependency
    {
        /// <summary>
        ///     获取实体元数据。
        /// </summary>
        /// <param name="type">实体类型。</param>
        /// <param name="user">当前已进行身份验证的用户。</param>
        /// <returns></returns>
        IEntityMetadata GetEntityMetadata(Type type);

        /// <summary>
        ///     获取实体元数据。
        /// </summary>
        /// <typeparam name="T">实体类型。</typeparam>
        /// <param name="user">当前已进行身份验证的用户。</param>
        /// <returns></returns>
        IEntityMetadata GetEntityMetadata<T>();
    }
}
