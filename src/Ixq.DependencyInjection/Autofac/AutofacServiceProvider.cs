using System;
using Autofac;
using Ixq.Core.DependencyInjection;

namespace Ixq.DependencyInjection.Autofac
{
    public class AutofacServiceProvider : IServiceProvider, ISupportAliasServiceProvider
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
    }
}