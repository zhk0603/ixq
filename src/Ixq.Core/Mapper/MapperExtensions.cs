using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Dto;
using Ixq.Core.Entity;

namespace Ixq.Core.Mapper
{
    public static class MapperExtensions
    {
        public static Lazy<IMapper> LazyMapper;
        public static IMapper Instance => LazyMapper.Value;

        /// <summary>
        /// 将 数据对象 转换为 数据传输对象。
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TDto MapToDto<TDto, TEntity>(this TEntity entity)
            where TEntity : class, IEntity<Guid>, new()
            where TDto : class, IDto<TEntity, Guid>, new()
        {
            return Instance.MapTo<TDto>(entity);
        }
    }
}
