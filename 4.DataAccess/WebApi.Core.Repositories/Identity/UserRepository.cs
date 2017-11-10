using WebApi.Core.Repositories.Core;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using WebApi.Core.IRepositories.Identity;
using WebApi.Core.EntityModels.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Core.Repositories.Identity
{
    public class UserRepository : IdentityBaseRepository<User>, IUserRepository
    {
        //public UserRepository(DataContext dataContext)
        //    : base(dataContext)
        //{
        //}

        public User FindByEmail(string email)
        {
            return DbSet.FirstOrDefault(x => x.Email == email);
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return DbSet.FirstOrDefaultAsync(x => x.Email == email);
        }

        public Task<User> FindByEmailAsync(CancellationToken cancellationToken, string email)
        {
            return DbSet.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public User FindByUserName(string username)
        {
            return DbSet.FirstOrDefault(x => x.UserName == username);
        }

        public Task<User> FindByUserNameAsync(string username)
        {
            return DbSet.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public Task<User> FindByUserNameAsync(CancellationToken cancellationToken, string username)
        {
            return DbSet.FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);
        }
    }
}
