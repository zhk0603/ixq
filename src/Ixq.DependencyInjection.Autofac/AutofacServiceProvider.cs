using System;
using Autofac;

namespace Ixq.DependencyInjection.Autofac
{
    public class AutofacServiceProvider : IServiceProvider
    {
        private readonly IComponentContext _componentContext;

        public AutofacServiceProvider(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public object GetService(Type serviceType)
        {
            return _componentContext.Resolve(serviceType);
        }
    }
}