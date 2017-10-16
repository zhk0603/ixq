using System;
using System.Data.Entity;
using Ixq.Data.Repository;
using Ixq.Demo.Entities;
using Ixq.Demo.Entities.System;
using Ixq.Security;
using Ixq.Security.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ixq.Demo.DbContext
{
    public class DataContext : IdentityDbContextBase<ApplicationUser, ApplicationRole>
    {
        public DataContext() : base("DataContext")
        {
            Initialize();
            Configuration.ValidateOnSaveEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static DataContext Create()
        {
            return new DataContext();
        }

        public IDbSet<ProductType> ProductTypes { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<SystemMenu> SystemMenus { get; set; }
        public IDbSet<Test> Tests { get; set; }
    }
}