using Ixq.Core.Entity;
using Ixq.Core.Mapper;

namespace Ixq.Core.Dto
{
    /// <summary>
    ///     数据传输对象。
    /// </summary>
    /// <typeparam name="TKey">主键类型。</typeparam>
    /// <typeparam name="TEntity">实体对象。</typeparam>
    public interface IDto<out TEntity, TKey> : IDto
        where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        ///     主键标识。
        /// </summary>
        TKey Id { get; set; }

        /// <summary>
        ///     获取或设置映射器。
        /// </summary>
        IMapper Mapper { get; set; }

        /// <summary>
        ///     将数据传输对象装换为实体对象。
        /// </summary>
        /// <returns></returns>
        TEntity MapTo();
    }

    /// <summary>
    ///     数据传输对象。
    /// </summary>
    public interface IDto
    {
    }
}