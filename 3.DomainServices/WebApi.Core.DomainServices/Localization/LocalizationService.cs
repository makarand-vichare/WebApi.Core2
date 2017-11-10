using WebApi.Core.DomainServices.Core;
using WebApi.Core.IDomainServices.Services;
using WebApi.Core.ViewModels;
using WebApi.Core.EntityModels.Localization;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.Core.Utility;
using WebApi.Core.InfraStructure.Logging;

namespace WebApi.Core.DomainServices
{
    public class LocalizationService : BaseService<LocalizationKey, LocalizationKeyViewModel>, ILocalizationService
    {
        public Dictionary<string,string> GetLocalizations(string keyGroup, string languageCode)
        {
            var localizationKeys = new Dictionary<string, string>();
            try
            {
                var resourceKeyModel = UnitOfWork.KeyGroupRepository.GetResourceKeysByGroup(keyGroup);
                if(resourceKeyModel != null )
                {
                    var resourceKeys = resourceKeyModel.LocalizationKeys.Split(',').ToList();
                    var resourceValues = UnitOfWork.LocalizationKeyRepository.GetResourceByKeys(resourceKeys);
                    if (resourceValues != null && resourceValues.Count > 0)
                    {
                        if (languageCode == AppConstants.IrishLanguage)
                        {
                            localizationKeys = resourceValues.ToDictionary(o => o.LocalizationKeyCode, o => o.IrishValue);
                        }
                        else
                        {
                            localizationKeys = resourceValues.ToDictionary(o => o.LocalizationKeyCode, o => o.EnglishValue);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NLogLogger.Instance.Log(ex.Message);
            }
            return localizationKeys;
        }

    }
}
