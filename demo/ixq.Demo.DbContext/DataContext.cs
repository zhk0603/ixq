using System.Data.Entity;
using Ixq.Data.Repository;
using Ixq.Demo.Entities;
using Ixq.Security;
using Ixq.Security.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ixq.Demo.DbContext
{
    public class DataContext : IdentityDbContextBase<ApplicationUser, ApplicationRole>
    {
        public DataContext() : base("DataContext")
        {
        }

        public static DataContext Create()
        {
            return new DataContext();
        }

        public IDbSet<ProductType> ProductType { get; set; }
        public IDbSet<Product> Product { get; set; }
    }
}