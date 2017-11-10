using WebApi.Core.ViewModels.Core;
using System;

namespace WebApi.Core.ViewModels
{
    [Serializable]
    public class LookUpViewModel : IdentityColumnViewModel
    {
        public string Value { get; set; }
    }
}
