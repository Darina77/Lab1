namespace Lab1.Migrations
{
    using System.Data.Entity.Migrations;
   
    internal sealed class Configuration : DbMigrationsConfiguration<Lab1.DBAdapter.DirFileCounterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Lab1.Adapter.DirFileCounterContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}