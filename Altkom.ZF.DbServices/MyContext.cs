using System;
using System.Linq;
using System.Reflection;
using Altkom.ZF.Models;
using Microsoft.EntityFrameworkCore;


namespace Altkom.ZF.DbServices
{
    public partial class MyContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

      //  public DbQuery<OrderHeader> OrderHeaders { get; set; }

        [DbFunction("ufnGetCountOrder", "dbo")]
        public static int GetCountOrder(int customerId)
        {
            throw new NotSupportedException();
        }       

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


            

         //   modelBuilder.Query<OrderHeader>().ToView("OrderHeaders");

          // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetCallingAssembly());
        }

            public override int SaveChanges()
            {
                var addedCustomers = ChangeTracker.Entries()
                    .Where(p=>p.State == EntityState.Added)
                    .Where(p => p.Entity.GetType() == typeof(Customer));

                foreach(var entry in addedCustomers)
                {
                    entry.Property("CreatedDate").CurrentValue = DateTime.UtcNow;
                }

                return base.SaveChanges();
            } 
    }
}
