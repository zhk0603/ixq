using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ixq.Demo.Web.Models
{
    public interface IAnimal : Ixq.Core.DependencyInjection.ISingletonDependency
    {
        void Say();
    }

    [Ixq.Core.DependencyInjection.ServiceAlias("Tiger")]
    public class Tiger : IAnimal
    {
        public void Say()
        {
            System.Console.WriteLine("I'm tiger");
        }
    }

    [Ixq.Core.DependencyInjection.ServiceAlias("bird")]
    public class Bird : IAnimal
    {
        public virtual void Say()
        {
            System.Console.WriteLine("I'm bird");
        }
    }

    [Ixq.Core.DependencyInjection.ServiceAlias("bird")]
    public class OldBird : Bird
    {
        public override void Say()
        {
            Console.WriteLine("I'm OldBird");
        }
    }
}