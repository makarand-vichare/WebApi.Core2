using WebApi.Core.Repositories.Core;
using System.Threading.Tasks;
using WebApi.Core.EntityModels.Identity;
using WebApi.Core.IRepositories.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Core.Repositories.Identity
{
    public class RefreshTokenRepository : IdentityBaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository()
        {

        }
        public Task<RefreshToken> FindByTokenIdAsync(string tokenId)
        {
            return DbSet.FirstOrDefaultAsync(x => x.TokenId == tokenId);
        }
    }
}
