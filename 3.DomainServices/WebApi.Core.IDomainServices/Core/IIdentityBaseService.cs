using WebApi.Core.EntityModels.Core;
using WebApi.Core.ServiceResponse;
using WebApi.Core.ViewModels.Core;

namespace WebApi.Core.IDomainServices.Core
{
    public interface IIdentityBaseService<T,VM> : IBaseService<T,VM>  where T : IdentityColumnEntity where VM : IdentityColumnViewModel
    {
    }
}
