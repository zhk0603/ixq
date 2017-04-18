using System;
using System.Linq;
using System.Web;
using Ixq.Core;
using Ixq.Core.Mapper;

namespace Ixq.Mapper.AutoMapper
{
    public static class AutoMapperRegistration
    {
        public static AppBootProgram RegisterAutoMappe(this AppBootProgram app)
        {
            MapperExtensions.LazyMapper = new Lazy<IMapper>(() => new AutoMapperMapper());
            if (!app.MapperCollection.Any()) return app;
            MapperExtensions.Instance.Initialize(app.MapperCollection);
            return app;
        }
    }
}