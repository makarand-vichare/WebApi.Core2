using WebApi.Core.EntityModels.Core;

namespace WebApi.Core.EntityModels.Localization
{
    public class LocalizationKey : IdentityColumnEntity
    {

        #region Scalar Properties
        public string LocalizationKeyCode { get; set; }
        public string EnglishValue { get; set; }
        public string IrishValue { get; set; }
        public bool IsActive { get; set; }
        #endregion
    }
}
