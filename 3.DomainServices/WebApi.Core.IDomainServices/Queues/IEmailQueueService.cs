using WebApi.Core.IDomainServices.Core;
using System.Collections.Generic;
using WebApi.Core.EntityModels.Queues;
using WebApi.Core.ViewModels.Identity.WebApi;
using WebApi.Core.ViewModels;
using WebApi.Core.ServiceResponse;

namespace WebApi.Core.IDomainServices.Queues
{
    public interface IEmailQueueService : IBaseService<EmailQueue, EmailQueueViewModel>
    {
        BaseResponseResult SendUserRegistrationMail(IdentityUserViewModel viewModel);
        List<EmailQueueViewModel> GetEmailsFromQueue();
    }
}
