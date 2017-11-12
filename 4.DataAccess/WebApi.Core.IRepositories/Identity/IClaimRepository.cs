using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Core.EntityModels.Identity;
using WebApi.Core.IRepositories.Core;

namespace WebApi.Core.IRepositories.Identity
{
    public interface IClaimRepository : IIdentityBaseRepository<Claim>
    {
        Claim FindByType(string claimType);
        Task<Claim> FindByTypeAsync(CancellationToken cancellationToken, string claimType);
        Task<Claim> FindByTypeAsync(string claimType);
        List<long> GetUserIdsByClaim(System.Security.Claims.Claim claim);
    }
}