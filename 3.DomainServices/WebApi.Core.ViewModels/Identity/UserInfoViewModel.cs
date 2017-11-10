using WebApi.Core.ViewModels.Core;

namespace WebApi.Core.ViewModels.Identity.WebApi
{
    public class UserInfoViewModel : BaseViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }

    }

}
