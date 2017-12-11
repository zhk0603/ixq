using System;
using Ixq.Core.DependencyInjection;

namespace Ixq.Core
{
    public static class AppBootProgramExtensions
    {
        public static AppBootProgram RegisterService(this AppBootProgram bootProgram, Action<IServiceCollection> action)
        {
            action(bootProgram.ServiceCollection);
            return bootProgram;
        }
    }
}