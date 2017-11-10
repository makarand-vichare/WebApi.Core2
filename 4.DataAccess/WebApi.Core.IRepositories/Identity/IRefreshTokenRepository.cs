using WebApi.Core.IRepositories.Core;
using System.Threading.Tasks;
using WebApi.Core.EntityModels.Identity;

namespace WebApi.Core.IRepositories.Identity
{
    public interface IRefreshTokenRepository : IIdentityBaseRepository<RefreshToken>
    {
        Task<RefreshToken> FindByTokenIdAsync(string tokenId);
    }
}
