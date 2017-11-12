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
    //public class TemporaryDbContextFactory : IDbContextFactory<DataContext>
    //{
    //    public DataContext Create(DbContextFactoryOptions options)
    //    {
    //        var builder = new DbContextOptionsBuilder<DataContext>();
    //        builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=efmigrations2017;Trusted_Connection=True;MultipleActiveResultSets=true",
    //            optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(DataContext).GetTypeInfo().Assembly.GetName().Name));
    //        return new DataContext(builder.Options);
    //    }
    //}

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            //var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();

            //var enviornmentName = configuration["ASPNETCORE_ENVIRONMENT"];


            //var enviornmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
             var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                //.AddJsonFile($"appsettings.{enviornmentName}.json", optional: false)
            .Build();

            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(DataContext).GetTypeInfo().Assembly.GetName().Name));
            return new DataContext(builder.Options);
        }
    }
}
