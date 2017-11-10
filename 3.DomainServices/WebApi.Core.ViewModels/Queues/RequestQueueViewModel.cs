﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Core.ViewModels
{
    [Serializable]
    public class RequestQueueViewModel : AuditableViewModel
    {
        [Required]
        public string SearchParameters { get; set; }

        [Required]
        public bool IsRequestSucceed { get; set; }

        [Required]
        public string ErrorMessage { get; set; }

    }
}
