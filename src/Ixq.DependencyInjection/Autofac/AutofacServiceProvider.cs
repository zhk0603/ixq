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
            if (!_componentContext.IsRegistered(serviceType))
            {
                return null;
            }
            return _componentContext.Resolve(serviceType);
        }

        public object GetService(Type serviceType, string alias)
        {
            if (!_componentContext.IsRegisteredWithName(alias,serviceType))
            {
                return null;
            }
            return _componentContext.ResolveNamed(alias, serviceType);
        }

        public T GetService<T>(string alias)
        {
            if (!_componentContext.IsRegisteredWithName<T>(alias))
            {
                return default(T);
            }
            return _componentContext.ResolveNamed<T>(alias);
        }
    }
}