using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApi.Core.EntityModels.Core;
using WebApi.Core.EntityModels.Identity;
using WebApi.Core.EntityModels.Location;
using WebApi.Core.Repositories.Core;
using WebApi.Core.Utility;

namespace WebApi.Core.Repositories.Extensions
{
    public static class DbContextExtension
    {
        static string folderPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "SeedData";
        static string filePathFormat = folderPath + Path.DirectorySeparatorChar + "{0}.json";
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        //private static void PopulateEntities(DataContext context)
        //{
        //    foreach(var filePath in Directory.GetFiles(folderPath, "*.json"))
        //    {
        //        var fileName = Path.GetFileNameWithoutExtension(filePath);
        //        Type elementType = Type.GetType(fileName);
        //        PopulateEntity<Country>(context);

        //    }
        //}

        public static void EnsureSeeded(this DataContext context)
        {

            PopulateEntity<Country>(context);
            PopulateEntity<City>(context);
            PopulateEntity<Role>(context);
            PopulateEntity<User>(context);
            PopulateEntity<UserRole>(context);

            if (!context.Clients.Any())
            {
                PopulateClient(context, AppMethods.GetHash("abc@123"));
            }
        }

        private static void PopulateEntity<T>(DataContext context) where T : BaseEntity
        {
            if (context.DbSet<T>().Count() <= 0 && File.Exists(string.Format(filePathFormat, typeof(T).Name)))
            {
                var types = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(string.Format(filePathFormat, typeof(T).Name)));
                context.AddRange(types);
                context.SaveChanges();
            }
        }

        private static void PopulateClient(DataContext context, string secretKey)
        {
            var client = "client";
            if (File.Exists(string.Format(filePathFormat, client)))
            {
                var types = JsonConvert.DeserializeObject<List<Client>>(File.ReadAllText(string.Format(filePathFormat, client)));

                types.ForEach(o => o.Secret = secretKey);
                context.AddRange(types);
                context.SaveChanges();
            }
        }
    }
}
