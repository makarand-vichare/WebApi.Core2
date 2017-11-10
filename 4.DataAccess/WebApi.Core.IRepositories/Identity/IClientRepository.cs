using WebApi.Core.EntityModels.Identity;
using WebApi.Core.IRepositories.Core;

namespace WebApi.Core.IRepositories.Identity
{
    public interface IClientRepository : IIdentityBaseRepository<Client>
    {
        Client FindByClientId(string clientId);

    }
}
