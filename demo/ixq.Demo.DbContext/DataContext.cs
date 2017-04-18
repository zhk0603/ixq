using System.Data.Entity;
using Ixq.Data.Repository;
using Ixq.Demo.Entities;

namespace ixq.Demo.DbContext
{
    public class DataContext : DbContextBase
    {
        public IDbSet<ProductType> ProductType { get; set; }
        public IDbSet<Product> Product { get; set; }
    }
}