using WebApi.Core.EntityModels.Identity;
using WebApi.Core.IDomainServices.AutoMapper;
using WebApi.Core.IRepositories.Core;
using WebApi.Core.ViewModels.Identity.WebApi;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace WebApi.Core.DomainServices.IdentityStores
{
    public class CustomRoleStore :  IRoleStore<IdentityRoleViewModel, long>, IQueryableRoleStore<IdentityRoleViewModel, long>, IDisposable
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomRoleStore(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task CreateAsync(IdentityRoleViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("role");

            var model = GetRoleModel(viewModel);

            unitOfWork.RoleRepository.Add(model);
            return unitOfWork.CommitAsync();
        }

        public Task UpdateAsync(IdentityRoleViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException("role");

            var model = unitOfWork.RoleRepository.FindById(viewModel.Id);
            if (model == null)
                throw new ArgumentException("IdentityRoleViewModel does not correspond to a Role entity.", "role");

            model = viewModel.ToEntityModel<Role, IdentityRoleViewModel>();

            unitOfWork.RoleRepository.Update(model);
            return unitOfWork.CommitAsync();
        }

        public Task DeleteAsync(IdentityRoleViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("GetRole");

            var model = GetRoleModel(viewModel);

            unitOfWork.RoleRepository.Delete(model);
            return unitOfWork.CommitAsync();
        }

        public Task<IdentityRoleViewModel> FindByIdAsync(long roleId)
        {
            var model = unitOfWork.RoleRepository.FindById(roleId);

            var viewModel = model.ToViewModel<Role, IdentityRoleViewModel>();

            return Task.FromResult<IdentityRoleViewModel>(viewModel);
        }

        public Task<IdentityRoleViewModel> FindByNameAsync(string roleName)
        {
            var model = unitOfWork.RoleRepository.FindByName(roleName);
            var viewModel = model.ToViewModel<Role, IdentityRoleViewModel>();

            return Task.FromResult<IdentityRoleViewModel>(viewModel);
        }

        public IQueryable<IdentityRoleViewModel> Roles
        {
            get
            {
                return unitOfWork.RoleRepository
                    .GetAll()
                    .Select(x => GetIdentityRoleViewModel(x))
                    .AsQueryable();
            }
        }

        #region private methods

        private Role GetRoleModel(IdentityRoleViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            var model = viewModel.ToEntityModel<Role, IdentityRoleViewModel>();
            return model;
        }

        private IdentityRoleViewModel GetIdentityRoleViewModel(Role model)
        {
            if (model == null)
                return null;

            return model.ToViewModel<Role, IdentityRoleViewModel>();
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RoleStore() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
