using System.Data.Entity;
using Ixq.Data.Repository;
using Ixq.Demo.Entities;

namespace ixq.Demo.DbContext
{
    public class DataContext : DbContextBase
    {
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}