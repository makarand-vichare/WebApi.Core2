using WebApi.Core.IRepositories.Core;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Core.EntityModels.Identity;

namespace WebApi.Core.IRepositories.Identity
{
    public interface IUserRepository  : IIdentityBaseRepository<User>
    {
        User FindByUserName(string username);
        Task<User> FindByUserNameAsync(string username);
        Task<User> FindByUserNameAsync(CancellationToken cancellationToken, string username);

        User FindByEmail(string email);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByEmailAsync(CancellationToken cancellationToken, string email);
    }
}
