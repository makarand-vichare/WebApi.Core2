using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using WebApi.Core.InfraStructure.Logging;
using WebApi.Core.Repositories.Core;
using WebApi.Core.Repositories.Extensions;

namespace MigrationDBApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Migration app for entity framework core started");

            IServiceCollection services = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            factory.AddNLog();

            NLogLogger.Instance.Log("App Init completed");
            Console.WriteLine("Initialization completed .. checking migrations data ...");

            //if (!serviceProvider.GetService<DataContext>().AllMigrationsApplied())
            //{
                Console.WriteLine("Applying pending migrations changes");
                serviceProvider.GetService<DataContext>().Database.Migrate();
                Console.WriteLine("Applying seed data.");
                serviceProvider.GetService<DataContext>().EnsureSeeded();
            //}
            //else
            //{
            //    Console.WriteLine("Migrations is uptodate.");
            //}

            Console.WriteLine("Migrations is done. please enter a key for exist");
            Console.ReadLine();
            //factory.ConfigureNLog(".\\bin\\Debug\\netcoreapp2.0\\NLog.config");

            //var logger = serviceProvider.GetService<ILogger>();
            //logger.LogCritical("App Init completed");
        }
    }
}
