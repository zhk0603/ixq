using System;
using System.Collections.Generic;
using Ixq.Core.Mapper;
using M = AutoMapper.Mapper;
using AM = AutoMapper;

namespace Ixq.Mapper.AutoMapper
{
    public class AutoMapperMapper : IMapper
    {
        /// <summary>
        ///     将对象映射为指定类型
        /// </summary>
        /// <typeparam name="TTarget">要映射的目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns>目标类型的对象</returns>
        public TTarget MapTo<TTarget>(object source)
        {
            return M.Instance.Map<TTarget>(source);
        }

        /// <summary>
        ///     使用源类型的对象更新目标类型的对象
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="target">待更新的目标对象</param>
        /// <returns>更新后的目标类型对象</returns>
        public TTarget MapTo<TSource, TTarget>(TSource source, TTarget target)
        {
            return M.Instance.Map(source, target);
        }
        public void Initialize(IEnumerable<MapperDescriptor> mapperCollection)
        {
            M.Initialize(cfg =>
            {
                foreach (var descriptor in mapperCollection)
                {
                    cfg.CreateMap(descriptor.SourceType, descriptor.TargetType);
                    cfg.CreateMap(descriptor.TargetType, descriptor.SourceType);
                }
            });
        }
    }
}