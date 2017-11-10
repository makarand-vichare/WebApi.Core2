using WebApi.Core.Repositories.Core;
using System.Linq;
using WebApi.Core.EntityModels.Identity;
using WebApi.Core.IRepositories.Identity;

namespace WebApi.Core.Repositories.Identity
{
    public class ClientRepository : IdentityBaseRepository<Client>, IClientRepository
    {
        public ClientRepository()
        {

        }

        public Client FindByClientId(string clientId)
        {
            return DbSet.FirstOrDefault(x => x.ClientId == clientId);
        }
    }
}
