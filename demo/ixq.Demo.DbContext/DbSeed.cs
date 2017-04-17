using System;
using System.Linq;

namespace ixq.Demo.DbContext
{
    public class DbSeed
    {
        public static void SeedProductType(DataContext context)
        {
            if (context.ProductType.Any())
            {
                return;
            }

            for (var i = 0; i < 10; i++)
            {
                var tmp = context.ProductType.Create();
                tmp.OnCreateComplete();
                tmp.Id = Guid.NewGuid();
                tmp.Name = "ProductTypeName" + i;
                context.ProductType.Add(tmp);
            }
            context.SaveChanges();
        }
    }
}