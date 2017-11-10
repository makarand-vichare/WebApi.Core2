using WebApi.Core.DomainServices.Core;
using WebApi.Core.ViewModels;
using WebApi.Core.IDomainServices.AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System;
using WebApi.Core.EntityModels.Queues;
using WebApi.Core.IDomainServices.Queues;

namespace WebApi.Core.DomainServices
{
    public class RequestQueueService : IdentityBaseService<RequestQueue, RequestQueueViewModel>, IRequestQueueService
    {

        public List<RequestQueueViewModel> GetPendingRequestQueue()
        {
            var result = new List<RequestQueueViewModel>();

            var entityList = UnitOfWork.RequestQueueRepository.GetPendingRequestQueue().ToList();

            if (entityList != null && entityList.Count > 0)
            {
                result = entityList.ToViewModel<RequestQueue, RequestQueueViewModel>().ToList();
            }

            return result;
        }

        private void UpdateRequestQueue(RequestQueueViewModel requestViewModel,bool isSucceed)
        {
           var existingEntity = UnitOfWork.RequestQueueRepository.FindById(requestViewModel.Id);
                existingEntity.IsRequestSucceed = isSucceed;
            UnitOfWork.RequestQueueRepository.Update(existingEntity);
        }

        public bool ProcessPendingRequests()
        {
            throw new NotImplementedException();
        }
    }
}
