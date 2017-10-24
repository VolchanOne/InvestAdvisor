namespace InvestAdvisor.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<InvestAdvisorDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(InvestAdvisorDbContext context)
        {
            //context.Projects.AddOrUpdate(
            //      p => p.Name,
            //      new Project { Name = "Biznet" },
            //      new Project { Name = "X-Traders" },
            //      new Project { Name = "Alt-Trade" }
            //    );
        }
    }
}
