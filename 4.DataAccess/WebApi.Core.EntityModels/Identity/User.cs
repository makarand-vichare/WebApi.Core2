using WebApi.Core.EntityModels.Core;
using System;
using System.Collections.Generic;

namespace WebApi.Core.EntityModels.Identity
{
    public class User : IdentityColumnEntity
    {
        #region Fields
        private ICollection<Claim> _claims;
        private ICollection<ExternalLogin> _externalLogins;
        #endregion

        #region Scalar Properties
        //public long UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long CountryId { get; set; }
        public long CityId { get; set; }
        public string AboutInfo { get; set; }

        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }
        #endregion

        #region Navigation Properties
        public virtual ICollection<Claim> Claims
        {
            get { return _claims ?? (_claims = new List<Claim>()); }
            set { _claims = value; }
        }

        public virtual ICollection<ExternalLogin> Logins
        {
            get
            {
                return _externalLogins ??
                    (_externalLogins = new List<ExternalLogin>());
            }
            set { _externalLogins = value; }
        }

        public ICollection<UserRole> UserRoles { get; set; }

        #endregion
    }
}
