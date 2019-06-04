using System;
using System.Reflection;
using Altkom.ZF.Models;
using Microsoft.EntityFrameworkCore;


namespace Altkom.ZF.DbServices
{
    public class MyContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public MyContext(DbContextOptions options)
            : base(options)
        {
            
            // this.Database.EnsureDeleted();

            // this.Database.EnsureCreated();

       // this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder
                .ApplyConfiguration(new CustomerConfiguration())
                .ApplyConfiguration(new OrderConfiguration());

          // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetCallingAssembly());
        }
    }
}
