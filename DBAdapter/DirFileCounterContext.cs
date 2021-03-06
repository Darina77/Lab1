﻿using System.Data.Entity;
using Lab1.DBAdapter.Migrations;
using Lab1.DBModels;

namespace Lab1.DBAdapter
{
    class DirFileCounterContext : DbContext
    {
        public DbSet<User> Users { get; set;}
        public DbSet<Request> Requests { get; set;}
        public DirFileCounterContext()
            : base("DbConnection")
        { 
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<DirFileCounterContext, Configuration>(true));
		}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new User.UserEntityConfiguration());
            modelBuilder.Configurations.Add(new Request.RequestEntityConfiguration());
        }
    }
}
