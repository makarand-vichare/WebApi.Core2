using WebApi.Core.Repositories.Core;
using System.Collections.Generic;
using System;
using WebApi.Core.IRepositories.Queues;
using WebApi.Core.EntityModels.Queues;

namespace WebApi.Core.Repositories.Queues
{
    public class EmailQueueRepository : IdentityBaseRepository<EmailQueue>, IEmailQueueRepository
    {

        //public EmailQueueRepository(DataContext dataContext)
        //    : base(dataContext) {
        //}

        public IEnumerable<EmailQueue> GetPendingEmailQueue()
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<EmailQueueEntityModel> GetPendingEmailQueue()
        //{
        //    var entityList = this.DataContext.EmailQueues.Where(o => o.IsSucceedEmailSent == false);
        //    return entityList;

        //}
    }
 
}
