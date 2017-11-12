using System.Threading;
using System.Threading.Tasks;
using WebApi.Core.EntityModels.Core;

namespace WebApi.Core.IRepositories.Core
{
    public interface IIdentityBaseRepository<EntityModel> : IBaseRepository<EntityModel> where EntityModel : IdentityColumnEntity
    {
        EntityModel FindById(object id);
        Task<EntityModel> FindByIdAsync(object id);
        Task<EntityModel> FindByIdAsync(CancellationToken cancellationToken, object id);

        void Delete(long id);
        EntityModel GetById(long id);
    }
}
