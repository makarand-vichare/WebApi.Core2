using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApi.Core.Utility;
using WebApi.Core.ViewModels.Core;

namespace DatingSite.ViewModels
{
    [Serializable]
    public class LoginViewModel : BaseViewModel
    {
        [Required]
        [DisplayName("Email")]
        [StringLength(50, ErrorMessage = AppMessages.Input_MaxCharsAllowed)]
        [RegularExpression(@"^.{1,}$", ErrorMessage = AppMessages.Input_MaxCharsAllowed)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

    }

}
