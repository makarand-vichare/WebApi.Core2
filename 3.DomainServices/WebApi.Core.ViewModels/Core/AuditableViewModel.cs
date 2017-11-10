using WebApi.Core.ViewModels.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Core.ViewModels
{
    [Serializable]
    public abstract class AuditableViewModel : IdentityColumnViewModel
    {

        [Display(Name = "Updated On")]
        public DateTime UpdatedOn { get; set; }

        [Display(Name = "Updated By")]
        public long UpdatedBy { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

    }
}
