using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
