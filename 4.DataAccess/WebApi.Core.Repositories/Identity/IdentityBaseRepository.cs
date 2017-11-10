﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Core.EntityModels.Core;
using WebApi.Core.IRepositories.Core;

namespace WebApi.Core.Repositories.Core
{

    public class IdentityBaseRepository<EntityModel> : IIdentityBaseRepository<EntityModel> where EntityModel : IdentityColumnEntity
    {
        private DataContext dataContext;

        public IDataContext DbContext {
            get {
                return dataContext;
            }
            set {
                dataContext = (DataContext)value;
            }
        }

        protected IdentityBaseRepository()
        {
        }

        ////protected UserBaseRepository(DataContext dataContext)
        ////{
        ////    this.DbContext = dataContext;
        ////}

        protected DbSet<EntityModel> DbSet
        {
            get
            {
                return dataContext.Set<EntityModel>();
            }
        }

        public List<EntityModel> GetAll()
        {
            return DbSet.ToList();
        }

        public Task<List<EntityModel>> GetAllAsync()
        {
            return DbSet.ToListAsync();
        }

        public Task<List<EntityModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            return DbSet.ToListAsync(cancellationToken);
        }

        public List<EntityModel> PageAll(int skip, int take)
        {
            return DbSet.Skip(skip).Take(take).ToList();
        }

        public Task<List<EntityModel>> PageAllAsync(int skip, int take)
        {
            return DbSet.Skip(skip).Take(take).ToListAsync();
        }

        public Task<List<EntityModel>> PageAllAsync(CancellationToken cancellationToken, int skip, int take)
        {
            return DbSet.Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        public virtual EntityModel Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public EntityModel FindById(object id)
        {
            return DbSet.Find(id);
        }

        public Task<EntityModel> FindByIdAsync(object id)
        {
            return DbSet.FindAsync(id);
        }

        public Task<EntityModel> FindByIdAsync(CancellationToken cancellationToken, object id)
        {
            return DbSet.FindAsync(cancellationToken, id);
        }

        public void Add(EntityModel entity)
        {
            DbSet.Add(entity);
        }

        public void Update(EntityModel entity)
        {
            var entry = dataContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
                entry = dataContext.Entry(entity);
            }
            entry.State = EntityState.Modified;
        }

        public virtual EntityModel Get(Expression<Func<EntityModel, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual IEnumerable<EntityModel> GetMany(Expression<Func<EntityModel, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual bool Contains(Expression<Func<EntityModel, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }


        public virtual void Delete(EntityModel model)
        {
            DbSet.Remove(model);
        }

        public virtual void Delete(Expression<Func<EntityModel, bool>> predicate)
        {
            var objects = GetMany(predicate);

            foreach (var obj in objects)
                DbSet.Remove(obj);
        }

        public void Delete(long id)
        {
            var model = GetById(id);
            DbSet.Remove(model);
        }

        public EntityModel GetById(long id)
        {
            return  DbSet.Where(o => o.Id == id).FirstOrDefault();
        }

        public virtual long Count
        {
            get { return DbSet.Count(); }
        }
    }
}
