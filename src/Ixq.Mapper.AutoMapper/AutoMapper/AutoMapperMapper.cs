using Ixq.Core.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M=AutoMapper.Mapper;

namespace Ixq.Mapper.AutoMapper
{
    public class AutoMapperMapper : IMapper
    {
        /// <summary>
        /// 将对象映射为指定类型
        /// </summary>
        /// <typeparam name="TTarget">要映射的目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns>目标类型的对象</returns>
        public TTarget MapTo<TTarget>(object source)
        {
            return M.Map<TTarget>(source);
        }

        /// <summary>
        /// 使用源类型的对象更新目标类型的对象
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="target">待更新的目标对象</param>
        /// <returns>更新后的目标类型对象</returns>
        public TTarget MapTo<TSource, TTarget>(TSource source, TTarget target)
        {
            return M.Map(source, target);
        }
    }
}
