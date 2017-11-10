using WebApi.Core.EntityModels.Core;
using System;
using System.Collections.Generic;

namespace WebApi.Core.EntityModels.Identity
{
    public class UserRole : BaseEntity
    {
        #region Fields
        #endregion

        #region Scalar Properties
        public long RoleId { get; set; }
        public long UserId { get; set; }

        #endregion

        #region Navigation Properties
        public User User { get; set; }
        public Role Role { get; set; }

        #endregion
    }
}
