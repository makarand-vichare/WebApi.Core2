using WebApi.Core.EntityModels.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Core.EntityModels.Identity
{
    public class RefreshToken : IdentityColumnEntity
    {
        private Client _client;

        [Required]
        public string TokenId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(50)]
        public string ClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        [Required]
        public string ProtectedTicket { get; set; }

        #region Navigation Properties
        public virtual Client Client
        {
            get { return _client; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _client = value;
                ClientId = value.ClientId;
            }
        }
        #endregion
    }
}
