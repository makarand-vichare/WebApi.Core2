using WebApi.Core.IRepositories.Core;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Core.EntityModels.Identity;

namespace WebApi.Core.IRepositories.Identity
{
    public interface IExternalLoginRepository : IIdentityBaseRepository<ExternalLogin>
    {
        ExternalLogin GetByProviderAndKey(string loginProvider, string providerKey);
        Task<ExternalLogin> GetByProviderAndKeyAsync(string loginProvider, string providerKey);
        Task<ExternalLogin> GetByProviderAndKeyAsync(CancellationToken cancellationToken, string loginProvider, string providerKey);
    }
}
