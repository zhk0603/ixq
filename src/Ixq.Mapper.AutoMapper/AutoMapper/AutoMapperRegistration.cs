using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core;
using System.Web;

namespace Ixq.Mapper.AutoMapper
{
    public static class AutoMapperRegistration
    {
        public static AppBootProgram<T> RegisterAutoMappe<T>(this AppBootProgram<T> app)
            where T : HttpApplication, new()
        {
            var mapper = new AutoMapperMapper();
            if (!app.MapperCollection.Any()) return app;
            foreach (var descriptor in app.MapperCollection)
            {
                mapper.CreateMap(descriptor.SourceType, descriptor.TargetType);
                mapper.CreateMap(descriptor.TargetType, descriptor.SourceType);
            }
            return app;
        }
    }
}
