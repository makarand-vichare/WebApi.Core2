using WebApi.Core.EntityModels.Core;
using WebApi.Core.ServiceResponse;
using WebApi.Core.ViewModels.Core;

namespace WebApi.Core.IDomainServices.Core
{
    public interface IBaseService<T,VM>  where T : BaseEntity where VM : BaseViewModel
    {
        ResponseResults<VM> GetAll();
        ResponseResult<VM> Save(VM viewModel);
    }
}
