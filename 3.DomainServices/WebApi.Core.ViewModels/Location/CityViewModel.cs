using WebApi.Core.ViewModels.Core;
using System;

namespace WebApi.Core.ViewModels
{
    [Serializable]
    public class CityViewModel : IdentityColumnViewModel
    {
        public long CountryId { get; set; }
        public string CityName { get; set; }
        public bool IsActive { get; set; }
    }
}
