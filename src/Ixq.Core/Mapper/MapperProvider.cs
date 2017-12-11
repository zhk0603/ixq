using System;

namespace Ixq.Core.Mapper
{
    public class MapperProvider
    {
        private static IMapper _mapper;
        private static Func<IMapper> _getMapper;
        public static IMapper Current => _mapper ?? (_mapper = _getMapper());

        public static void SetMapper(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public static void SetMapper(Func<IMapper> getMapper)
        {
            _getMapper = getMapper;
        }
    }
}