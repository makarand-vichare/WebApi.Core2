using WebApi.Core.EntityModels.Identity;
using WebApi.Core.IDomainServices.AutoMapper;
using WebApi.Core.IRepositories.Core;
using WebApi.Core.ServiceResponse;
using WebApi.Core.ViewModels.Identity.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace WebApi.Core.DomainServices.IdentityStores
{
    public class CustomUserStore : IUserLoginStore<IdentityUserViewModel, long>, 
        IUserClaimStore<IdentityUserViewModel, long>, 
        IUserRoleStore<IdentityUserViewModel, long>, 
        IUserPasswordStore<IdentityUserViewModel, long>, 
        IUserSecurityStampStore<IdentityUserViewModel, long>, 
        IUserStore<IdentityUserViewModel, long>, IDisposable 
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomUserStore(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region IUserStore<IdentityUserViewModel, long> Members
        public Task CreateAsync(IdentityUserViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException("user");

            var viewModel = GetUserModel(model);

            unitOfWork.UserRepository.Add(viewModel);
            return unitOfWork.CommitAsync();
        }

        public Task DeleteAsync(IdentityUserViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException("user");

            var viewModel = GetUserModel(model);

            unitOfWork.UserRepository.Delete(viewModel);
            return unitOfWork.CommitAsync();
        }

        public Task<IdentityUserViewModel> FindByIdAsync(long userId)
        {
            var model = unitOfWork.UserRepository.FindById(userId);
            return Task.FromResult<IdentityUserViewModel>(GetIdentityUserViewModel(model));
        }

        public Task<IdentityUserViewModel> FindByNameAsync(string userName)
        {
            var model = unitOfWork.UserRepository.FindByUserName(userName);
            return Task.FromResult<IdentityUserViewModel>(GetIdentityUserViewModel(model));
        }

        public Task UpdateAsync(IdentityUserViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException("user");

            var model = unitOfWork.UserRepository.FindById(viewModel.Id);
            if (model == null)
                throw new ArgumentException("IdentityUserViewModel does not correspond to a User entity.", "user");

            unitOfWork.UserRepository.Update(model);
            return unitOfWork.CommitAsync();
        }
        #endregion

        #region IUserClaimStore<IdentityUserViewModel, long> Members
        public Task AddClaimAsync(IdentityUserViewModel viewModel, System.Security.Claims.Claim claim)
        {
            if (viewModel == null)
                throw new ArgumentNullException("user");
            if (claim == null)
                throw new ArgumentNullException("claim");

            var model = unitOfWork.UserRepository.FindById(viewModel.Id);
            if (model == null)
                throw new ArgumentException("IdentityUserViewModel does not correspond to a User entity.", "user");

            var claimEntityModel = new EntityModels.Identity.Claim
            {
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
                User = model
            };
            model.Claims.Add(claimEntityModel);

            unitOfWork.UserRepository.Update(model);
            return unitOfWork.CommitAsync();
        }

        public Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(IdentityUserViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("user");

            var model = unitOfWork.UserRepository.FindById(viewModel.Id);
            if (model == null)
                throw new ArgumentException("IdentityUserViewModel does not correspond to a User entity.", "user");

            return Task.FromResult<IList<System.Security.Claims.Claim>>(model.Claims.Select(x => new System.Security.Claims.Claim(x.ClaimType, x.ClaimValue)).ToList());
        }

        public Task RemoveClaimAsync(IdentityUserViewModel viewModel, System.Security.Claims.Claim claim)
        {
            if (viewModel == null)
                throw new ArgumentNullException("user");
            if (claim == null)
                throw new ArgumentNullException("claim");

            var model = unitOfWork.UserRepository.FindById(viewModel.Id);
            if (model == null)
                throw new ArgumentException("IdentityUserViewModel does not correspond to a User entity.", "user");

            var claimEntityModel = model.Claims.FirstOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            model.Claims.Remove(claimEntityModel);

            unitOfWork.UserRepository.Update(model);
            return unitOfWork.CommitAsync();
        }
        #endregion

        #region IUserLoginStore<IdentityUserViewModel, long> Members
        public Task AddLoginAsync(IdentityUserViewModel viewModel, UserLoginInfo login)
        {
            if (viewModel == null)
                throw new ArgumentNullException("user");
            if (login == null)
                throw new ArgumentNullException("login");

            var model = unitOfWork.UserRepository.FindById(viewModel.Id);
            if (model == null)
                throw new ArgumentException("IdentityUserViewModel does not correspond to a User entity.", "user");

            var externalLoginEntityModel = new ExternalLogin
            {
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey,
                User = model
            };
            model.Logins.Add(externalLoginEntityModel);

            unitOfWork.UserRepository.Update(model);
            return unitOfWork.CommitAsync();
        }

        public Task<IdentityUserViewModel> FindAsync(UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            var identityUser = default(IdentityUserViewModel);

            var externalLoginEntityModel = unitOfWork.ExternalLoginRepository.GetByProviderAndKey(login.LoginProvider, login.ProviderKey);
            if (externalLoginEntityModel != null)
                identityUser = GetIdentityUserViewModel(externalLoginEntityModel.User);

            return Task.FromResult<IdentityUserViewModel>(identityUser);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUserViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("user");

            var model = unitOfWork.UserRepository.FindById(viewModel.Id);
            if (model == null)
                throw new ArgumentException("IdentityUserViewModel does not correspond to a User entity.", "user");

            return Task.FromResult<IList<UserLoginInfo>>(model.Logins.Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey)).ToList());
        }

        public Task RemoveLoginAsync(IdentityUserViewModel viewModel, UserLoginInfo login)
        {
            if (viewModel == null)
                throw new ArgumentNullException("user");
            if (login == null)
                throw new ArgumentNullException("login");

            var model = unitOfWork.UserRepository.FindById(viewModel.Id);
            if (model == null)
                throw new ArgumentException("IdentityUserViewModel does not correspond to a User entity.", "user");

            var loginModel = model.Logins.FirstOrDefault(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey);
            model.Logins.Remove(loginModel);

            unitOfWork.UserRepository.Update(model);
            return unitOfWork.CommitAsync();
        }
        #endregion

        #region IUserRoleStore<IdentityUserViewModel, long> Members
        public Task AddToRoleAsync(IdentityUserViewModel viewModel, string roleName)
        {
            if (viewModel == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: roleName.");

            var model = unitOfWork.UserRepository.FindById(viewModel.Id);
            if (model == null)
                throw new ArgumentException("IdentityUserViewModel does not correspond to a User entity.", "user");
            var roleEntityModel = unitOfWork.RoleRepository.FindByName(roleName);
            if (roleEntityModel == null)
                throw new ArgumentException("roleName does not correspond to a Role entity.", "roleName");

            model.UserRoles.Add(new UserRole { UserId = viewModel.Id, RoleId= roleEntityModel.Id });
            unitOfWork.UserRepository.Update(model);

            return unitOfWork.CommitAsync();
        }

        public Task<IList<string>> GetRolesAsync(IdentityUserViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("user");

            var model = unitOfWork.UserRepository.FindById(viewModel.Id);
            if (model == null)
                throw new ArgumentException("IdentityUserViewModel does not correspond to a User entity.", "user");

            var ids = model.UserRoles.Select(o => o.RoleId);
            var list = unitOfWork.RoleRepository.GetMany(o=> ids.Contains(o.Id));
            return Task.FromResult<IList<string>>(list.Select(x => x.Name).ToList());
        }

        public Task<bool> IsInRoleAsync(IdentityUserViewModel viewModel, string roleName)
        {
            if (viewModel == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");

            var model = unitOfWork.UserRepository.FindById(viewModel.Id);
            if (model == null)
                throw new ArgumentException("IdentityUserViewModel does not correspond to a User entity.", "user");

            var ids = model.UserRoles.Select(o => o.RoleId);
            var list = unitOfWork.RoleRepository.GetMany(o => ids.Contains(o.Id));

            return Task.FromResult<bool>(list.Any(x => x.Name == roleName));
        }

        public Task RemoveFromRoleAsync(IdentityUserViewModel viewModel, string roleName)
        {
            if (viewModel == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");

            var model = unitOfWork.UserRepository.FindById(viewModel.Id);
            if (model == null)
                throw new ArgumentException("IdentityUserViewModel does not correspond to a User entity.", "user");
            var ids = model.UserRoles.Select(o => o.RoleId);
            var r = unitOfWork.RoleRepository.Get(x => x.Name == roleName);
            var userRole = model.UserRoles.FirstOrDefault(o => o.RoleId == r.Id);
            model.UserRoles.Remove(userRole);

            unitOfWork.UserRepository.Update(model);
            return unitOfWork.CommitAsync();
        }
        #endregion

        #region IUserPasswordStore<IdentityUserViewModel, long> Members
        public Task<string> GetPasswordHashAsync(IdentityUserViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<string>(viewModel.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IdentityUserViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<bool>(!string.IsNullOrWhiteSpace(viewModel.PasswordHash));
        }

        public Task SetPasswordHashAsync(IdentityUserViewModel viewModel, string passwordHash)
        {
            viewModel.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }
        #endregion

        #region IUserSecurityStampStore<IdentityUserViewModel, long> Members
        public Task<string> GetSecurityStampAsync(IdentityUserViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<string>(viewModel.SecurityStamp);
        }

        public Task SetSecurityStampAsync(IdentityUserViewModel viewModel, string stamp)
        {
            viewModel.SecurityStamp = stamp;
            return Task.FromResult(0);
        }
        #endregion

        #region Private Methods
        private User GetUserModel(IdentityUserViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            var model = viewModel.ToEntityModel<User, IdentityUserViewModel>();
            return model;
        }

        private IdentityUserViewModel GetIdentityUserViewModel(User model)
        {
            if (model == null)
                return null;

            var viewModel = model.ToViewModel<User, IdentityUserViewModel>();

            return viewModel;
        }

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
        // ~UserStore() {
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

        public ResponseResults<RefreshTokenViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ResponseResult<RefreshTokenViewModel> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public ResponseResult<RefreshTokenViewModel> Save(RefreshTokenViewModel viewModel)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}
