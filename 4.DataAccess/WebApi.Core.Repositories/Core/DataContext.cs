using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using WebApi.Core.EntityModels.Core;
using WebApi.Core.EntityModels.Identity;
using WebApi.Core.EntityModels.Localization;
using WebApi.Core.EntityModels.Location;
using WebApi.Core.EntityModels.Queues;
using WebApi.Core.IRepositories.Core;
using WebApi.Core.Repositories.Configuration;

namespace WebApi.Core.Repositories.Core
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
        public DataContext() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<ExternalLogin> Logins { get; set; }
        public DbSet<Claim> Claims { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<KeyGroup> KeyGroups { get; set; }
        public DbSet<LocalizationKey> LocalizationKeys { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<RequestQueue> RequestQueues { get; set; }
        public DbSet<PdfQueue> PdfQueues { get; set; }
        public DbSet<EmailQueue> EmailQueues { get; set; }

        public virtual DbSet<T> DbSet<T>() where T : BaseEntity
        {
            return Set<T>();
        }

        public new EntityEntry Entry<T>(T entity) where T : BaseEntity
        {
            return base.Entry(entity);
        }

        public virtual int Commit()
        {
            return base.SaveChanges();
        }

        public virtual Task<int> CommitAsync()
        {
            return base.SaveChangesAsync();
        }

        public virtual Task<int> CommitAsync(System.Threading.CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

            modelBuilder.ApplyConfiguration(new ExternalLoginConfiguration());
            modelBuilder.ApplyConfiguration(new ClaimConfiguration());

            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());

            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());

            modelBuilder.ApplyConfiguration(new KeyGroupConfiguration());
            modelBuilder.ApplyConfiguration(new LocalizationKeyConfiguration());

            modelBuilder.ApplyConfiguration(new EmailQueueConfiguration());
            modelBuilder.ApplyConfiguration(new RequestQueueConfiguration());
            modelBuilder.ApplyConfiguration(new PdfQueueConfiguration());
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //if (optionsBuilder.IsConfigured) return;

        //    ////Called by parameterless ctor Usually Migrations
        //    //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        //    //Console.WriteLine("mak" + environment);
        //    //var builder = new ConfigurationBuilder().SetBasePath(Path.GetDirectoryName(path: GetType().GetTypeInfo().Assembly.Location))
        //    //        .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: false)
        //    //        //.AddJsonFile($"appsettings.{environment}.json", optional: false)
        //    //        .Build()
        //    //        .GetConnectionString("DefaultConnection");

        //    //optionsBuilder.UseSqlServer(builder);
        //}
    }
}
