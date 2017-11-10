using WebApi.Core.ViewModels.Core;
using System;

namespace WebApi.Core.ViewModels
{
    [Serializable]
    public class LocalizationKeyViewModel : IdentityColumnViewModel
    {
        public string LocalizationKey { get; set; }
        public string EnglishValue { get; set; }
        public string IrishValue { get; set; }
        public bool IsActive { get; set; }
    }
}
