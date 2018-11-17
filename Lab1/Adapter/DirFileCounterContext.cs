using System.Data.Entity;
using Lab1.Models;

namespace Lab1.Adapter
{
    class DirFileCounterContext : DbContext
    {
        public DbSet<User> Users { get; set;}
        public DbSet<Request> Requests { get; set;}
        public DirFileCounterContext()
            : base("DbConnection")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new User.UserEntityConfiguration());
            modelBuilder.Configurations.Add(new Request.RequestEntityConfiguration());
        }
    }
}
