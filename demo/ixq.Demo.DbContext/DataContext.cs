using System.Data.Entity;
using Ixq.Data.Repository;
using Ixq.Demo.Entities;
using Ixq.Security;
using Ixq.Security.Identity;

namespace ixq.Demo.DbContext
{
    public class DataContext : IdentityDbContextBase<ApplicationUser, ApplicationRole>
    {
        public DataContext() : base("DataContext")
        {
        }

        public IDbSet<ProductType> ProductType { get; set; }
        public IDbSet<Product> Product { get; set; }
    }
}