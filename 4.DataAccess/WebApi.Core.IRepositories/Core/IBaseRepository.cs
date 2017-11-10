using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Core.EntityModels.Core;

namespace WebApi.Core.IRepositories.Core
{
    public interface IBaseRepository<EntityModel> where EntityModel : BaseEntity
    {
        IDataContext DbContext { get; set; }

        List<EntityModel> GetAll();
        Task<List<EntityModel>> GetAllAsync();
        Task<List<EntityModel>> GetAllAsync(CancellationToken cancellationToken);

        List<EntityModel> PageAll(int skip, int take);
        Task<List<EntityModel>> PageAllAsync(int skip, int take);
        Task<List<EntityModel>> PageAllAsync(CancellationToken cancellationToken, int skip, int take);

        EntityModel Find(params object[] keys);

        void Add(EntityModel entityModel);
        void Update(EntityModel entityModel);
        void Delete(EntityModel entityModel);
        void Delete(Expression<Func<EntityModel, bool>> predicate);

        EntityModel Get(Expression<Func<EntityModel, bool>> predicate);
        IEnumerable<EntityModel> GetMany(Expression<Func<EntityModel, bool>> predicate);
        bool Contains(Expression<Func<EntityModel, bool>> predicate);
        long Count { get; }

    }
}
