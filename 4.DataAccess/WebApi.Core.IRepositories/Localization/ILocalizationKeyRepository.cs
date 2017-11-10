using WebApi.Core.IRepositories.Core;
using System.Collections.Generic;
using WebApi.Core.EntityModels.Localization;

namespace WebApi.Core.IRepositories.Localization
{
    public interface ILocalizationKeyRepository : IIdentityBaseRepository<LocalizationKey>
    {
        List<LocalizationKey> GetResourceByKeys(List<string> resourceKeys);
    }
}
