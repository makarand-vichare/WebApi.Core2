using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using WebApi.Core.Repositories.Core;

namespace WebApi.Core.Repositories.Extensions
{

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            DbContextOptions<DataContext> options = GetDbContextOptions();
            return new DataContext(options);
        }

        public static DbContextOptions<DataContext> GetDbContextOptions()
        {

            var enviornmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
           .AddJsonFile($"appsettings.{enviornmentName}.json", optional: false)
           .Build();

            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(DataContext).GetTypeInfo().Assembly.GetName().Name));
            return builder.Options;
        }
    }
}
