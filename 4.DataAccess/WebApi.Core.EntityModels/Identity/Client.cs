using WebApi.Core.EntityModels.Core;
using System.Collections.Generic;

namespace WebApi.Core.EntityModels.Identity
{
    public class Client : IdentityColumnEntity
    {
        private ICollection<RefreshToken> _refreshTokens;

        //[Key]
        public string ClientId { get; set; }

        //[Required]
        public string Secret { get; set; }

        //[Required]
        //[MaxLength(100)]
        public string Name { get; set; }

        public string ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }

        //[MaxLength(100)]
        public string AllowedOrigin { get; set; }

        #region Navigation Properties
        public virtual ICollection<RefreshToken> RefreshTokens
        {
            get { return _refreshTokens ?? (_refreshTokens = new List<RefreshToken>()); }
            set { _refreshTokens = value; }
        }
        #endregion
    }
}
