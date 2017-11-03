using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
