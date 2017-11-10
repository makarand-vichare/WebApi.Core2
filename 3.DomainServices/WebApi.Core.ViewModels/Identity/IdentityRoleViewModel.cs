using Microsoft.AspNet.Identity;
using WebApi.Core.ViewModels.Core;

namespace WebApi.Core.ViewModels.Identity.WebApi
{
    public class IdentityRoleViewModel : BaseViewModel, IRole<long>
    {

        public IdentityRoleViewModel(string name)
        {
            this.Name = name;
        }

        public IdentityRoleViewModel(string name, long id)
        {
            this.Name = name;
            this.Id = id;
        }

        public long Id { get; set; }
        public string Name { get; set; }
    }
}
