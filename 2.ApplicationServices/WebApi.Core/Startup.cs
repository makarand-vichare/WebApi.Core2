using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using System;
using WebApi.Core.DomainServices;
using WebApi.Core.IDomainServices.Services;
using WebApi.Core.IOC;
using WebApi.Core.IRepositories.Location;
using WebApi.Core.Repositories.Location;

namespace WebApi.Core
{
    public class Startup
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            hostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication().AddCookie("ApplicationCookie");
            //services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
            //    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //});

            //services.AddAuthentication().AddGoogle(googleOptions =>
            //{
            //    googleOptions.ClientId = Configuration["Authentication:Facebook:ClientId"];
            //    googleOptions.ClientSecret = Configuration["Authentication:Facebook:ClientSecret"];
            //});

            services.AddMvc();

            // Configure the IoC container
            return ConfigureIoC(services);

            //services.AddDbContext<DataContext>(options =>
            //{
            //    Environment.SetEnvironmentVariable("EnvironmentName", hostingEnvironment.EnvironmentName.ToLower() == "development" ? "local" : hostingEnvironment.EnvironmentName);
            //    options.UseSqlServer(Configuration.GetConnectionString("DalSoftDbContext"));
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc();
        }

        public IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            // var container = StructureMapConfig.RegisterComponents();

            var container = new Container();

            container.Configure(config =>
            {
                //Populate the container using the service collection
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(Startup));
                    _.AssembliesFromPath(".\\bin\\Debug\\netcoreapp2.0", o => {                       
                        return o.FullName.Contains("WebApi.Core.") && o.FullName.EndsWith(".dll");
                    });
                    _.WithDefaultConventions();
                });
                //config.For<ICountryService>().Use<CountryService>();
                //config.For<ICountryRepository>().Use<CountryRepository>();
                config.Populate(services);
            });
            return container.GetInstance<IServiceProvider>();
        }
    }
}
