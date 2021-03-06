﻿using Autofac;
using Ixq.Core.DependencyInjection;

namespace Ixq.DependencyInjection.Autofac
{
    public class AutofacServiceScopeFactory : IServiceScopeFactory
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacServiceScopeFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public IServiceScope CreateScope()
        {
            return new AutofacServiceScope(_lifetimeScope.BeginLifetimeScope());
        }
    }
}