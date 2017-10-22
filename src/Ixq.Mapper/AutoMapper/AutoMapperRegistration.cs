using System;
using System.Linq;
using Ixq.Core;
using Ixq.Core.Mapper;

namespace Ixq.Mapper.AutoMapper
{
    public static class AutoMapperRegistration
    {
        public static AppBootProgram RegisterAutoMappe(this AppBootProgram app)
        {
            MapperProvider.SetMapper(new AutoMapperMapper());
            if (!app.MapperCollection.Any())
            {
                return app;
            }
            MapperProvider.Current.Initialize(app.MapperCollection);
            return app;
        }
    }
}