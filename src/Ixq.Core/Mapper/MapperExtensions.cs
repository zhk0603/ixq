using System;
using Ixq.Core.Dto;
using Ixq.Core.Entity;

namespace Ixq.Core.Mapper
{
    /// <summary>
    ///     映射器扩展。
    /// </summary>
    public static class MapperExtensions
    {
        public static Lazy<IMapper> LazyMapper;
        public static IMapper Instance => LazyMapper.Value;

        /// <summary>
        ///     将 数据对象 转换为 数据传输对象。
        /// </summary>
        /// <typeparam name="TDto">数据传输对象类型。</typeparam>
        /// <typeparam name="TEntity">数据对象类型。</typeparam>
        /// <typeparam name="TKey">主键类型。</typeparam>
        /// <param name="entity">数据对象。</param>
        /// <returns>数据传输对象。</returns>
        public static TDto MapToDto<TDto, TEntity, TKey>(this IEntity<TKey> entity)
            where TEntity : class, IEntity<TKey>, new()
            where TDto : class, IDto<TEntity, TKey>, new()
            where TKey : struct
        {
            return Instance.MapTo<TDto>(entity);
        }

        /// <summary>
        ///     将 数据对象 转换为 数据传输对象。
        /// </summary>
        /// <typeparam name="TDto">数据传输对象类型。</typeparam>
        /// <param name="entity">数据对象。</param>
        /// <returns>数据传输对象。</returns>
        public static TDto MapToDto<TDto>(this IEntity<Guid> entity)
            where TDto : class, IDto<IEntity<Guid>, Guid>, new()
        {
            return Instance.MapTo<TDto>(entity);
        }

        /// <summary>
        ///     将 数据对象 转换为 数据传输对象。
        /// </summary>
        /// <typeparam name="TDto">数据传输对象类型。</typeparam>
        /// <typeparam name="TKey">主键类型。</typeparam>
        /// <param name="entity">数据对象。</param>
        /// <returns>数据传输对象。</returns>
        public static TDto MapToDto<TDto, TKey>(this IEntity<TKey> entity)
            where TDto : class, IDto<IEntity<TKey>, TKey>, new()
            where TKey : struct
        {
            return Instance.MapTo<TDto>(entity);
        }
    }
}