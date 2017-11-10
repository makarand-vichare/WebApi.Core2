using WebApi.Core.Utility;
using WebApi.Core.ViewModels.Core;

namespace WebApi.Core.ViewModels.Identity.WebApi
{
    public class ClientViewModel : IdentityColumnViewModel
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }
    }
}
