using System;
using System.Linq;
using Ixq.Demo.Entities;

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

        public static void SeedSysRole(DataContext context)
        {
            var role1 = new ApplicationRole() {Name = "Admin", Description = "具备全部权限的用户组"};

        }
    }
}