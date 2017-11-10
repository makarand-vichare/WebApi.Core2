using WebApi.Core.EntityModels.Location;
using WebApi.Core.IRepositories.Location;
using WebApi.Core.Repositories.Core;

namespace WebApi.Core.Repositories.Location
{
    public class CountryRepository : IdentityBaseRepository<Country>, ICountryRepository
    {
        //public IEnumerable<CountryEntityModel> GetCountries()
        //{
        //    return DbSet.ToList();
        //}
    }
}
