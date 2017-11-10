using WebApi2.IDomainServices.IdentityStores;
using WebApi2.Utility;
using WebApi2.ViewModels.Identity.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Threading.Tasks;

namespace WebApi2.EndPointApi.Providers
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");
            var userManager = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<UserManager<IdentityUserViewModel, long>>();

            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime"); 
               
            var token = new RefreshTokenViewModel() 
            { 
                TokenId = AppMethods.GetHash(refreshTokenId),
                ClientId = clientid, 
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime)) 
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;
                
            token.ProtectedTicket = context.SerializeTicket();
            var refreshTokenService = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<IRefreshTokenService>();

            var result = await refreshTokenService.AddRefreshToken(token);

            if (result)
            {
                context.SetToken(refreshTokenId);
            }
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {

            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            var refreshTokenService = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<IRefreshTokenService>();

            string hashedTokenId = AppMethods.GetHash(context.Token);

            var refreshToken = await refreshTokenService.FindRefreshToken(hashedTokenId);

            if (refreshToken != null )
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                var result = await refreshTokenService.RemoveRefreshToken(hashedTokenId);
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}