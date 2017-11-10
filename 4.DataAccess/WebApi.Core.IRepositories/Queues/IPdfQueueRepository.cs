using WebApi.Core.IRepositories.Core;
using System.Collections.Generic;
using WebApi.Core.EntityModels.Queues;

namespace WebApi.Core.IRepositories.Queues
{
    public interface IPdfQueueRepository : IIdentityBaseRepository<PdfQueue>
    {
        IEnumerable<PdfQueue> GetPendingPdfQueue();
    }
}
