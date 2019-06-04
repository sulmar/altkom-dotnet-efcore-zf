using System;
using System.Threading.Tasks;
using Altkom.ZF.DbServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Altkom.ZF.Models;
using System.Linq;

namespace Altkom.ZF.ConsoleClient
{
    // dotnet add package Microsoft.EntityFrameworkCore.Design
    public class MyContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();

            // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            optionsBuilder.UseSqlServer(connectionString); 

            return new MyContext(optionsBuilder.Options);
        }
    }

    class Program
    {

        // C# 7.0
        static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Hello .NET Core!!!!"); 

            GetMetaDataTest();

            AddEntityTest();

           // await CreateDbTestAsync();

            AuditTest();

            LocalNoTrackingTest();


             System.Console.WriteLine("Press any key to exit.");

             Console.ReadLine();
        }


        private static void GetMetaDataTest()
        {
            System.Console.WriteLine("Metadata");
            System.Console.WriteLine("-----------");

            string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(connectionString);
     
            using(var context = new MyContext(optionsBuilder.Options))
            {
                foreach (var entityType in context.Model.GetEntityTypes())
                {
                    var tableName = entityType.Relational().TableName;

                    foreach (var propertyType in entityType.GetProperties())
                    {
                        var columnName = propertyType.Relational().ColumnName;

                        System.Console.WriteLine($"{tableName} {columnName}");
                    }
                }
            }

        }

       
        private static void LocalNoTrackingTest()
        {
             string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(connectionString);
     
            using(var context = new MyContext(optionsBuilder.Options))
            {
               var customers = context.Customers.AsNoTracking().ToList();

                foreach(var entry in context.ChangeTracker.Entries())
                {
                    System.Console.WriteLine($"Entity: {entry.Entity.GetType().Name} State: {entry.State}");
                }

            }
        }


        // private static GlobalNoTrackingTest()
        // {
        //     context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        //     var customers = context.Customers.ToList();
        // }

        private static void AuditTest()
        {
            System.Console.WriteLine("AuditTest");
            System.Console.WriteLine("----------------");

            string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(connectionString);
     
            using(var context = new MyContext(optionsBuilder.Options))
            {
                foreach(var entry in context.ChangeTracker.Entries())
                {
                    System.Console.WriteLine($"{entry.Entity.GetType().Name} State: {entry.State}");
                }
            }
        }

        private static void AddEntityTest()
        {
           string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(connectionString);

            Customer customer = new Customer
            {
                FirstName = "John",
                LastName = "Smith"
            };

            using(var context = new MyContext(optionsBuilder.Options))
            {
                System.Console.WriteLine(context.Entry(customer).State);

                context.Customers.Add(customer);

                System.Console.WriteLine(context.Entry(customer).State);

                context.SaveChanges();

                System.Console.WriteLine(context.Entry(customer).State);

                customer.FirstName = "Marcin";

                var original = context.Entry(customer).OriginalValues["FirstName"];
                var current = context.Entry(customer).CurrentValues["FirstName"];

                System.Console.WriteLine($"FirstName was changed {original} -> {current}");

               // context.Entry(customer).State = EntityState.Deleted;

                 System.Console.WriteLine(context.Entry(customer).State);

                 context.SaveChanges();
                 
                 System.Console.WriteLine(context.Entry(customer).State);

            }
        }

        private static async Task CreateDbTestAsync()
        {
            string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();

            // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            optionsBuilder.UseSqlServer(connectionString);

            using(var context = new MyContext(optionsBuilder.Options))
            {
                System.Console.WriteLine("Droping...");
                await context.Database.EnsureDeletedAsync();

                System.Console.WriteLine("Creating...");
                bool created = await context.Database.EnsureCreatedAsync();
                System.Console.WriteLine("Created.");
            }
        }
    }
}
