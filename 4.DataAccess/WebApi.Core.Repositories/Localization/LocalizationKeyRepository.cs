using WebApi.Core.Repositories.Core;
using System.Collections.Generic;
using System.Linq;
using WebApi.Core.EntityModels.Localization;
using WebApi.Core.IRepositories.Localization;

namespace WebApi.Core.Repositories.Localization
{
    public class LocalizationKeyRepository : IdentityBaseRepository<LocalizationKey>, ILocalizationKeyRepository
    {

        public List<LocalizationKey> GetResourceByKeys(List<string> resourceKeys)
        {
            if (resourceKeys != null && resourceKeys.Count > 0)
            {
                return DbSet.Where(o => o.IsActive == true && resourceKeys.Contains(o.LocalizationKeyCode)).ToList();
            }
            else
            {
                return new List<LocalizationKey>();
            }
        }
    }
}
