using WebApi.Core.EntityModels.Identity;
using WebApi.Core.IRepositories.Identity;
using WebApi.Core.Repositories.Core;

namespace WebApi.Core.Repositories.Identity
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        //public RoleRepository(DataContext dataContext)
        //    : base(dataContext)
        //{
        //}

        //public Role FindByName(string roleName)
        //{
        //    return DbSet.FirstOrDefault(x => x.Name == roleName);
        //}

        //public Task<Role> FindByNameAsync(string roleName)
        //{
        //    return DbSet.FirstOrDefaultAsync(x => x.Name == roleName);
        //}

        //public Task<Role> FindByNameAsync(System.Threading.CancellationToken cancellationToken, string roleName)
        //{
        //    return DbSet.FirstOrDefaultAsync(x => x.Name == roleName, cancellationToken);
        //}
    }
}
