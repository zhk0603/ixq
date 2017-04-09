using ixq.Demo.DbContext;

namespace Ixq.Demo.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ixq.Demo.DbContext.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ixq.Demo.DbContext.DataContext context)
        {
            DbSeed.SeedProductType(context);
        }
    }
}
