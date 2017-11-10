using WebApi.Core.IDomainServices.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Core.ViewModels.Identity.WebApi;
using WebApi.Core.EntityModels.Identity;

namespace WebApi.Core.IDomainServices.IdentityStores
{
    public interface IRefreshTokenService : IBaseService<RefreshToken, RefreshTokenViewModel>
    {
        Task<bool> AddRefreshToken(RefreshTokenViewModel token);
        Task<bool> RemoveRefreshToken(string refreshTokenId);
        Task<bool> RemoveRefreshToken(RefreshTokenViewModel refreshToken);
        Task<RefreshTokenViewModel> FindRefreshToken(string refreshTokenId);
        List<RefreshTokenViewModel> GetAllRefreshTokens();
    }
}
