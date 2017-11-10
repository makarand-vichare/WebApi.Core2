using WebApi.Core.EntityModels.Core;
using System;
using System.Collections.Generic;

namespace WebApi.Core.EntityModels.Identity
{
    public class Role : IdentityColumnEntity
    {

        #region Scalar Properties
        public string Name { get; set; }
        #endregion

        #region Navigation Properties
        public ICollection<UserRole> UserRoles { get; set; }
        #endregion
    }
}
