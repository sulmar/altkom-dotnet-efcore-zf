using System;
using System.Threading.Tasks;
using Altkom.ZF.DbServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Altkom.ZF.Models;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

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

            TagTest();
           // NonDeclareDbQueryTest();

            DbQueryTest();

            AddOrderTest();

            AddCustomerWithAddressTest();

            GlobalFilterTest();

            DisableGlobalFilterTest();

            PatchTest(); 

            TrackGraphTest();

            GetMetaDataTest();

            AddEntityTest();

           // await CreateDbTestAsync();

            AuditTest();

            LocalNoTrackingTest();


             System.Console.WriteLine("Press any key to exit.");

             Console.ReadLine();
        }

        // dotnet add package Microsoft.Extensions.Logging.Console
        public static readonly LoggerFactory LoggerFactory = 
             new LoggerFactory(new[] {new ConsoleLoggerProvider((category, level) => level == LogLevel.Information, true)});


        // EF Core 2.2
        private static void TagTest()
        {
            System.Console.WriteLine("---------------");
            System.Console.WriteLine("Tag Test");

            string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
        
            optionsBuilder
                .UseLoggerFactory(LoggerFactory)
                .UseSqlServer(connectionString);

            using(var context = new MyContext(optionsBuilder.Options))
            {
                var groups = context.Customers
                    .GroupBy(c=>c.IsDeleted)
                    .Select(g=>new { IsDeleted = g.Key, Count = g.Count()})
                    .TagWith("This is my query!")
                    .ToList();

                foreach(var group in groups)
                {
                    System.Console.WriteLine($"IsDeleted {group.IsDeleted} Count: {group.Count}");
                }
            }

        }

        private static void QueryRawSQLTest()
        {
            string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
           
            optionsBuilder
                .UseLoggerFactory(LoggerFactory)
                .UseSqlServer(connectionString);

            using(var context = new MyContext(optionsBuilder.Options))
            {
                var orderHeaders = context.Customers.FromSql("select * from dbo.customers");
            }

        }

        private static void NonDeclareDbQueryTest()
        {
            string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            
         //   optionsBuilder.UseLoggerFactory(LoggerFactory);

            optionsBuilder.UseSqlServer(connectionString);

            using(var context = new MyContext(optionsBuilder.Options))
            {
                var orderHeaders = context.Query<OrderHeader>().ToList();
            }

        }

        private static void DbQueryTest()
        {
             string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder
                .UseLoggerFactory(LoggerFactory)
                .UseSqlServer(connectionString);

            using(var context = new MyContext(optionsBuilder.Options))
            {
                var orderHeaders = context.OrderHeaders
                    .Include(p=>p.Customer)
                    .ToList();

                foreach(var header in orderHeaders)
                {
                    System.Console.WriteLine($"{header.FirstName} {header.TotalAmount} {header.Customer.LastName}");
                } 
            }
        }

        private static void AddOrderTest()
        {
            Customer customer = new Customer { Id = 1 };

            Order order = new Order
            {
                Number = "ZAM 001",
                OrderDate = DateTime.UtcNow,
                TotalAmount = 1000,
                Status = OrderStatus.Draft,
                Customer = customer
            };

            string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using(var context = new MyContext(optionsBuilder.Options))
            {
                context.Orders.Attach(order);

                foreach(var entry in context.ChangeTracker.Entries())
                {
                    System.Console.WriteLine($"Entity: {entry.Entity.GetType().Name} State: {entry.State}");
                }

                context.SaveChanges();
            }


        }


        private static void AddCustomerWithAddressTest()
        {
            System.Console.WriteLine("-----------------");
            System.Console.WriteLine("Address Test");

            string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(connectionString);

            var customer = new Customer
            {
                FirstName = "Marcin",
                LastName = "Sulecki",
                ShippingAddress = new Address{
                    City = "Czestochowa",
                    Street = "Rolnicza 33",
                    Country = "Poland"  
                } 
            };
            
            using(var context = new MyContext(optionsBuilder.Options))
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }

        private static void GlobalFilterTest()
        {
            System.Console.WriteLine("-----------------");
            System.Console.WriteLine("GlobalFilterTest");

            string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using(var context = new MyContext(optionsBuilder.Options))
            {
                var customer = context.Customers.Find(1);

                if(customer == null)
                {
                    System.Console.WriteLine("Nieznaleziono");
                }
                else
                 System.Console.WriteLine(customer);
            }
        }

          private static void DisableGlobalFilterTest()
        {
            System.Console.WriteLine("-----------------");
            System.Console.WriteLine("DisableGlobalFilterTest");

            string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using(var context = new MyContext(optionsBuilder.Options))
            {
                var customer = context.Customers.IgnoreQueryFilters().SingleOrDefault(p=>p.Id == 1);

                if(customer == null)
                {
                    System.Console.WriteLine("Nieznaleziono");
                }
                else
                 System.Console.WriteLine(customer);
            }
        }



        private static void PatchTest()
        {
            Customer customer = new Customer { Id = 1 };

            customer.FirstName = "Jan";

            string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(connectionString);

            string propertyName = "FirstName";
     
            using(var context = new MyContext(optionsBuilder.Options))
            {
                context.Entry(customer).Property(propertyName).IsModified = true;

                 foreach(var entry in context.ChangeTracker.Entries())
                {
                    System.Console.WriteLine($"Entity: {entry.Entity.GetType().Name} State: {entry.State}");
                }

                context.SaveChanges();
            }

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


        private static void TrackGraphTest()
        {
            System.Console.WriteLine("--------------------");
            System.Console.WriteLine("TrackGraphTest");

             string connectionString = "Server=127.0.0.1,1433;Database=ZFDb;User Id=sa;Password=P@ssw0rd";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(connectionString);
     
            using(var context = new MyContext(optionsBuilder.Options))
            {
                var customer = context.Customers.First();

                context.ChangeTracker.TrackGraph(customer, e => e.Entry.State = EntityState.Added);

                // context.ChangeTracker.TrackGraph(customer, e => 
                //     {
                //         if (e.Entry.IsKeySet)
                //         {
                //             e.Entry.State = EntityState.Unchanged;
                //         }
                //         else
                //         {
                //             e.Entry.State = EntityState.Added;
                //         }
                //     }
                // );

                foreach(var entry in context.ChangeTracker.Entries())
                {
                    System.Console.WriteLine($"Entity: {entry.Entity.GetType().Name} State: {entry.State}");
                }
            }   
        }
       
        private static void LocalNoTrackingTest()
        {
            System.Console.WriteLine("--------------------");
            System.Console.WriteLine("LocalNoTrackingTest");

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
