using System.Data.Entity.Migrations;

namespace Lab1.DBAdapter.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DirFileCounterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DirFileCounterContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
