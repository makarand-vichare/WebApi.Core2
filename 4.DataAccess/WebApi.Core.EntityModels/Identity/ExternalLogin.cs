using WebApi.Core.EntityModels.Core;

namespace WebApi.Core.EntityModels.Identity
{
    public class ExternalLogin : IdentityColumnEntity
    {
        private User _user;

        #region Scalar Properties
        public virtual string LoginProvider { get; set; }
        public virtual string ProviderKey { get; set; }
        public virtual long UserId { get; set; }
        #endregion

        #region Navigation Properties
        public virtual User User
        {
            get { return _user; }
            set
            {
                _user = value;
                UserId = value.Id;
            }
        }
        #endregion
    }
}
