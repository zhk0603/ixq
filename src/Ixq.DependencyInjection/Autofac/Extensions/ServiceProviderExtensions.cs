using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.DependencyInjection.Autofac.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider provider, string alias)
        {
            if (provider is AutofacServiceProvider autofacProvider)
            {
                return autofacProvider.GetService<T>(alias);
            }
            return default(T);
        }

        public static object GetService(this IServiceProvider provider, Type serviceType, string alias)
        {
            if (provider is AutofacServiceProvider autofacProvider)
            {
                return autofacProvider.GetService(serviceType, alias);
            }
            return null;
        }
    }
}
