using WebApi.Core.IRepositories.Core;
using System.Collections.Generic;
using WebApi.Core.EntityModels.Localization;

namespace WebApi.Core.IRepositories.Localization
{
    public interface IKeyGroupRepository : IIdentityBaseRepository<KeyGroup>
    {
        KeyGroup GetResourceKeysByGroup(string groupId);
        List<KeyGroup> GetResourceKeysByGroups(List<string> groupIds);
    }
}
