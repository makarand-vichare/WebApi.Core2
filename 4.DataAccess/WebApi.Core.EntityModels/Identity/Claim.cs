using WebApi.Core.EntityModels.Core;
using System;

namespace WebApi.Core.EntityModels.Identity
{
    public class Claim : IdentityColumnEntity
    {
        private User _user;

        #region Scalar Properties
        public virtual long UserId { get; set; }
        public virtual string ClaimType { get; set; }
        public virtual string ClaimValue { get; set; }
        #endregion

        #region Navigation Properties
        public virtual User User
        {
            get { return _user; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _user = value;
                UserId = value.Id;
            }
        }
        #endregion
    }
}
