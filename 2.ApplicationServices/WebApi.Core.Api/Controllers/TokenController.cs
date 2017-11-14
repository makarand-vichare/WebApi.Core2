using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Core.ViewModels.Identity.WebApi;

namespace WebApi.Core.Api.Controllers
{

    [Route("/token")]
    public class TokenController : Controller
    {
        private UserManager<IdentityUserViewModel> userManager;
        private SignInManager<IdentityUserViewModel> signInManager;

        public TokenController(UserManager<IdentityUserViewModel> _userManager,
            SignInManager<IdentityUserViewModel> _signInManager)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
        }
        [HttpPost]
        public IActionResult Create(string username, string password)
        {
            if (IsValidUserAndPasswordCombination(username, password))
                return new ObjectResult(GenerateToken(username));
            return BadRequest();
        }

        private bool IsValidUserAndPasswordCombination(string username, string password)
        {
            var result = userManager.FindByNameAsync(username).Result;
            if(result == null)
            {
                return false;
            }

            var passwordSignInResult = signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false).Result;
            return passwordSignInResult.Succeeded;
        }

        private string GenerateToken(string username)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the secret that needs to be at least 16 characeters long for HmacSha256")),
                                             SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}