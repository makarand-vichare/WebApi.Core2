using WebApi.Core.EntityModels.Location;
using WebApi.Core.IDomainServices.Core;
using WebApi.Core.ServiceResponse;
using WebApi.Core.ViewModels;

namespace WebApi.Core.IDomainServices.Services
{
    public interface ICountryService : IBaseService<Country, CountryViewModel>
    {
        ResponseResults<LookUpViewModel> GetLookup();
    }
}
