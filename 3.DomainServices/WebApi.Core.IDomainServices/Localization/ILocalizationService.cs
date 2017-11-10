using WebApi.Core.IDomainServices.Core;
using System.Collections.Generic;
using WebApi.Core.ViewModels;
using WebApi.Core.EntityModels.Localization;

namespace WebApi.Core.IDomainServices.Services
{
    public interface ILocalizationService : IBaseService<LocalizationKey, LocalizationKeyViewModel>
    {
        Dictionary<string, string> GetLocalizations(string keyGroup, string languageCode);
    }
}
