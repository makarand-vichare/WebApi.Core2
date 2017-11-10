using WebApi.Core.EntityModels.Identity;
using WebApi.Core.IDomainServices.Core;
using WebApi.Core.ViewModels.Identity.WebApi;

namespace WebApi.Core.IDomainServices.IdentityStores
{
    public interface IClientService : IBaseService<Client, ClientViewModel>
    {
        ClientViewModel FindClient(string clientId);
        
    }
}
