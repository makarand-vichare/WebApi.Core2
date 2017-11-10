using WebApi.Core.DomainServices.Core;
using WebApi.Core.EntityModels.Identity;
using WebApi.Core.IDomainServices.AutoMapper;
using WebApi.Core.ViewModels.Identity.WebApi;
using System.Threading.Tasks;
using WebApi.Core.IDomainServices.IdentityStores;

namespace WebApi.Core.DomainServices.IdentityStores
{
    public class ClientService : BaseService<Client, ClientViewModel> , IClientService
    {
        public async Task<RefreshTokenViewModel> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await UnitOfWork.RefreshTokenRepository.FindByTokenIdAsync(refreshTokenId);
            var tokenViewModel = refreshToken.ToViewModel<RefreshToken, RefreshTokenViewModel>();
            return tokenViewModel;
        }

        public ClientViewModel FindClient(string clientId)
        {
            var clientEntity = UnitOfWork.ClientRepository.FindByClientId(clientId);
            var clientViewModel = clientEntity.ToViewModel<Client, ClientViewModel>();

            return clientViewModel;
        }

    }
}
