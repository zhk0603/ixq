using Ixq.Core.Entity;
using Ixq.Core.Mapper;

namespace Ixq.Core.Dto
{
    /// <summary>
    ///     数据传输对象。
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDto<out TEntity, TKey> : IDto
        where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        ///     主键标识。
        /// </summary>
        TKey Id { get; set; }

        IMapper Mapper { get; set; }

        TEntity MapTo();
    }

    public interface IDto
    {
    }
}