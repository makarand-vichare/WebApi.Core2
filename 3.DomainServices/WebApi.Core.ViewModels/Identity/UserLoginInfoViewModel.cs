using WebApi.Core.ViewModels.Core;

namespace WebApi.Core.ViewModels.Identity.WebApi
{
    public class UserLoginInfoViewModel : BaseViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }


    }

}
