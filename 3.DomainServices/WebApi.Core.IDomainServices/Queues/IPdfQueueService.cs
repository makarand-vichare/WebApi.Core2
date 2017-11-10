using WebApi.Core.IDomainServices.Core;
using System.Collections.Generic;
using WebApi.Core.ViewModels;
using WebApi.Core.EntityModels.Queues;

namespace WebApi.Core.IDomainServices.Queues
{
    public interface IPdfQueueService : IBaseService<PdfQueue, PdfQueueViewModel>
    {
        List<PdfQueueViewModel> GetPendingPdfQueue();
        bool ProcessPendingPdfs();
        //List<PdfResultViewModel> GetRequestsForEmailQueue();
    }
}
