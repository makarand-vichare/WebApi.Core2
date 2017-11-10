using WebApi.Core.ViewModels.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Core.ViewModels
{
    [Serializable]
    public class PdfQueueViewModel : IdentityColumnViewModel
    {
        [Required]
        public long CriminalId { get; set; }

        [Required]
        public string GeneratedHtml { get; set; }

        [Required]
        public bool ReGenerationRequired { get; set; }

        [Required]
        public bool IsPdfGenerationSucceed { get; set; }

        [Required]
        public string ErrorMessage { get; set; }

    }
}
