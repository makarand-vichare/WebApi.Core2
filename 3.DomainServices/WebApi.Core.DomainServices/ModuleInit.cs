using WebApi.Core.DomainServices.IdentityStores;
using WebApi.Core.IDomainServices.IdentityStores;
using WebApi.Core.IDomainServices.Queues;
using WebApi.Core.ViewModels.Identity.WebApi;
using WebApi.Core.IDomainServices.Services;
using WebApi.Core.Common.MEF;
using System.Composition;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Core.DomainServices
{
    [Export(typeof(IModule))]
    public class ModuleInit : IModule
    {
        public void Initialize(IModuleRegistrar registrar)
        {
            registrar.RegisterType(typeof(IUserStore<IdentityUserViewModel>), typeof(CustomUserStore));
            registrar.RegisterType(typeof(IRoleStore<IdentityRoleViewModel>), typeof(CustomRoleStore));

            registrar.RegisterType<IEmailQueueService, EmailQueueService>();
            registrar.RegisterType<IPdfQueueService, PdfQueueService>();
            registrar.RegisterType<IRequestQueueService, RequestQueueService>();

            registrar.RegisterType<IClientService, ClientService>();
            registrar.RegisterType<IRefreshTokenService, RefreshTokenService>();

            registrar.RegisterType<ICityService, CityService>();
            registrar.RegisterType<ICountryService, CountryService>();

            registrar.RegisterType<ILocalizationService, LocalizationService>();
        }
    }
}
