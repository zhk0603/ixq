using System;
using System.Data.Entity;
using Ixq.Data.Repository;
using Ixq.Demo.Entities;
using Ixq.Demo.Entities.System;
using Ixq.Security.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ixq.Demo.DbContext
{
    public class DataContext : IdentityDbContextBase<ApplicationUser>
    {
        public DataContext() : base("DataContext")
        {
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