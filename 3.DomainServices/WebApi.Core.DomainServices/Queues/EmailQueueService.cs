using WebApi.Core.DomainServices.Core;
using WebApi.Core.ServiceResponse;
using WebApi.Core.ViewModels;
using System;
using WebApi.Core.IDomainServices.AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApi.Core.Utility;
using WebApi.Core.EntityModels.Queues;
using WebApi.Core.ViewModels.Identity.WebApi;
using WebApi.Core.IDomainServices.Queues;
using WebApi.Core.Mails;

namespace WebApi.Core.DomainServices
{
    public class EmailQueueService : IdentityBaseService<EmailQueue, EmailQueueViewModel>, IEmailQueueService
    {

        private bool AddEmailIntoQueue(EmailQueue entity)
        {
            bool result = false;
            try
            {

                if (entity != null)
                {
                    UnitOfWork.EmailQueueRepository.Add(entity);
                    UnitOfWork.Commit();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public List<EmailQueueViewModel> GetEmailsFromQueue()
        {
            var result = new List<EmailQueueViewModel>();

            var entityList = UnitOfWork.EmailQueueRepository.GetPendingEmailQueue().ToList();

            if (entityList != null && entityList.Count > 0)
            {
                result = entityList.ToViewModel<EmailQueue, EmailQueueViewModel>().ToList();
            }

            return result;
        }

        public BaseResponseResult SendUserRegistrationMail(IdentityUserViewModel viewModel)
        {
            BaseResponseResult result = new BaseResponseResult();

            try
            {
                if (string.IsNullOrEmpty(viewModel.Email) == false)
                {
                    var maiTemplate = new UserRegistrationMail(viewModel.Email, viewModel.InputPassword);
                    //var queueViewModel = maiTemplate.CreateEmailQueueViewModel(viewModel.Email);
                    //var entity = queueViewModel.ToEntityModel<EmailQueue, EmailQueueViewModel>();
                    //result.IsSucceed = AddEmailIntoQueue(entity); 

                    if (result.IsSucceed)
                    {
                        result.Message = AppMessages.Email_Succeed_Message;
                    }
                    else
                    {
                        result.Message = AppMessages.Email_Failed_Message;
                    }
                }
            }
            catch (ApplicationException)
            {
                result.IsSucceed = false;
                result.Message = AppMessages.Email_Failed_Message;
            }

            return result;
        }
    }

}
