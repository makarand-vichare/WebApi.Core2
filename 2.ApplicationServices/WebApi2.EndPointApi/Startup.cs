using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using WebApi2.EndPointApi.DependencyResolution;
using WebApi2.IDomainServices.AutoMapper;

[assembly: OwinStartup(typeof(WebApi2.EndPointApi.Startup))]

namespace WebApi2.EndPointApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            AreaRegistration.RegisterAllAreas();

            ConfigureAuth(app);

            config.DependencyResolver = new StructureMapWebApiDependencyResolver(StructuremapMvc.StructureMapDependencyScope.Container);
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            AutoMapperInit.BuildMap();

        }
    }
}
