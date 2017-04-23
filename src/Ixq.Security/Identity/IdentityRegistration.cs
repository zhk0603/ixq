using System;
using Ixq.Core;
using Ixq.Core.DependencyInjection;

namespace Ixq.Security.Identity
{
    public static class IdentityRegistration
    {
        public static AppBootProgram RegisterIdentity(this AppBootProgram app, Action<IServiceCollection> action)
        {
            action(app.ServiceCollection);
            return app;
        }
    }
}