using WebApi.Core.EntityModels.Location;
using WebApi.Core.IRepositories.Core;

namespace WebApi.Core.IRepositories.Location
{
    public interface ICountryRepository : IIdentityBaseRepository<Country>
    {
        //IEnumerable<CountryEntityModel> GetCountries();
    }
}
