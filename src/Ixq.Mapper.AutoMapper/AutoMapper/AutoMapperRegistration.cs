using System;
using System.Linq;
using System.Web;
using Ixq.Core;
using Ixq.Core.Mapper;

namespace Ixq.Mapper.AutoMapper
{
    public static class AutoMapperRegistration
    {
        public static AppBootProgram<T> RegisterAutoMappe<T>(this AppBootProgram<T> app)
            where T : HttpApplication, new()
        {
            MapperExtensions.LazyMapper = new Lazy<IMapper>(() => new AutoMapperMapper());
            if (!app.MapperCollection.Any()) return app;
            MapperExtensions.Instance.Initialize(app.MapperCollection);
            return app;
        }
    }
}