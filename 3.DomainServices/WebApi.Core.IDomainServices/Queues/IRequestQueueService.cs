using WebApi.Core.IDomainServices.Core;
using System.Collections.Generic;
using WebApi.Core.ViewModels;
using WebApi.Core.EntityModels.Queues;

namespace WebApi.Core.IDomainServices.Queues
{
    public interface IRequestQueueService : IBaseService<RequestQueue, RequestQueueViewModel>
    {
        List<RequestQueueViewModel> GetPendingRequestQueue();
        bool ProcessPendingRequests();
    }
}
