namespace Ixq.Demo.Web.Migrations
{
    using ixq.Demo.DbContext;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataContext context)
        {
            DbSeed.SeedProductType(context);
            DbSeed.SeedSysRole(context);
        }
    }
}
