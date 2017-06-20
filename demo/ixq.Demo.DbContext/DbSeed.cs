using System;
using System.Linq;
using Ixq.Demo.Entities;
using Ixq.Security.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ixq.Demo.DbContext
{
    public class DbSeed
    {
        public static void SeedProductType(DataContext context)
        {
            if (context.ProductTypes.Any())
            {
                return;
            }
            //for (var i = 0; i < 10000; i++)
            //{
                for (var j = 0; j < 1000; j++)
                {
                    var tmp = context.ProductTypes.Create();
                    tmp.OnCreateComplete();
                    tmp.Id = Guid.NewGuid();
                    tmp.Name = "ProductTypeName" + j;
                    context.ProductTypes.Add(tmp);
                }
                context.SaveChanges();
            //}

        }



        public static void SeedSysRole(DataContext context)
        {
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var adminRole = new ApplicationRole() {Name = "Admin", Description = "具备全部权限的用户组", CreateDate = DateTime.Now};
            var supervisorRole = new ApplicationRole() {Name = "Supervisor", Description = "主管的用户组", CreateDate = DateTime.Now};
            if (!roleManager.RoleExists(adminRole.Name))
                roleManager.Create(adminRole);
            if (!roleManager.RoleExists(supervisorRole.Name))
                roleManager.Create(supervisorRole);

            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Age = 20,
                CreateDate = DateTime.Now,
                PhoneNumber = "",
            };
            userManager.Create(adminUser, "123@Abc");
            var zkUser = new ApplicationUser
            {
                UserName = "zhaokun",
                Age = 20,
                CreateDate = DateTime.Now,
                PhoneNumber = "",
            };
            userManager.Create(zkUser, "123@Abc");
            context.SaveChanges();
            
            userManager.AddToRole(adminUser.Id, adminRole.Name);
            userManager.AddToRole(zkUser.Id, supervisorRole.Name);

            context.SaveChanges();
        }
    }

    public class UserStore<TUser> : UserStoreBase<TUser, ApplicationRole>
    where TUser : IdentityUser
    {
        public UserStore(System.Data.Entity.DbContext context) : base(context)
        {
        }
    }
}