using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Core.EntityModels.Core;

namespace WebApi.Core.IRepositories.Core
{
    public interface IDataContext : IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
        Task<int> CommitAsync(CancellationToken cancellationToken);
        DbSet<T> DbSet<T>() where T : BaseEntity;
        EntityEntry Entry<T>(T entity) where T : BaseEntity;

    }
}
