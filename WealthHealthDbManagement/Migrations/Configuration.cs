using WealthHealth.Models.Investments;

namespace WealthHealth.DbManagement.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<WealthHealthDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(WealthHealthDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Brokerages.AddOrUpdate(
              new Brokerage { Id = 1, Title = "TRowe Price" },
              new Brokerage { Id = 2, Title = "ShareBuilder" },
              new Brokerage { Id = 3, Title = "Bank Of America" },
              new Brokerage { Id = 4, Title = "ETrade" },
              new Brokerage { Id = 4, Title = "TDAmeritrade" }
            );
        }
    }
}