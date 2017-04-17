using System.Data.Entity.Migrations;
using ixq.Demo.DbContext;

namespace Ixq.Demo.Web.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataContext context)
        {
            DbSeed.SeedProductType(context);
        }
    }
}