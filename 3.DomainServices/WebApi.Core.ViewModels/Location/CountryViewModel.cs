using WebApi.Core.ViewModels.Core;
using System;

namespace WebApi.Core.ViewModels
{
    [Serializable]
    public class CountryViewModel : IdentityColumnViewModel
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
    }
}
