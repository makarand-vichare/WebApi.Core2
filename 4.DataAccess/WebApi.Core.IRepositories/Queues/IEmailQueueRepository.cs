﻿using WebApi.Core.IRepositories.Core;
using System.Collections.Generic;
using WebApi.Core.EntityModels.Queues;

namespace WebApi.Core.IRepositories.Queues
{
    public interface IEmailQueueRepository : IIdentityBaseRepository<EmailQueue>
    {
        IEnumerable<EmailQueue> GetPendingEmailQueue();
    }
}
