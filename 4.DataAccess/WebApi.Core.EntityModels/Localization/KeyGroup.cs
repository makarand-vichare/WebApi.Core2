using WebApi.Core.EntityModels.Core;

namespace WebApi.Core.EntityModels.Localization
{
    public class KeyGroup : IdentityColumnEntity
    {

        #region Scalar Properties
        public string KeyGroupCode { get; set; }
        public string LocalizationKeys { get; set; }

        #endregion
    }
}
