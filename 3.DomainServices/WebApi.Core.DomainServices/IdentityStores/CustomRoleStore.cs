using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Core.EntityModels.Identity;
using WebApi.Core.IDomainServices.AutoMapper;
using WebApi.Core.IRepositories.Core;
using WebApi.Core.ViewModels.Identity.WebApi;

namespace WebApi.Core.DomainServices.IdentityStores
{
    public class CustomRoleStore :  IRoleStore<IdentityRoleViewModel>, IQueryableRoleStore<IdentityRoleViewModel>, IDisposable
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomRoleStore(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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

        public Task<IdentityResult> CreateAsync(IdentityRoleViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
                throw new ArgumentNullException("role");

            var model = GetRoleModel(viewModel);

            unitOfWork.RoleRepository.Add(model);
          
            var result = unitOfWork.CommitAsync().Result;
            return result > 0 ? Task.FromResult(IdentityResult.Success) : Task.FromResult(new IdentityResult { });

        }

        public Task<IdentityResult> UpdateAsync(IdentityRoleViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
                throw new ArgumentException("role");

            var model = unitOfWork.RoleRepository.FindById(viewModel.Id);
            if (model == null)
                throw new ArgumentException("IdentityRoleViewModel does not correspond to a Role entity.", "role");

            model = viewModel.ToEntityModel<Role, IdentityRoleViewModel>();

            unitOfWork.RoleRepository.Update(model);
            var result = unitOfWork.CommitAsync().Result;
            return result > 0 ? Task.FromResult(IdentityResult.Success) : Task.FromResult(new IdentityResult { });
        }

        public Task<IdentityResult> DeleteAsync(IdentityRoleViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
                throw new ArgumentNullException("GetRole");

            var model = GetRoleModel(viewModel);

            unitOfWork.RoleRepository.Delete(model);
            var result = unitOfWork.CommitAsync().Result;
            return result > 0 ? Task.FromResult(IdentityResult.Success) : Task.FromResult(new IdentityResult { });
        }

        public Task<string> GetRoleIdAsync(IdentityRoleViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = unitOfWork.RoleRepository.FindById(viewModel.Id);

            //var viewModel = model.ToViewModel<Role, IdentityRoleViewModel>();

            return Task.FromResult<string>(model.Name);
        }

        public Task<string> GetRoleNameAsync(IdentityRoleViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = unitOfWork.RoleRepository.FindByName(viewModel.Name);
          //  var viewModel = model.ToViewModel<Role, IdentityRoleViewModel>();
            return Task.FromResult<string>(model.Name);
        }

        public Task SetRoleNameAsync(IdentityRoleViewModel viewModel, string roleName, CancellationToken cancellationToken)
        {
            if (viewModel == null)
                throw new ArgumentException("role");

            var model = unitOfWork.RoleRepository.FindById(viewModel.Id);
            viewModel.Name = roleName;
            if (model == null)
                throw new ArgumentException("IdentityRoleViewModel does not correspond to a Role entity.", "role");

            model = viewModel.ToEntityModel<Role, IdentityRoleViewModel>();

            unitOfWork.RoleRepository.Update(model);
            return unitOfWork.CommitAsync();
        }

        public Task<string> GetNormalizedRoleNameAsync(IdentityRoleViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = unitOfWork.RoleRepository.FindById(viewModel.Id);
            //  var viewModel = model.ToViewModel<Role, IdentityRoleViewModel>();
            return Task.FromResult<string>(model.Name);
        }

        public Task SetNormalizedRoleNameAsync(IdentityRoleViewModel viewModel, string normalizedName, CancellationToken cancellationToken)
        {
            if (viewModel == null)
                throw new ArgumentException("role");

            var model = unitOfWork.RoleRepository.FindById(viewModel.Id);
            viewModel.Name = normalizedName;
            if (model == null)
                throw new ArgumentException("IdentityRoleViewModel does not correspond to a Role entity.", "role");

            model = viewModel.ToEntityModel<Role, IdentityRoleViewModel>();

            unitOfWork.RoleRepository.Update(model);
            return unitOfWork.CommitAsync();
        }

        public Task<IdentityRoleViewModel> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            var model = unitOfWork.RoleRepository.FindById(roleId);

            var viewModel = model.ToViewModel<Role, IdentityRoleViewModel>();

            return Task.FromResult<IdentityRoleViewModel>(viewModel);
        }

        public Task<IdentityRoleViewModel> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            var model = unitOfWork.RoleRepository.FindByName(normalizedRoleName);
            var viewModel = model.ToViewModel<Role, IdentityRoleViewModel>();

            return Task.FromResult<IdentityRoleViewModel>(viewModel);
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
