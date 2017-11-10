using WebApi.Core.EntityModels.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Core.EntityModels.Queues
{
    [Serializable]
    public class PdfQueue : IdentityColumnEntity
    {

        [Required]
        public long CriminalId { get; set; }

        [Required]
        public string GeneratedHtml { get; set; }

        [Required]
        public bool ReGenerationRequired { get; set; }

        [Required]
        public bool IsPdfGenerationSucceed { get; set; }

        public string ErrorMessage { get; set; }

    }
}
