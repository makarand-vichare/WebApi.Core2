using WebApi.Core.Repositories.Core;
using WebApi.Core.Repositories.Identity;
using WebApi.Core.Repositories.Queues;
using WebApi.Core.Repositories.Location;
using WebApi.Core.Repositories.Localization;
using WebApi.Core.Common.MEF;
using System.Composition;
using WebApi.Core.IRepositories.Core;
using WebApi.Core.IRepositories.Queues;
using WebApi.Core.IRepositories.Identity;
using WebApi.Core.IRepositories.Location;
using WebApi.Core.IRepositories.Localization;

namespace WebApi.Core.Repositories
{
    [Export(typeof(IModule))]
    public class ModuleInit : IModule
    {
        public void Initialize(IModuleRegistrar registrar)
        {
            registrar.RegisterType<IUnitOfWork, UnitOfWork>();
            registrar.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            registrar.RegisterType<IEmailQueueRepository, EmailQueueRepository>();
            registrar.RegisterType<IPdfQueueRepository, PdfQueueRepository>();
            registrar.RegisterType<IRequestQueueRepository, RequestQueueRepository>();
            registrar.RegisterType<IUserRepository, UserRepository>();
            registrar.RegisterType<IRoleRepository, RoleRepository>();
            registrar.RegisterType<IExternalLoginRepository, ExternalLoginRepository>();
            registrar.RegisterType<IRefreshTokenRepository, RefreshTokenRepository>();
            registrar.RegisterType<IClientRepository, ClientRepository>();

            registrar.RegisterType<ICityRepository, CityRepository>();
            registrar.RegisterType<ICountryRepository, CountryRepository>();

            registrar.RegisterType<IKeyGroupRepository, KeyGroupRepository>();
            registrar.RegisterType<ILocalizationKeyRepository, LocalizationKeyRepository>();

        }
    }
}
